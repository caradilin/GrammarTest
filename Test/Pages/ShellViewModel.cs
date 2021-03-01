using System.Diagnostics;
using System.IO;
using MahApps.Metro.Controls.Dialogs;
using Stylet;
using StyletIoC;
using Test.Utilities;

namespace Test.Pages
{
    public class ShellViewModel : Conductor<ViewModelBase>.Collection.OneActive
    {
        private readonly string _logFile = $"APP.log";
        private readonly IContainer _container;
        private readonly IDialogCoordinator _dialogCoordinator;
        public string Title => $"{Const.APP_NAME} v{ReflectionUtility.GetVersionText()}";

        public ShellViewModel(IContainer container, IDialogCoordinator dialogCoordinator)
        {
            _container = container;
            _dialogCoordinator = dialogCoordinator;
        }

        protected override void OnViewLoaded()
        {
            foreach (var vmType in ReflectionUtility.GetViewModelTypes())
            {
                // 添加到Items中
                Items.Add((ViewModelBase)_container.Get(vmType));
            }

            if (Items.Count > 0)
            {
                // 激活第一个TAB
                ActivateItem(Items[0]);
            }
        }

        public async void OpenLogFile()
        {
            string logFile = Path.Combine(App.AppDirectory, "Logs", _logFile);
            if (!File.Exists(logFile))
            {
                await _dialogCoordinator.ShowMessageAsync(this, "错误", $"日志文件 [{logFile}] 未找到.");
                return;
            }

            Process.Start(logFile);
        }

    }
}