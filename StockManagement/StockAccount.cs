using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StockAccountManagement.StockManagement
{
    class StockAccount
    {
        const string COMPANY = @"C:\Users\saura\source\repos\StockAccountManagement\Company.json";
        const string STOCKLIST = @"C:\Users\saura\source\repos\StockAccountManagement\StockManagement\StockList.json";

        /// <summary>
        /// Show all account list
        /// </summary>
        /// <param name="filepath"></param>
        public void AllCompany(string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    string allDataFile = File.ReadAllText(filepath);
                    List<ModelClass> companyModel = JsonConvert.DeserializeObject<List<ModelClass>>(allDataFile);
                    Console.WriteLine("Name\t\tSymbol\tShares\tTotalAmount\tDate Time");
                    foreach (ModelClass companyData in companyModel)
                    {
                        Console.WriteLine(companyData.Name + "\t" + companyData.Symbol + "\t" + companyData.NumberOfShares + "\t" + companyData.Amount + "\t\t" + companyData.DateTime);
                    }
                }
                else
                {
                    Console.WriteLine("File does not exits");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Add new Account
        /// </summary>
        /// <param name="filepath"></param>
        public void AddNewAccount(string filepath)
        {
            if (File.Exists(filepath))
            {
                string allDataFile = File.ReadAllText(filepath);
                List<ModelClass> companyModels= JsonConvert.DeserializeObject<List<ModelClass>>(allDataFile);
                ModelClass companymodel = new ModelClass();
                Console.WriteLine("Enter Company Name");
                companymodel.Name = Console.ReadLine();
                companymodel.NumberOfShares = 0;
                Console.WriteLine("Enter Amount");
                companymodel.Amount = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter a Symbol");
                companymodel.Symbol = Console.ReadLine();
                DateTime dateTime = DateTime.Now;
                companymodel.DateTime = dateTime;
                companyModels.Add(companymodel);
                File.WriteAllText(filepath, JsonConvert.SerializeObject(companyModels));
                Console.WriteLine("New Account Created Successfully");
            }
            else
            {
                Console.WriteLine("file does not exits");
            }
        }
        public double TotalValueOfAccount(string companyName)
        {
            bool checkCompany = false;
            double returnAmount = 0;

            List<ModelClass> companymodel = JsonConvert.DeserializeObject<List<ModelClass>>(File.ReadAllText(COMPANY));
            foreach (ModelClass company in companymodel)
            {
                if (company.Name.Equals(companyName))
                {
                    checkCompany = true;
                    returnAmount = company.Amount;
                    break;
                }
            }
            if (checkCompany == false)
            {
                returnAmount = -1;
            }
            return returnAmount;
        }
        public void Sell(double amount, string symbol)
        {
            bool checkSymbol = false, checkStock = false;
            StockPortfolio stockPortfolio = new StockPortfolio();
            stockPortfolio.ShowAllStock(STOCKLIST);
            Console.WriteLine("Which share do you want to sell");
            string shareName = Console.ReadLine();
            List<StockModel> stockList = JsonConvert.DeserializeObject<List<StockModel>>(File.ReadAllText(STOCKLIST));
            foreach (StockModel allStocks in stockList)
            {
                if (allStocks.StockName.Equals(shareName))
                {
                    double sellShares = (amount / allStocks.SharePrice);
                    List<ModelClass> companyList = JsonConvert.DeserializeObject<List<ModelClass>>(File.ReadAllText(COMPANY));
                    foreach (ModelClass company in companyList)
                    {
                        if (company.Symbol.Equals(symbol))
                        {
                            if (company.NumberOfShares >= sellShares)
                            {
                                company.Amount += amount;
                                company.NumberOfShares -= sellShares;
                                DateTime dateTime = DateTime.Now;
                                company.DateTime = dateTime;
                                File.WriteAllText(COMPANY, JsonConvert.SerializeObject(companyList));
                                allStocks.NumberOfShare += sellShares;
                                File.WriteAllText(STOCKLIST, JsonConvert.SerializeObject(stockList));
                                Console.WriteLine(" Stock is selled successfully");
                                checkSymbol = true;
                                break;
                            }
                            else
                            {
                                checkSymbol = true;
                                Console.WriteLine("Company does not contains this number of share :" + sellShares);
                                break;
                            }

                        }
                    }
                    checkStock = true;
                }
            }
            if (checkSymbol == false)
                Console.WriteLine("Company does not contains this Symbol");
            if (checkStock == false)
                Console.WriteLine("This stock are not available");
        }
        public void Buy(double amount, string symbol)
        {
            double noOfShare;
            string shareName;
            bool checkBuy = false, checkSharename = false;
            StockPortfolio stockPortfolio = new StockPortfolio();
            stockPortfolio.ShowAllStock(STOCKLIST);
            List<StockModel> stockList = JsonConvert.DeserializeObject<List<StockModel>>(File.ReadAllText(STOCKLIST));
            Console.WriteLine("Enter the name of share  to buy");
            shareName = Console.ReadLine();
            foreach (StockModel allStocks in stockList)
            {
                if (allStocks.StockName.Equals(shareName))
                {
                    noOfShare = amount / allStocks.SharePrice;
                    if (allStocks.NumberOfShare >= noOfShare)
                    {
                        Console.WriteLine("Share buy=" + noOfShare);
                        allStocks.NumberOfShare = allStocks.NumberOfShare - noOfShare;
                        Console.WriteLine(noOfShare + "Share Buy is successfully");
                        File.WriteAllText(STOCKLIST, JsonConvert.SerializeObject(stockList));
                        List<ModelClass> customerList = JsonConvert.DeserializeObject<List<ModelClass>>(File.ReadAllText(COMPANY));
                        foreach (ModelClass company in customerList)
                        {
                            if (company.Symbol.Equals(symbol))
                            {
                                DateTime dateTime = DateTime.Now;
                                company.NumberOfShares = company.NumberOfShares + noOfShare;
                                company.Amount = company.Amount - amount;
                                company.DateTime = dateTime;
                                File.WriteAllText(COMPANY, JsonConvert.SerializeObject(customerList));
                                Console.WriteLine("Company Info  updated..");
                                break;
                            }
                        }
                        checkBuy = true;
                    }
                    checkSharename = true;
                    break;
                }
            }
            if (checkSharename == false)
                Console.WriteLine(shareName + " Share NA");
            if (checkBuy == false)
                Console.WriteLine("This amount of shares not available");
        }
        public void PrintReport()
        {
            Console.WriteLine("Detail Report of stock are listed below :");
            StockPortfolio stockPortfolio = new StockPortfolio();
            stockPortfolio.ShowAllStock(STOCKLIST);
        }

    }
}
