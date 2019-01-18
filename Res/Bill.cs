using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res
{
    class Bill
    {
        public Order order;
        public double Tax { get; set; }
        public string PaymentMethode { get; set; }
        public double TotalAmountPaid { get; set; }

        public void GenerateBill(Order order)
        {
            this.order = order;
            PaymentMethode = "onCash";
            TotalAmountPaid = order.GetTotalSellPrice();
            Tax = TotalAmountPaid * 0.5;
            
        }

        public void SaveBillToDatabase()
        {
            new DataAccess().ExecuteNonQuery($"INSERT INTO Bill VALUES ({order.Id}, {Tax}, '{PaymentMethode}', {TotalAmountPaid})");
        }
    }
}
