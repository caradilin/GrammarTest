using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Stylet;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Test.Pages
{
    public class FeatureViewModel : ViewModelBase
    {
        public string InputFile { get; set; }
        public string InputFolder { get; set; }

        public FeatureViewModel(IModelValidator<FeatureViewModel> validator) : base(validator)
        {
            DisplayName = "TestTemplate";
            // 恢复上次输入内容
            Mapper.Map(App.AppSettings, this);
        }

        public void BrowseFile()
        {
            var dlg = new OpenFileDialog
            {
                Title = "选择文件",
                Filter = "Excel文件|*.xlsx",
                CheckFileExists = true,
            };
            if (dlg.ShowDialog() != true) return;
            InputFile = dlg.FileName;
        }

        public async Task Run()
        {
            if (!IsIdle) return; // 防止二次点击

            try
            {
                IsIdle = false;
                // 验证
                if (!Validate()) return;


                // 保存输入内容
                Mapper.Map(this, App.AppSettings);

                // 运行
               RunJob();

                await ShowMessageAsync("成功", "运行完了");
            }
            catch (Exception e)
            {
                Logger.Error(e);
                await ShowMessageAsync("异常", e.Message);
            }
            finally
            {
                Progress = 0;
                IsIdle = true;
            }
        }
        /// <summary>
        /// 运行JOB
        /// </summary>
        /// <returns></returns>
        private  void RunJob()
        {
            string text = "Let’s suppose this is a really long string";

            var letterFrequencies1 = new int[26];
            foreach (char c in text)
            {
                int index = char.ToUpper(c) - 'A';
                if (index >= 0 && index <= 26) letterFrequencies1[index]++;
            };

            int[] result1 =
                text.Aggregate(
                    new int[26],                // Create the "accumulator"
                    (letterFrequencies, c) =>   // Aggregate a letter into the accumulator
                    {
                        int index = char.ToUpper(c) - 'A';
                        if (index >= 0 && index <= 26) letterFrequencies[index]++;
                        return letterFrequencies;
                    });
            Logger.Info(string.Join("",result1));
            int[] result2 =
                text.AsParallel().Aggregate(
                    () => new int[26],             // Create a new local accumulator

                    (localFrequencies, c) =>       // Aggregate into the local accumulator
                    {
                        int index = char.ToUpper(c) - 'A';
                        if (index >= 0 && index <= 26) localFrequencies[index]++;
                        return localFrequencies;
                    },
                    // Aggregate local->main accumulator
                    (mainFreq, localFreq) =>
                        mainFreq.Zip(localFreq, (f1, f2) => f1 + f2).ToArray(),

                    finalResult => finalResult     // Perform any final transformation
                );
            Logger.Info(string.Join("", result2));
            //
            //
            //
            //
            //
            //test2
            //test3
        }
    }
}