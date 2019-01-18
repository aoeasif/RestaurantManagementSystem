using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res
{
    class SellLog
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public double SellPrice { get; set; }
        public double Tax { get; set; }
        public double TotalCost { get; set; }

        public void TaxCalc()
        {
            Tax = SellPrice * 0.05;
        }

        public List<SellLog> GetAllSellLog()
        {
            SqlDataReader reader = new DataAccess().ExecuteQuery(@"SELECT * FROM SellLog");
            List<SellLog> SellLogList = new List<SellLog>();
            while (reader.Read())
            {
                SellLog selllog = new SellLog();
                selllog.Id = reader.GetInt32(0);
                selllog.TimeStamp = DateTime.Parse(reader.GetString(1));
                selllog.SellPrice = reader.GetDouble(2);
                selllog.Tax = reader.GetDouble(3);
                selllog.TotalCost = reader.GetDouble(4);
                SellLogList.Add(selllog);
            }
            return SellLogList;
        }

        public void InsertIntoSellLog() {
            if(Tax == 0 && SellPrice > 0) TaxCalc();
            new DataAccess().ExecuteNonQuery("Insert Into SellLog Values('" + TimeStamp + "', '" + SellPrice + "', '" + Tax + "', '" + TotalCost + "')");
        }

        public double GetTotalDailyTax(DateTime dt)
        {
            double TotalDailyTax = 0;
            SqlDataReader reader = new DataAccess().ExecuteQuery("Select Tax From SellLog Where Date='" + dt + "'");

            while (reader.Read())
            {
                TotalDailyTax += reader.GetDouble(0);
            }

            return TotalDailyTax;
        }

        public double GetTotalDailySell(DateTime dt)
        {
            double TotalDailySellPrice = 0;
            SqlDataReader reader = new DataAccess().ExecuteQuery("Select SellPrice From SellLog Where Date='" + dt + "'");

            while (reader.Read())
            {
                TotalDailySellPrice += reader.GetDouble(0);
            }

            return TotalDailySellPrice;
        }

        public double GetTotalDailyCost(DateTime dt)
        {
            double TotalDailyCost = 0;
            SqlDataReader reader = new DataAccess().ExecuteQuery("Select TotalCost From SellLog Where Date='" + dt + "'");
            while (reader.Read())
            {
                TotalDailyCost += reader.GetDouble(0);
            }

            return TotalDailyCost;
        }
      





    }










}
