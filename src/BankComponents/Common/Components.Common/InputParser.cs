using System;
using Components.Common.Exceptions;
using Components.Contracts;

namespace Components.Common
{
    public static class InputParser
    {
        public static int ParseInt(string parameterName, string toparse)
        {
            int val;
            if (!int.TryParse(toparse, out val))
            {
                throw new InvalidParameterException(parameterName);
            }
            return val;
        }

        public static AccountType ParseAccountType(string parameterName, string toparse)
        {
            int val;
            if (!int.TryParse(toparse, out val))
            {
                throw new InvalidParameterException(parameterName);
            }
            switch (val)
            {
                case 1:
                    return AccountType.SavingsAccount;
                case 2:
                    return AccountType.LoanAccount;
                default:
                    throw new InvalidParameterException(parameterName);
            }
        }

        public static int GetIntInput(string message, string parameterName)
        {
            Console.Write(message);
            var input = Console.ReadLine();
            int val;
            if (!int.TryParse(input, out val))
            {
                throw new InvalidInputException($"Input {input} not Valid for: {parameterName}");
            }
            return val;
        }

        public static int GetIntInput(string message, string parameterName, Func<int, bool> predicate)
        {
            var val = GetIntInput(message, parameterName);
            if (!predicate.Invoke(val))
            {
                throw new InvalidInputException($"Invlid range for {parameterName}.");
            }
            return val;
        }

        public static double GetDoubleInput(string message, string parameterName)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();
            double val;
            if (!double.TryParse(input, out val))
            {
                throw new InvalidInputException($"Input {input} not Valid for: {parameterName}");
            }
            return val;
        }

        public static double GetDoubleInput(string message, string parameterName, Func<double, bool> predicate)
        {
            var val = GetDoubleInput(message, parameterName);
            if (!predicate.Invoke(val))
            {
                throw new InvalidInputException($"Invlid range for {parameterName}.");
            }
            return val;
        }

        public static string GetStringInput(string message, string parameterName)
        {
            Console.WriteLine(message);
            var val = Console.ReadLine();
            return val;
        }

        public static string GetStringInput(string message, string parameterName, Func<string,bool> predicate)
        {
            var val = GetStringInput(message, parameterName);
            if (!predicate.Invoke(val))
            {
                throw new InvalidInputException($"Invlid range for {parameterName}.");
            }
            return val;
        }

        public static AccountType GetAccountTypeInput()
        {
            var accountTypeInt = GetIntInput("Enter AccountType SavingsAccount=1 LoadAccount=2: ", "Account type",
                i => (i == 1 || i == 2));
            switch (accountTypeInt)
            {
                case 1:
                    return AccountType.LoanAccount;
                default:
                    return AccountType.SavingsAccount;
            }
        }

        public static OwnCurrency GetCurrency()
        {
            var currencyString = GetStringInput("Enter Currency: ", "Currency",
                s => (!string.IsNullOrWhiteSpace(s) && s.Length == 3));
            OwnCurrency currency;
            if (!Enum.TryParse(currencyString, true, out currency))
            {
                throw new InvalidInputException($"Invalid currency. Enter e.g. EUR for Euro.");
            }
            return currency;
        }
    }
}