using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res
{
    class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int EmpId { get; set; }

        private bool isDuplicate()
        {
            return false;
        }

        public bool GetAuthentication()
        {
            int count = 0;
            count = new DataAccess().CountExistingRows($"SELECT COUNT(id) FROM UserLogin WHERE Email = '{Email}' AND Password = '{Password}'");
            if (count == 1)
            {

                return true;
            }
            return false;
        }

        public string GetEmpRoleByUser()
        {
            string role = "";
            SqlDataReader reader = new DataAccess().ExecuteQuery($"SELECT e.Role FROM Employee AS e WHERE e.id = (SELECT u.EmpId FROM UserLogin AS u WHERE u.Email = '{Email}')");
            while(reader.Read()) role = reader.GetString(0);
            return role;
        }

        public Employee GetEmp()
        {
            Employee emp = new Employee();
            SqlDataReader reader = new DataAccess().ExecuteQuery($"SELECT * FROM Employee AS e WHERE e.id = (SELECT u.EmpId FROM UserLogin AS u WHERE u.Email = '{Email}')");
            while (reader.Read())
            {
                emp.Id = reader.GetInt32(0);
                emp.Name = reader.GetString(1);
                emp.Role = reader.GetString(2);
                emp.Salary = reader.GetDouble(3);
                emp.Email = reader.GetString(4);
                emp.Mobile = reader.GetString(5);
            }
            return emp;

        }

        public void AddUserToDatabase()
        {
            new DataAccess().ExecuteNonQuery($"INSERT INTO User VALUES ('{Email}', '{Password}', {EmpId})");
        }
    }
}
