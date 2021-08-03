using StockAccountManagement.StockManagement;
using System;


namespace StockAccountManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables
            int userChoice = 0;
            double amount;
            string symbol;
            //constants
            const string COMPANY = @"C:\Users\saura\source\repos\StockAccountManagement\Company.json";
            const int COMPANYDETAILS = 1, ADD_NEW_ACCOUNT = 2, VALUE_OF_ACCOUNT = 3, BUY = 4, SELL = 5, PRINT = 6, EXIT = 7;
            StockAccount stockAccount = new StockAccount();
            while (userChoice != 7)
            {
                Console.WriteLine("Press 1 : Show All Account List");
                Console.WriteLine("Press 2 : Add New Account");
                Console.WriteLine("Press 3 : Get Value of Account");
                Console.WriteLine("Press 4 : Buy a Share");
                Console.WriteLine("Press 5 : Sell a Share");
                Console.WriteLine("Press 6 : PrintReport");
                Console.WriteLine("Press 7 : Exit");
                Console.WriteLine("Enter your choice");
                userChoice = Convert.ToInt16(Console.ReadLine());
                switch (userChoice)
                {
                    case COMPANYDETAILS:
                        stockAccount.AllCompany(COMPANY);
                        break;
                    case ADD_NEW_ACCOUNT:
                        stockAccount.AddNewAccount(COMPANY);
                        break;
                    case VALUE_OF_ACCOUNT:
                        string companyName;
                        Console.WriteLine("Enter the name of Company to show total amount");
                        companyName = Console.ReadLine();
                        amount = stockAccount.TotalValueOfAccount(companyName);
                        if (amount < 0)
                            Console.WriteLine("Account name does not exit");
                        else
                            Console.WriteLine("Account Amount is $ :" + amount );
                        break;
                    case BUY:
                        Console.WriteLine("Enter a amount");
                        amount = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Enter a symbol");
                        symbol = Console.ReadLine();
                        stockAccount.Buy(amount, symbol);
                        break;
                    case SELL:
                        Console.WriteLine("Enter a amount");
                        amount = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Enter a symbol");
                        symbol = Console.ReadLine();
                        stockAccount.Sell(amount, symbol);
                        break;
                    case PRINT:
                        stockAccount.PrintReport();
                        break;
                    case EXIT:
                        userChoice = 7;
                        break;
                    default:
                        Console.WriteLine("Entered Wrong choice");
                        break;
                }
            }
        }

    }
}

