using System.Runtime.InteropServices;

namespace BankKomponentenTestCRaute
{
    // ReSharper disable once ArrangeTypeModifiers
    // ReSharper disable once InconsistentNaming
    class API
    {
        [DllImport("BankKomponenten.dll")]
        public static extern void Foo(Currency currency);
    }
}
