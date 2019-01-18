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
    public partial class FormAdmin : Form
    {
        private Employee employee;
        public FormAdmin(Employee emp)
        {
            this.employee = emp;

            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            FormAddSetMenu menu = new FormAddSetMenu();
            menu.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAddingStaffInfo addStaff = new FormAddingStaffInfo();
            addStaff.ShowDialog();
        }
    }
}
