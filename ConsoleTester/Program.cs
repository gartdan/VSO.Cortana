using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VSO.Cortana.Service;
using VSO.Cortana.Service.Models;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string accountInfo = "../../passwords.IGNORE.txt"; //my account info. don't check this into version control
            var lines = File.ReadAllLines(accountInfo);
            var userName = lines[0];
            var password = lines[1];
            var service = new VSOService("dgartner", userName, password);
            //var response = service.GetWorkItemById(682);
            var response = service.GetWorkItemsByTitle("Mobile");
            //563, 564
            var workItems = service.GetWorkItemsById(563, 564);
        }
    }
}
