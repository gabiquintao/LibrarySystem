using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LibrarySystem.WinForms.Forms.Users
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
        }

        public string UserName => txtName.Text;

        private void AddUserForm_Load(object sender, EventArgs e)
        {

        }
    }
}
