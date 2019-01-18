using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res
{
    class SetMenuItem
    {
        public int SetMenuId { get; set; }
        public int InventoryId { get; set; }

        private bool isDuplicate()
        {
            int count = 0;
            count = new DataAccess().CountExistingRows($"SELECT COUNT(SetMenuId) WHERE InventoryId = {InventoryId} AND SetMenuId = {SetMenuId}");
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        private bool isValidSetMenu()
        {
            int count = 0;
            count += new DataAccess().CountExistingRows($"SELECT COUNT (Id) WHERE Id = {SetMenuId} AND isSetMenu = 1");
            count += new DataAccess().CountExistingRows($"SELECT COUNT (Id) WHERE Id = {InventoryId} AND isSetMenu = 0");
            if (count == 2) return true;
            return false;
        }

        public void AddSetMenuItemId()
        {

            if (!isDuplicate() && isValidSetMenu())
            {
                new DataAccess().ExecuteNonQuery($"INSERT INTO SetMenuItem VALUES ({InventoryId}, {SetMenuId})");
            }
        }

        public void DeleteSetMenuItemId()
        {
            int count = 0;
            count = new DataAccess().CountExistingRows($"SELECT COUNT(SetMenuId) WHERE SetMenuId = {SetMenuId}");
            new DataAccess().ExecuteNonQuery($"DELETE FROM SetMenuItem WHERE InventoryId = {InventoryId} AND SetMenuId = {SetMenuId}");
            if (count == 1)
            {
                new DataAccess().ExecuteNonQuery($"DELETE FROM Inventory WHERE Id = {SetMenuId}");
            }
        }
    }
}
