using ApiClient.Repositories;
using JarmuKolcsonzo.Model.Entities;
using JarmuKolcsonzo.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace JarmuKolcsonzo.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // https://learn.microsoft.com/en-us/windows/communitytoolkit/mvvm/ioc
        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
        }
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<IPagerRepository<Jarmu>, PagerAPIRepository<Jarmu>>(x =>
            {
                return new PagerAPIRepository<Jarmu>("api/Jarmuvek");
            });
            services.AddTransient<IGenericRepository<JarmuTipus>, GenericAPIRepository<JarmuTipus>>(x =>
            {
                return new PagerAPIRepository<JarmuTipus>("api/JarmuTipusok");
            });
            services.AddTransient<JarmuvekViewModel>();
            services.AddTransient<UgyfelekViewModel>();
            return services.BuildServiceProvider();
        }
    }
}
