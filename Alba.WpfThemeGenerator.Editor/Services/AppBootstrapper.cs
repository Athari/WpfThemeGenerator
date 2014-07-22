using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using Alba.Framework.Caliburn;
using Alba.Framework.Diagnostics;
using Alba.Framework.Text;
using Alba.WpfThemeGenerator.Editor.ViewModels;
using Caliburn.Micro;
using EventTrigger = System.Windows.Interactivity.EventTrigger;
using ILog = Caliburn.Micro.ILog;

namespace Alba.WpfThemeGenerator.Editor.Services
{
    public class AppBootstrapper : BootstrapperBase
    {
        //private static readonly Logger _log = LogManager.GetCurrentClassLogger();

        private CompositionContainer _container;
        private UnhandledExceptionsHandler _unhandledExceptionsHandler;

        public AppBootstrapper ()
        {
            Start();
        }

        protected override void Configure ()
        {
            //AppDomain.CurrentDomain.SetData("DataDirectory", CommonConsts.CommonAppDir);

            _unhandledExceptionsHandler = new UnhandledExceptionsHandler();
            _unhandledExceptionsHandler.UnhandledException += UnhandledExceptionsHandler_OnUnhandledException;
            //LogManager.GetLog = t => new DebugLogger();
            //LogManager.GetLog = t => new NLogCaliburnLogger(t);
            //_log.Info("{0}#{0}# Started {1}{0}#", Environment.NewLine, Paths.ExecutableFilePath);

            Parser.CreateTrigger = (target, triggerText) => {
                if (triggerText == null)
                    return ConventionManager.GetElementConvention(target.GetType()).CreateTrigger();
                string triggerName, triggerArg;
                triggerText.Trim().RemoveSuffixes("[", "]").Split(out triggerName, out triggerArg, ' ');
                switch (triggerName.Trim()) {
                    case "Event":
                        return new EventTrigger(triggerArg.Trim());
                    case "Key":
                        return new KeyTrigger(triggerArg.Trim());
                    default:
                        throw new ArgumentException("Unsupported trigger type: {0}".Fmt(triggerName));
                }
            };

            var config = new TypeMappingConfiguration {
                ViewModelSuffix = "Model",
                IncludeViewSuffixInViewModelNames = false,
            };
            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);

            _container = new CompositionContainer(
                new AggregateCatalog(
                    AssemblySource.Instance.Select(x => new AssemblyCatalog(x))
                    //.Concat(new AssemblyCatalog(typeof(TypeName).Assembly))
                    ));
            _container.SatisfyImportsOnce(this);

            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            _container.Compose(batch);
        }

        private void UnhandledExceptionsHandler_OnUnhandledException (object sender, UnhandledExceptionsEventArgs args)
        {
            args.Handled = true;
            if (args.CanHandle) {
                MessageBoxResult result = MessageBox.Show(Application.MainWindow,
                    "Unhandled exception occured in {0}.\n\n{1}\n\nContinue execution?".Fmt(args.Source, args.FullMessage),
                    "Error", MessageBoxButton.YesNo, MessageBoxImage.Error, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                    args.Handled = true;
                else
                    FailFast(args);
            }
            else {
                MessageBox.Show(Application.MainWindow,
                    "Unhandled exception occured in {0}.\n\n{1}\n\nApplication will terminate now.".Fmt(args.Source, args.FullMessage),
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                FailFast(args);
            }
        }

        private static void FailFast (UnhandledExceptionsEventArgs args)
        {
            if (Debugger.IsAttached)
                Debugger.Break();
            else
                Environment.FailFast("Unhandled exception.", args.Exceptions.FirstOrDefault());
        }

        protected override IEnumerable<Assembly> SelectAssemblies ()
        {
            yield return GetType().Assembly;
        }

        protected override object GetInstance (Type type, string key)
        {
            if (!type.IsInterface && !type.IsAbstract)
                return Activator.CreateInstance(type);
            string contract = !key.IsNullOrEmpty() ? key : AttributedModelServices.GetContractName(type);
            return _container.GetExportedValue<object>(contract);
        }

        protected override IEnumerable<object> GetAllInstances (Type type)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(type));
        }

        protected override void BuildUp (object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup (object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<EditorModel>();
        }

        private class DebugLogger : ILog
        {
            public void Info (string format, params object[] args)
            {
                string msg = format.Fmt(args);
                if (msg == "Invoking Action: MouseMove.")
                    return;
                Debug.WriteLine("INFO: " + msg);
            }

            public void Warn (string format, params object[] args)
            {
                Debug.WriteLine("WARN: " + format.Fmt(args));
            }

            public void Error (Exception exception)
            {
                Debug.WriteLine("ERROR: " + exception);
            }
        }
    }
}