using System;

namespace BankingApplication
{
  [Flags]
  public enum FunctionalAvailabilities
  {
    Own=1,
    Foreign=2,
    All=3
  }
}