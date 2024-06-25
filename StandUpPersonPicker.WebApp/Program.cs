using RestSharp;
using StandUpPersonPicker.Core.Implementations;
using StandUpPersonPicker.Core.Interfaces;
using StandUpPersonPicker.Domain.Models;
using StandUpPersonPicker.Infrastructure.Dals.Implementations;
using StandUpPersonPicker.Infrastructure.Dals.Interfaces;
using StandUpPersonPicker.Infrastructure.Wrappers.Implementations;
using StandUpPersonPicker.Infrastructure.Wrappers.Interfaces;

namespace StandUpPersonPicker.WebApp
{
	public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<ISQLiteWrapper<StatisticDao>, SQLiteWrapper<StatisticDao>>();
            builder.Services.AddSingleton<IStatisticDal, StatisticDal>();
			builder.Services.AddSingleton<IRestClient, RestClient>(_ => new RestClient("https://rickandmortyapi.com/api/"));
            builder.Services.AddSingleton<ICharacterBl, CharacterBl>();
            builder.Services.AddSingleton<IPersonBl, PersonBl>();

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var app = builder.Build();

            app.Services.GetRequiredService<IStatisticDal>();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
