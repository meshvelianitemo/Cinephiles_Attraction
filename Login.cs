using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Login_btn_Click(object sender, EventArgs e)
        {
            SQL_queries sql = new SQL_queries();
            bool success = sql.CheckAuthentification(Username_txtb.Text, Password_txtb.Text, Error_label);
            if (success)
            {
                Home_Page homePage = new Home_Page(); // your main form
                homePage.Show();                      // show main form
                this.Hide();                          // hide login form
            }

        }
    }
}
