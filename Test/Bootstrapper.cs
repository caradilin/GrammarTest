using FluentValidation;
using MahApps.Metro.Controls.Dialogs;
using Stylet;
using StyletIoC;
using Test.Pages;
using Test.Validations;

namespace Test
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            // Configure the IoC container in here
            builder.Bind<IDialogCoordinator>().ToInstance(DialogCoordinator.Instance);
            builder.Bind(typeof(IModelValidator<>)).To(typeof(FluentModelValidator<>));
            builder.Bind(typeof(IValidator<>)).ToAllImplementations();
        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts
        }
    }
}
