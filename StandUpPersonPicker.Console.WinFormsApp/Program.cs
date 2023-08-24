using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using StandUpPersonPicker.Core.Implementations;
using StandUpPersonPicker.Core.Interfaces;
using StandUpPersonPicker.Domain.Models;

namespace StandUpPersonPicker.Console.WinFormsApp
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
                .AddSingleton<IPersonBl, PersonBl>()
                .BuildServiceProvider();

            var personBl = serviceProvider.GetService<IPersonBl>();

            if (personBl == null)
            {
                throw new ArgumentNullException(nameof(personBl));
            }

            var configurationRoot = serviceProvider.GetService<IConfigurationRoot>();

            if (configurationRoot == null)
            {
                throw new ArgumentNullException(nameof(configurationRoot));
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(personBl, configurationRoot));
            return Task.CompletedTask;
        }
    }
}