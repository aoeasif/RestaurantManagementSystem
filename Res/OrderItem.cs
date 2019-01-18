using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res
{
    class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public double TotalSellPrice { get; set; }
        public double TotalCost { get; set; }
        public List<Inventory> ItemList { get; set; }

        public OrderItem(int orderId, List<Inventory> itemList)
        {
            this.OrderId = orderId;
            this.ItemList = itemList;
            totalSellPriceAmount();
            totalCostAmount();
        }
        public OrderItem(int orderId)
        {
            this.ItemList = new List<Inventory>();
            this.OrderId = orderId;
        }

        public void GetAllItem()
        {
            SqlDataReader reader1 = new DataAccess().ExecuteQuery($"SELECT InventoryId FROM OrderItem WHERE OrderId = {OrderId}");
            while(reader1.Read())
            {
                SqlDataReader reader = new DataAccess().ExecuteQuery($"SELECT * FROM Inventory WHERE Id = ({reader1["InventoryId"]})");
                while (reader.Read())
                {
                    ItemList.Add(new Inventory()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                        Cost = reader.GetDouble(3),
                        SellPrice = reader.GetDouble(4),
                        isSetMenu = reader.GetInt32(5)
                    });

                }
            }
            
        } 

        public void AddOrderedItemsToDatabase()
        {
            foreach (var item in ItemList)
            {
                string orderType = "Single Item";
                if (item.isSetMenu == 1)
                {
                    orderType = "Set Menu";
                }
                new DataAccess().ExecuteNonQuery($"INSERT INTO OrderItem VALUES('{OrderId}', '{item.Quantity}', '{orderType}', '{item.Id}')");
            }
        }

        public void totalSellPriceAmount()
        {
            foreach (var item in ItemList)
            {
                TotalSellPrice += item.SellPrice * item.Quantity;
            }
        }

        public void totalCostAmount()
        {
            foreach (var item in ItemList)
            {
                TotalCost += item.Cost * item.Quantity;
            }
        }
    }
}
