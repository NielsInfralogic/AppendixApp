using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppendixApp.Services;
using AppendixApp.Views;
using Xamarin.Essentials;

namespace AppendixApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<AppendixHttpClient>();
            DependencyService.Register<DialogService>();

            //
            //Preferences.Get("CompanyID", 1234);
            //Preferences.Get("UserID", 100);

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
