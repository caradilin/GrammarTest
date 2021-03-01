using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using NLog;
using Stylet;

namespace Test.Pages
{
    public class ViewModelBase : Screen
    {
        /// <summary>
        /// Logger
        /// </summary>
        protected static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 对话框服务
        /// </summary>
        protected IDialogCoordinator DialogCoordinator;

        /// <summary>
        /// 是否处于空闲状态
        /// </summary>
        public bool IsIdle { get; set; } = true;

        /// <summary>
        /// 进度
        /// </summary>
        public int Progress { get; set; }

        public ViewModelBase(IModelValidator validator) : base(validator)
        {
            DialogCoordinator = MahApps.Metro.Controls.Dialogs.DialogCoordinator.Instance;
        }

        /// <summary>
        /// 显示Message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="style"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public Task<MessageDialogResult> ShowMessageAsync(string title, string message,
            MessageDialogStyle style = MessageDialogStyle.Affirmative, MetroDialogSettings settings = null)
        {
            if (settings == null)
            {
                settings = new MetroDialogSettings
                {
                    ColorScheme = MetroDialogColorScheme.Theme,
                    AffirmativeButtonText = "確定",
                    NegativeButtonText = "取消",
                };
            }
            else
            {
                settings.ColorScheme = MetroDialogColorScheme.Theme;
            }

            return DialogCoordinator.ShowMessageAsync(RootViewModel, title, message, style, settings);
        }

        /// <summary>
        /// 取得根ViewModel (ShellViewModel)
        /// </summary>
        public ShellViewModel RootViewModel
        {
            get
            {
                Screen vm = this;
                while (!(vm is ShellViewModel ))
                {
                    vm = (Screen) vm.Parent;
                }

                return (ShellViewModel) vm;
            }
        }
    }
}