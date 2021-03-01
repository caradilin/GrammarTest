using AutoMapper;
using Test.Utilities;

namespace Test
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            var typeAppSettings = typeof(AppSettings);
            foreach (var vmType in ReflectionUtility.GetViewModelTypes())
            {
                // 创建XXXViewModel和AppSettings的AutoMapper映射关系
                CreateMap(typeAppSettings, vmType);
                CreateMap(vmType, typeAppSettings);
            }
        }
    }
}