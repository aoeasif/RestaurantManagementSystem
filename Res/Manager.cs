using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Res
{
    class Manager : Employee
    {

        private bool isStaff(string Role)
        {
            if (Role.Equals("Staff"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool isDuplicateEmp(Employee emp)
        {
            int count = 0;
            count = new DataAccess().CountExistingRows($"SELECT count(id) FROM Employee WHERE Email = '{emp.Email}' OR Mobile = '{emp.Mobile}'");
            if (count > 0)
            {
                return true;
            }
            return false;
        }


        public void CreateStaff(Employee emp)
        {
            if (isStaff(emp.Role) && !isDuplicateEmp(emp))
            {
                new DataAccess().ExecuteNonQuery("insert into Employee values ('" + emp.Name + "','" + emp.Role + "'," + emp.Salary + ",'" + emp.Email + "','" + emp.Mobile + "')");
            }
        }

        public void UpdateStaff(Employee emp)
        {
            if (isStaff(emp.Role))
            {
                new DataAccess().ExecuteNonQuery("Update Employee Set Name = '" + emp.Name + "', Role = '" + emp.Role + "', Salary = '" + emp.Salary + "', Email = '" + emp.Email + "', Mobile = '" + emp.Mobile + "' where id = '" + emp.Id + "'");
            }
        }

        public void DeleteStaff(Employee emp)
        {
            if (isStaff(emp.Role))
            {
                new DataAccess().ExecuteNonQuery("Delete From Employee Where id = '" + emp.Id + "'");
            }
        }

        public List<Employee> GetAllEmployee()
        {
            SqlDataReader reader = new DataAccess().ExecuteQuery("SELECT * FROM Employee WHERE Role = 'Staff'");
            List<Employee> empList = new List<Employee>();
            while (reader.Read())
            {
                Employee employee = new Employee();
                employee.Id = reader.GetInt32(0);
                employee.Name = reader.GetString(1);
                employee.Role = reader.GetString(2);
                employee.Salary = reader.GetDouble(3);
                employee.Email = reader.GetString(4);
                employee.Mobile = reader.GetString(5);
                empList.Add(employee);
            }
            return empList;
        }

    }
}
