using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res
{
    class Admin : Manager
    {
        public void CreateManager(Employee emp)
        {
            new DataAccess().ExecuteNonQuery("insert into Employee values ('" + emp.Name + "','" + emp.Role + "'," + emp.Salary + ",'" + emp.Email + "','" + emp.Mobile + "')");
        }

        public void UpdateManager(Employee emp)
        {
            new DataAccess().ExecuteNonQuery("Update Employee Set Name = '" + emp.Name + "', Role = '" + emp.Role + "', Salary = '" + emp.Salary + "', Email = '" + emp.Email + "', Mobile = '" + emp.Mobile + "' where id = '" + emp.Id + "'");
        }

        public void DeleteManager(Employee emp)
        {
            SqlDataReader reader = new DataAccess().ExecuteQuery("Select Count(id) From Employee Where Role='Admin'");
            if (reader.GetInt32(0) > 1)
            {
                new DataAccess().ExecuteNonQuery("Delete From Employee Where id = '" + emp.Id + "'");
            }
        }
    }
}
