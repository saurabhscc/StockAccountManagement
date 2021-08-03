using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StockAccountManagement.StockManagement
{
    class StockPortfolio
    {
        public void ShowAllStock(string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    string allStockData = File.ReadAllText(filepath);
                    List<StockModel> stockJsonData = JsonConvert.DeserializeObject<List<StockModel>>(allStockData);
                    Console.WriteLine("StockName\tNumberofStock\tStockPrice\tStocksValue");
                    foreach (var stock in stockJsonData)
                    {
                        Console.WriteLine(stock.StockName + "\t\t" + stock.NumberOfShare + "\t\t" + stock.SharePrice + "\t\t" + (stock.NumberOfShare * stock.SharePrice));
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
    }
}
