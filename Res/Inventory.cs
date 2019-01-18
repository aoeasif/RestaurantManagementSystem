using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res
{
    class Inventory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public int OrderQuantity { get; set; }
        public double Cost { get; set; }
        public double SellPrice { get; set; }
        public int isSetMenu { get; set; }

        private bool isDuplicate()
        {
            int count = 0;
            count = new DataAccess().CountExistingRows($"SELECT count(id) FROM Inventory WHERE Title = '{this.Title}'");
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public void AddInventoryItem()
        {
            if (!isDuplicate())
            {
                new DataAccess().ExecuteNonQuery($"INSERT INTO Inventory VALUES ('{this.Title}', {this.Quantity}, {this.Cost}, {this.SellPrice}, {this.isSetMenu})");
            }
        }

        public void UpdateInventoryItem()
        {
            new DataAccess().ExecuteNonQuery($"UPDATE Inventory SET Title = '{this.Title}', Quantity = {this.Quantity}, Cost = {this.Cost}, SellPrice = {this.SellPrice}, isSetMenu = {this.isSetMenu} WHERE Id = {this.Id}");
        }

        public bool DeleteInventoryItem()
        {
            if (this.isSetMenu == 1) return false;
            new DataAccess().ExecuteNonQuery($"DELETE FROM Inventory WHERE Id = {this.Id}");
            return true;
        }

        public List<Inventory> GetAllItem()
        {
            SqlDataReader reader = new DataAccess().ExecuteQuery("SELECT * FROM Inventory");
            List<Inventory> setMenuList = new List<Inventory>();
            while (reader.Read())
            {
                setMenuList.Add(new Inventory()
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Quantity = reader.GetInt32(2),
                    Cost = reader.GetDouble(3),
                    SellPrice = reader.GetDouble(4),
                    isSetMenu = reader.GetInt32(5)
                });
            }
            return setMenuList;
        }

        public List<Inventory> GetAllSetMenu()
        {
            SqlDataReader reader = new DataAccess().ExecuteQuery("SELECT * FROM Inventory WHERE isSetMenu = 1");
            List<Inventory> setMenuList = new List<Inventory>();
            while (reader.Read())
            {
                setMenuList.Add(new Inventory()
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Quantity = reader.GetInt32(2),
                    Cost = reader.GetDouble(3),
                    SellPrice = reader.GetDouble(4),
                    isSetMenu = reader.GetInt32(5)
                });
            }
            return setMenuList;
        }

    }
}
