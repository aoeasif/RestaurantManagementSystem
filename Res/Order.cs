using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res
{
    class Order
    {
        public int Id { get; set; }
        public string CustName { get; set; }
        public DateTime TimeStamp { get; set; }
        public OrderItem OrderItem { get; set; }
        public List<Inventory> ItemList { get; set; }

        public void TakeOrder(string custName, List<Inventory> itemList)
        {
            this.CustName = custName;
            this.ItemList = itemList;
            this.TimeStamp = DateTime.Today;
        }

        public void TakeOrder(List<Inventory> itemList)
        {
            this.ItemList = itemList;
            this.TimeStamp = DateTime.Today;
        }

        public void SaveOrderToDatabase()
        {
            new DataAccess().ExecuteNonQuery($"INSERT INTO OrderLog VALUES('{CustName}', '{TimeStamp}')");
            if (this.Id == 0)
            {
                SqlDataReader reader = new DataAccess().ExecuteQuery($"SELECT id FROM OrderLog WHERE CustName = '{CustName}' AND Date = '{TimeStamp}'");
                while (reader.Read()) this.Id = reader.GetInt32(0);
            }
            OrderItem = new OrderItem(this.Id, ItemList);
            OrderItem.AddOrderedItemsToDatabase();
        }

        public List<Order> GetAllOrder()
        {
            List<Order> orderList = new List<Order>();
            SqlDataReader reader = new DataAccess().ExecuteQuery($"SELECT * FROM OrderLog");
            while(reader.Read())
            {
                Order order = new Order()
                {
                    Id = reader.GetInt32(0),
                    CustName = reader.GetString(1),
                    TimeStamp = reader.GetDateTime(2)
                };
                orderList.Add(order);

            }
            return orderList;
        }
        public double GetTotalSellPrice()
        {
            return OrderItem.TotalSellPrice;
        }

        public double CalculateTax()
        {
            return OrderItem.TotalSellPrice * 0.5;
        }

        public double GetTotalCost()
        {
            return OrderItem.TotalCost;
        }

        public void DeleteOrder()
        {
            SellLog sellLog = new SellLog()
            {
                TimeStamp = this.TimeStamp,
                SellPrice = GetTotalSellPrice(),
                Tax = CalculateTax(),
                TotalCost = GetTotalCost()

            };
            sellLog.InsertIntoSellLog();            
            new DataAccess().ExecuteNonQuery($"DELETE FROM OrderItem WHERE OrderId = {Id}");
            new DataAccess().ExecuteNonQuery($"DELETE FROM OrderLog WHERE Id = {Id}");
        }

        
    }
}
