using System;

namespace BankingApplication
{
  [Flags]
  public enum ServiceType
  {
    Own = 1,
    Foreign = 2,
    All = 3
  }
}