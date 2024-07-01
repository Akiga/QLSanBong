using QLSanBong.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QLSanBong
{
    public partial class fAdmin : Form
    {
        BindingSource accountList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();

            dtgvAccount.DataSource = accountList; 

            LoadDateTimePickerBill();
            LoadBillListByDate(dtpkFormDate.Value, dtpkToDate.Value);
            LoadAccount();
            addAccountBinding();
        }

        void LoadDateTimePickerBill()
        {
            DateTime dateTime = DateTime.Now;
            dtpkFormDate.Value = new DateTime(dateTime.Year, dateTime.Month, 1);
            dtpkToDate.Value = dtpkFormDate.Value.AddMonths(1).AddDays(-1);
        }

        void LoadBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBill(checkIn, checkOut);
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadBillListByDate(dtpkFormDate.Value, dtpkToDate.Value);
        }

        void addAccountBinding()
        {
            txtUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Username", true, DataSourceUpdateMode.Never));
            txtAccountName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        void AddAccount(string userName, string displayName, int type)
        {
            if(AccountDAO.Instance.insertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }

            LoadAccount();
        }

        void editAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.updateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }

            LoadAccount();
        }

        void deleteAccount(string userName)
        {
            if (AccountDAO.Instance.deleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }

            LoadAccount();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtAccountName.Text;
            int type = (int)nmType.Value;

            AddAccount(userName, displayName, type );
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;

            deleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtAccountName.Text;
            int type = (int)nmType.Value;

            editAccount(userName, displayName, type);
        }

        void resetPass(string name)
        {
            if (AccountDAO.Instance.resetPass(name))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
        }
        private void btnResetPassWord_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;

            resetPass(userName);
        }
    }
}
