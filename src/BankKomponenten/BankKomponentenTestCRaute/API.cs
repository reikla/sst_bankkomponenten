using System.Runtime.InteropServices;

namespace BankKomponentenTestCRaute
{
    // ReSharper disable once ArrangeTypeModifiers
    class API
    {
        [DllImport("BankKomponenten.dll")]
        public static extern void Foo(Currency currency);
    }
}
