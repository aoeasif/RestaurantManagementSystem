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
    public partial class Staffs : Form
    {
        Employee employee;
        List<Inventory> invList;
        Order order;
        List<int> orderedItemId;
        public static int serial = 0;
        public static double totalPrice = 0;
        public Staffs(Employee emp)
        {
            InitializeComponent();
            label7.Text = emp.Name;
            this.employee = emp;
            invList = new Inventory().GetAllItem();
            order = new Order();
            orderedItemId = new List<int>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s =button1.Text;
            PlaceOrder(s);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = button2.Text;
            PlaceOrder(s);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = button3.Text;
            PlaceOrder(s);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Dispose();
        }

        private void Staffs_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Staffs_Load(object sender, EventArgs e)
        {

            LoadItemsForOrder();
            
        }

        private void LoadItemsForOrder()
        {
            itemListBox.Items.Clear();
            foreach (var inv in invList)
            {
                if (inv.isSetMenu == 0 && inv.Quantity > 0)
                {
                    itemListBox.Items.Add(inv.Title + "("+ inv.Quantity +")");
                }
            }
        }

        private void PlaceOrder(string selectedItem)
        {            
           
            Inventory Ordereditem = invList.FirstOrDefault(x => x.Title.Equals(selectedItem));
            FormQuantity q = new FormQuantity();
            var result = q.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                int quantity = 0;
                quantity = q.qnty;
                if (Ordereditem != null && quantity > 0 && Ordereditem.Quantity >= quantity)
                {
                    Ordereditem.OrderQuantity += quantity;
                    Ordereditem.Quantity -= quantity;
                    totalPrice += Ordereditem.SellPrice * Ordereditem.OrderQuantity;
                }
                else
                {
                    MessageBox.Show("Not sufficient product available.");
                    return;
                }
                string[] row = { Convert.ToString(++serial), Ordereditem.Title, Ordereditem.OrderQuantity.ToString() };
                listView1.Items.Add(new ListViewItem(row));
                label13.Text = totalPrice.ToString();
                label14.Text = Convert.ToString(totalPrice * 0.05);
                label8.Text = Convert.ToString(totalPrice + (totalPrice * 0.05));
                Ordereditem = null;
            } 
        }

        private void itemListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = itemListBox.SelectedItem.ToString();
            int len = s.Length - (s.Length - s.IndexOf("("));
            s = s.Substring(0, len);
            PlaceOrder(s);
            LoadItemsForOrder();
            //if (listView1.Items.Count > 0)
            //{
            //    for (int i = listView1.Items.Count - 1; i >= 0; i--)
            //    {
            //        var item = listView1.Items[i];
                   
            //        MessageBox.Show(item.SubItems["ItemName"].ToString());
                    
            //    }
            //}
            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string custName = textBox1.Text;
            List<Inventory> ListOfOrderdItem = new List<Inventory>();
            foreach(var inv in invList)
            {
                if(inv.OrderQuantity > 0)
                {
                    inv.Quantity -= inv.OrderQuantity;
                    inv.UpdateInventoryItem();
                    ListOfOrderdItem.Add(inv);
                }
            }
            Order order = new Order();
            order.TakeOrder(custName, ListOfOrderdItem);
            order.SaveOrderToDatabase();
            this.Owner.Show();
            this.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (var inv in invList)
            {
                if (inv.OrderQuantity > 0)
                {
                    inv.Quantity += inv.OrderQuantity;
                    inv.OrderQuantity = 0;
                }
            }
            listView1.Items.Clear();
            LoadItemsForOrder();
            textBox1.Text = "";
            label13.Text = "";
            label14.Text = "";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string s = button2.Text;
            PlaceOrder(s);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string s = button3.Text;
            PlaceOrder(s);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
