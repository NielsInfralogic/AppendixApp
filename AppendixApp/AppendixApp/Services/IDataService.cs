using AppendixApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppendixApp.Services
{
    public interface IDataService
    {
        Task<StatusResponse> SendAppendixAsync(Appendix appendix);

        Task<List<Appendix>> GetAppendices(int companyID, int userID);

        Task<List<Account>> GetAccounts(int companyID);
    }
}
