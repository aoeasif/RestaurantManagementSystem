using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res
{
    class DataAccess
    {
        private const string connectionString = @"Data Source=DESKTOP-6VQ4QTE\SQLEXPRESS;Initial Catalog=RestuarentManagementSystem;Integrated Security=True";

        public SqlDataReader ExecuteQuery(string sqlCommand)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand query = new SqlCommand(sqlCommand, connection);
            if (connection.State != ConnectionState.Open) connection.Open();
            return query.ExecuteReader();

        }

        public int ExecuteNonQuery(string sqlCommand)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using (SqlCommand query = new SqlCommand(sqlCommand, connection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    return query.ExecuteNonQuery();
                }
            }
        }

        public int CountExistingRows(string sqlCommand)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand query = new SqlCommand(sqlCommand, connection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    count = (int)query.ExecuteScalar();
                }
            }
            return count;
        }

    }
}

