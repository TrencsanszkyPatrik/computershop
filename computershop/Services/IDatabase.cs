using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computershop.Services
{
    interface IDatabase
    {
        DataView GetAllData();
        object GetData(string username, string password);
        object AddRecord(string username, string fullname, string email, string password);




    }
}
