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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private void textBox3_click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox3.BackColor = Color.White;            
        }

        private void textBox4_click(object sender, EventArgs e)
        {
            textBox4.UseSystemPasswordChar = true;
            textBox4.Text = "";
            textBox4.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                Email = textBox3.Text,
                Password = textBox4.Text            
            };
            if (user.GetAuthentication())
            {
                string role = user.GetEmpRoleByUser();
                Employee emp = user.GetEmp();
                if (role.Equals("Staff"))
                {
                    new FormTakeOrder(emp).Show(this);
                }
                else if(role.Equals("Admin"))
                {
                    new FormAdmin(emp).Show();
                }else if (role.Equals("Manager"))
                {
                    new FormManager(emp).Show(this);
                }
                this.Hide();
            }
            else
            {
                textBox3.BackColor = Color.OrangeRed;
                textBox4.BackColor = Color.OrangeRed;
                lblError.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new DailyLog().GenerateDailyLog();
        }
    }
}
