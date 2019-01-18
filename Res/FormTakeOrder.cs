using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Res
{
    public partial class FormTakeOrder : Form
    {
        private Employee employee;
        public FormTakeOrder(Employee emp)
        {
            InitializeComponent();
            this.employee = new Employee();
            this.employee = emp;
            label7.Text = employee.Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Staffs sf = new Staffs(employee);
            sf.Show(this);
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Owner.Dispose();
            this.Dispose();
        }

        private void FormTakeOrder_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int orderId = (int)dataGridView1[1, e.RowIndex].Value;
            string custName = (string)dataGridView1[2, e.RowIndex].Value;
            DateTime time = (DateTime)dataGridView1[3, e.RowIndex].Value;
            Order order = new Order()
            {
                Id = orderId,
                CustName = custName,
                TimeStamp = time                
            };
            order.OrderItem = new OrderItem(orderId);
            order.OrderItem.GetAllItem();
            order.ItemList = order.OrderItem.ItemList;
            order.OrderItem.totalSellPriceAmount();
            order.OrderItem.totalCostAmount();
            Bill bill = new Bill();
            bill.GenerateBill(order);
            bill.SaveBillToDatabase();
            MessageBox.Show(bill.TotalAmountPaid + " tk, Payment Successful.");
            SellLog sLog = new SellLog()
            {
                TimeStamp = order.TimeStamp,
                SellPrice = order.OrderItem.TotalSellPrice,
                TotalCost = order.OrderItem.TotalCost
            }; 
            order.DeleteOrder();
            LoadDataGridView();

        }

        private void LoadDataGridView()
        {
            List<Order> orderList = new List<Order>();
            orderList = new Order().GetAllOrder();
            dataGridView1.DataSource = orderList;
            dataGridView1.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Dispose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadDataGridView();
        }
    }
}
