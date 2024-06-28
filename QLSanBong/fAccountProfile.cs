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
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;
                ChangeAccount(loginAccount);
            }
        }
        public fAccountProfile(Account acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }

        void ChangeAccount(Account acc)
        {
            txtUserName.Text = LoginAccount.UserName;
            txtName.Text = LoginAccount.DisplayName;
        }

        void UpdateAccountInfo()
        {
            string displayName = txtName.Text;
            string password = txtPassWord.Text;
            string newPassword = txtNewPass.Text;
            string reenterPass = txtReEnter.Text;
            string userName = txtUserName.Text;

            if (!newPassword.Equals(reenterPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, displayName, password, newPassword))
                {
                    MessageBox.Show("Cập nhật thành công");
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khẩu");
                }
            }
        }

            private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }
    }
}
