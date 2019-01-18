using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res
{
    class DailyLog
    {
        public int Id { set; get; }
        public DateTime Date { set; get; }
        public Double TotalIncome { set; get; }
        public Double Profit { set; get; }
        public Double TotalTax { set; get; }

        public List<DailyLog> GetAllDailyLog()
        {
            SqlDataReader reader = new DataAccess().ExecuteQuery("SELECT * FROM DailyLog");
            List<DailyLog> DailyLogList = new List<DailyLog>();
            while (reader.Read())
            {
                DailyLog dailylog = new DailyLog();

                dailylog.Id = reader.GetInt32(0);
                dailylog.Date = reader.GetDateTime(1);
                dailylog.TotalIncome = reader.GetDouble(2);
                dailylog.Profit = reader.GetDouble(3);
                dailylog.TotalTax = reader.GetDouble(4);

            }
            return DailyLogList;
        }

        public void GenerateDailyLog()
        {
            Date = DateTime.Today;
            SellLog sl = new SellLog();
            TotalIncome = sl.GetTotalDailySell(Date);
            double TotalCost = sl.GetTotalDailyCost(Date);
            Profit = TotalIncome - TotalCost;
            TotalTax = sl.GetTotalDailyTax(Date);
            int count = new DataAccess().CountExistingRows($"SELECT COUNT(Id) FROM DailyLog WHERE Date = '{Date}'");
            if (count > 0)
            {
                new DataAccess().ExecuteNonQuery($"UPDATE DailyLog SET Date = '{Date}', TotaIIncome = {TotalIncome}, Profit = {Profit}, TaxMoney = {TotalTax}");
            }
            else
            {
                new DataAccess().ExecuteNonQuery($"INSERT INTO DailyLog VALUES ('{Date}', {TotalIncome}, {Profit}, {TotalTax})");
            }
        }


    }
}
