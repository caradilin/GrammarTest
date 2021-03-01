using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Test.Pages;

namespace Test.Utilities
{
    public class ReflectionUtility
    {
        public static IEnumerable<Type> GetViewModelTypes()
        {
            // 使用反射查找ViewModel类型
            return Assembly.GetAssembly(typeof(ViewModelBase)).GetTypes()
                    .Where(t => t.Name.EndsWith("ViewModel"))
                    .Where(t => t.IsSubclassOf(typeof(ViewModelBase)))
                ;
        }

        public static string GetVersionText()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            return $"{version.Major}.{version.Minor}";
        }
    }
}