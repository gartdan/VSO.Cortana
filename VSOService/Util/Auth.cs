using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSO.Cortana.Service.Util
{
    public static class Auth
    {
        public static string GetBasicAuthHeaderValue(string userName, string password)
        {
            string auth = Convert.ToBase64String(Encoding.GetEncoding("ASCII")
                .GetBytes(string.Format("{0}:{1}", userName, password)));
            return auth;
        }
    }
}
