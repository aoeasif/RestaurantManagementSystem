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
    public partial class FormQuantity : Form
    {
        public int qnty { get; set; }
        //public delegate void PassBackQuantityHandler(int x);
        //public event PassBackQuantityHandler PassBackQuantity;
        public FormQuantity()
        {
            qnty = 0;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            qnty = Convert.ToInt32(textBox1.Text);
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
        
    }
}
