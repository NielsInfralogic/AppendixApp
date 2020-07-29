using AppendixApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppendixApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppendixListPage : ContentPage
    {
        AppendixListViewModel viewModel;
        public AppendixListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new AppendixListViewModel();
        }
    }
}