﻿using System;
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
    public partial class FormManager : Form
    {
        private Employee employee;
        public FormManager(Employee emp)
        {
            this.employee = emp;
            InitializeComponent();
        }
    }
}
