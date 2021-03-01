using System.IO;
using FluentValidation;
using Test.Pages;

namespace Test.Validations
{
    public class FeatureViewModelValidator : AbstractValidator<FeatureViewModel>
    {
        public FeatureViewModelValidator()
        {
            RuleFor(vm => vm.InputFile).NotEmpty().WithMessage("请指定文件");
            RuleFor(vm => vm.InputFile).Must(File.Exists).WithMessage("文件不存在!");

            RuleFor(vm => vm.InputFolder).NotEmpty().WithMessage("请指定文件夹");
            RuleFor(vm => vm.InputFolder).Must(Directory.Exists).WithMessage("文件夹不存在!");
        }
    }
}