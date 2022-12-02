using AutoMapper;
using FictionDataAccessLibrary.Data;
using FictionDataAccessLibrary.DbAccess;
using FictionDataAccessLibrary.Models;
using FictionHoarder;
using FictionHoarderWPF.Core;
using FictionUI_Library.API;
using FictionHoarderWPF.MVVM.Model;
using FictionHoarderWPF.MVVM.View;
using FictionHoarderWPF.MVVM.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FictionUI_Library.Models;
using Prism.Events;

namespace FictionHoarderWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    var config = new MapperConfiguration(myConfig =>
                    {
                        myConfig.CreateMap<StoryModel, StoryDisplayModel>();
                        myConfig.CreateMap<StoryDisplayModel, StoryModel>();
                    });

                    var mapper = config.CreateMapper();

                    services.AddSingleton(mapper);
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<IEventAggregator, EventAggregator>();
                    services.AddSingleton<ILoggedInUser, LoggedInUser>();
                    services.AddSingleton<IApiHelper, ApiHelper>();
                    services.AddScoped<IStoryEndpoint, StoryEndpoint>();
                    services.AddMemoryCache();

                }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();

            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show(); 

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();

            base.OnExit(e);
        }

    }
}
