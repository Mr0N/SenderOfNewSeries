using Message.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Http;
using Message.Service;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Requests;
//using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Threading.Tasks;
using System.Threading;
using Hang.TaskScheduler;
///using Microsoft.Configuration.ConfigurationBuilder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
namespace Implementer
{
    class Program
    {
        static void Config(ServiceCollection services)
        {
            var config = configuration.Get<Config>(); 
            services.AddDbContext<Context>(a =>
            {
                a.UseSqlServer(config.ConnectionString);
            });
            services.AddHttpClient();
            services.AddScoped<Config>(a =>
            {
                return config;
            });
            services.AddScoped<Parser>();
            services.AddScoped<Extract>();
            services.AddScoped<TelegramBotClient>(a =>
            {
                var config = a.GetService<Config>();
                return new TelegramBotClient(config.TokenTelegramBot);
            });
            services.AddScoped<UpdateTelegram>();
        }
        static ServiceProvider CreateProvider()
        {
            var service = new ServiceCollection();
            Config(service);
            return service.BuildServiceProvider();
        }

        async static Task Main(string[] args)
        {
            
            using (provider)
            {
                await Initialization();
            }
        }
        async static Task Initialization()
        {
            while (true)
            {
                await Extract.Current.Start();
                await UpdateTelegram.Current.Start();
                await Task.Delay(3000);
            }
        }
        public static ServiceProvider provider { get; }
        public static IConfigurationRoot configuration { get; set; }
        static Program()
        {

            var obj = new ConfigurationBuilder();
            obj.AddJsonFile("config.json");
            configuration = obj.Build();
            provider = CreateProvider();
        }
    }

}
