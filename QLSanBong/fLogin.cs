using QLSanBong.DAO;
using QLSanBong.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSanBong
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            if (Login(userName, password))
            {
                Account loginAccount = AccountDAO.Instance.GetAccountByUserName(userName);
                fHome f = new fHome(loginAccount);
                this.Hide();
                f.ShowDialog();
                this.Show();
                txtPassword.Text = "";
            }
            else
            {
                MessageBox.Show("Sai thông tin");
            }
        }
        bool Login(string username, string password)
        {
            return AccountDAO.Instance.Login(username, password);
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(
                "Bạn có muốn thoát chương trình"
                , "Thông báo", MessageBoxButtons.OKCancel
                , MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
