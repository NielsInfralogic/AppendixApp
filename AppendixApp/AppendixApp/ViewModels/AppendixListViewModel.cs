using AppendixApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppendixApp.ViewModels
{
    public class AppendixListViewModel : BaseViewModel
    {
        private ObservableCollection<Appendix> appendices;
        public ObservableCollection<Appendix> Appendices
        {
            get { return this.appendices; }
            set { SetProperty(ref appendices, value); }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        public ICommand RefreshCommand => new Command(async () => await ExecuteRefresh());

        public AppendixListViewModel()
        {
            IsRefreshing = false;
            ExecuteRefresh().ContinueWith(t =>
            {
                // if (t.IsFaulted)
                //   Log(t.Exception);
            });
        }

        private async Task ExecuteRefresh()
        {
            await LoadData(Preferences.Get("UserID", 123));
        }

        private async Task LoadData(int userID)
        {
            IsRefreshing = true;
            var list = await AppendixHttpClient.GetAppendices(Preferences.Get("CompanyID", 1234), userID);
            Appendices = new ObservableCollection<Appendix>(list);
            IsRefreshing = false;
            return;
        }
    }
}
