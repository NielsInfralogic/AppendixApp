using AppendixApp.Models;
using AppendixApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppendixApp.ViewModels
{
    public class NewAppendixViewModel : BaseViewModel
    {
        private int useID;
        public int UserID
        {
            get { return useID; }
            set { SetProperty(ref useID, value); }
        }

        private string account;
        public string Account
        {
            get { return account; }
            set { SetProperty(ref account, value); }
        }

        private string note;
        public string Note
        {
            get { return note; }
            set { SetProperty(ref note, value); }
        }

        private DateTime appendixDate;
        public DateTime AppendixDate
        {
            get { return appendixDate; }
            set { SetProperty(ref appendixDate, value); }
        }

        private string fileName;
        public string FileName
        {
            get { return this.fileName; }
            set { SetProperty(ref fileName, value); }
        }

        private ObservableCollection<string> accounts;
        public ObservableCollection<string> Accounts
        {
            get { return accounts; }
            set { SetProperty(ref accounts, value); }
        }
        

        public ICommand UsePhotoCommand  => new Command(async () => await ExecuteSaveImageCommand());

        public NewAppendixViewModel()
        {
            Title = "New appendix picture";
            AppendixDate = DateTime.Today;
            UserID = Preferences.Get("UserID", 123);

            // Move to OnAppearing (event->command)
            GetAccounts().ContinueWith(t =>
            {
               // if (t.IsFaulted)
                 //   Log(t.Exception);
            });

        }

        private async Task GetAccounts()
        {
            List<Account> accountsFromAPI = await AppendixHttpClient.GetAccounts(Preferences.Get("CompanyID", 1234));

            List<string> accountNames = new List<string>();
            foreach (Account accountFromAPI in accountsFromAPI)
                accountNames.Add($"{accountFromAPI.AccountNo} {accountFromAPI.Name}");
            Accounts = new ObservableCollection<string>(accountNames);
        }

        private int GetAccountNumberFromName(string s)
        {
            int n = s.IndexOf(" ");
            if (n == -1)
                return 0;
            Int32.TryParse(s.Substring(0, n).Trim(), out int res);
            return res;
        }
        private async Task ExecuteSaveImageCommand()
        {
            Preferences.Set("UserID", GetAccountNumberFromName(Account));

            Models.Appendix appendix = new Models.Appendix()
            {
                CompanyID = Preferences.Get("CompanyID", 1234),
                AccountNo = Preferences.Get("UserID",123),
                Text = Note,
                UserID = UserID,
                ImageFileName = Path.GetFileName(FileName),
                Date = string.Format("{0:0000}-{1:00}-{2:00}", AppendixDate.Year, AppendixDate.Month, AppendixDate.Day)
            };

            byte[] bytes = null;
            try
            {
                bytes = File.ReadAllBytes(FileName);
            }
            catch
            {
            }

            if (bytes != null)
            {
                appendix.ImageData = Convert.ToBase64String(bytes);

                StatusResponse response = await AppendixHttpClient.SendAppendixAsync(appendix);

                if (response.Status > 0)
                    await DialogService.Show("Success", "Appendix saved OK", "Close");
                else
                    await DialogService.Show("Error", response.Message, "Close");
            }


        }
    }
}
