using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using StandUpDeveloperPicker.Core.Implementations;
using StandUpDeveloperPicker.Core.Interfaces;
using StandUpDeveloperPicker.Domain.Models;

namespace StandUpDeveloperPicker.Console.WinFormsApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static Task Main()
        {

            var settings = new Settings();
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            configuration.Bind(settings);

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfigurationRoot, ConfigurationRoot>(_ => (ConfigurationRoot)configuration)
                .AddSingleton<IRestClient, RestClient>(_ => new RestClient(settings.ApiBaseUrl))
                .AddSingleton<ICharacterBl, CharacterBl>()
                .AddSingleton<IDeveloperBl, DeveloperBl>()
                .BuildServiceProvider();

            var developerBl = serviceProvider.GetService<IDeveloperBl>();

            if (developerBl == null)
            {
                throw new ArgumentNullException(nameof(developerBl));
            }

            var configurationRoot = serviceProvider.GetService<IConfigurationRoot>();

            if (configurationRoot == null)
            {
                throw new ArgumentNullException(nameof(configurationRoot));
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(developerBl, configurationRoot));
            return Task.CompletedTask;
        }
    }
}