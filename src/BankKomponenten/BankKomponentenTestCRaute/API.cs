using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
    
namespace BankKomponentenTestCRaute
{
    class API
    {
        [DllImport("BankKomponenten.dll")]
        public static extern void Foo(Currency currency);
    }
}
