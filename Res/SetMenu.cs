using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res
{
    class SetMenu : Inventory
    {
        public List<Inventory> SetMenuItemList { get; set; }

        public void LoadSetMenuItems()
        {
            SqlDataReader reader = new DataAccess().ExecuteQuery($"SELECT * FROM Inventory WHERE Id = (SELECT InventoryId FROM SetMenuItem WHERE SetMenuId = {this.Id})");
            while (reader.Read())
            {
                SetMenuItemList.Add(new Inventory()
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

        public void CreateSetMenu()
        {
            foreach (var inv in SetMenuItemList)
            {
                new SetMenuItem() { SetMenuId = this.Id, InventoryId = inv.Id }.AddSetMenuItemId();
            }
        }

        public void UpdateSetMenu(Inventory oldInv, Inventory newInv)
        {
            SetMenuItemList.Remove(oldInv);
            SetMenuItemList.Add(newInv);
            new SetMenuItem() { SetMenuId = this.Id, InventoryId = oldInv.Id }.DeleteSetMenuItemId();
        }

        public void DeleteSetMenu()
        {
            new DataAccess().ExecuteNonQuery($"DELETE FROM SetMenuItem WHERE SetMenuId = {this.Id}");
            new DataAccess().ExecuteNonQuery($"DELETE FROM Inventory WHERE Id = {this.Id}");
        }
    }
}
