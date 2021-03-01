using System;
using System.IO;
using System.Windows;
using AutoMapper;
using Newtonsoft.Json;
using NLog;

namespace Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static string AppDirectory => AppDomain.CurrentDomain.BaseDirectory;
        public static AppSettings AppSettings;
        public static bool NoUpdate;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                string appSettingsFile = Path.Combine(AppDirectory, Const.APP_SETTINGS_FILE);
                if (File.Exists(appSettingsFile))
                {
                    string json = File.ReadAllText(appSettingsFile);
                    AppSettings = JsonConvert.DeserializeObject<AppSettings>(json);
                }
                else
                {
                    AppSettings = new AppSettings();
                }
                Mapper.Initialize(cfg => cfg.AddProfile<MapperProfile>());
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "加载配置文件失败,放弃加载.");
            }
            base.OnStartup(e);
            Logger.Info("工具启动.");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            string appSettingsFile = Path.Combine(AppDirectory, Const.APP_SETTINGS_FILE);
            string json = JsonConvert.SerializeObject(AppSettings);
            File.WriteAllText(appSettingsFile, json);
            Logger.Info("工具退出.");
            base.OnExit(e);
        }
    }
}
