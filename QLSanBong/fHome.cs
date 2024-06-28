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
    public partial class fHome : Form
    {
        private Account loginAccount;

        BindingSource List = new BindingSource();

        public Account LoginAccount 
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;
                ChangeAccount(loginAccount.Type);
            }
        }

        public fHome(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            Load();
        }

        void Load()
        {
            dtgvBill.DataSource = List;

            LoadStadiumIntoComboBox(cbStadium);
            LoadList();
            addBinding();
        }

        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinCáNhânToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);

            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
        }


        void LoadStadiumIntoComboBox(ComboBox cb)
        {
            cb.DataSource = StadiumDAO.Instance.getListStadium();
            cb.DisplayMember = "name";
        }
        void LoadList()
        {
            List.DataSource = CustomerDAO.Instance.GetList();
        }

        void addBinding()
        {
            txtID.DataBindings.Add(new Binding("Text", dtgvBill.DataSource, "ID"));
            txtName.DataBindings.Add(new Binding("Text", dtgvBill.DataSource, "Name"));
            txtPhone.DataBindings.Add(new Binding("Text", dtgvBill.DataSource, "Phone"));
            txtTimeS.DataBindings.Add(new Binding("Text",dtgvBill.DataSource, "DateCheckIn"));
            txtTimeE.DataBindings.Add(new Binding("Text", dtgvBill.DataSource, "DateCheckOut"));
            nmPrice.DataBindings.Add(new Binding("Value", dtgvBill.DataSource, "Price"));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            float price = (float)nmPrice.Value;
            int phone;
            string checkIn = txtTimeS.Text;
            string checkOut = txtTimeE.Text;
            string stadium = cbStadium.Text;
            // Kiểm tra và chuyển đổi số điện thoại
            if (!int.TryParse(txtPhone.Text, out phone))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập một số nguyên.");
                return;
            }

            // Gọi phương thức insertCus từ CustomerDAO
            if (CustomerDAO.Instance.insertCus(name, phone, price, checkIn, checkOut, stadium))
            {
                MessageBox.Show("Thêm Thành Công");
                LoadList(); // Cập nhật danh sách khách hàng sau khi thêm thành công
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm khách hàng.");
            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvBill.SelectedCells.Count > 0)
            {
                int id = (int)dtgvBill.SelectedCells[0].OwningRow.Cells["idStadium"].Value;

                Stadium category = StadiumDAO.Instance.getStadiumById(id);

                cbStadium.SelectedItem = category;

                int index = -1;
                int i = 0;

                foreach (Stadium item in cbStadium.Items)
                {
                    if (item.ID == category.ID)
                    {
                        index = i;
                        break;
                    }
                    i++;

                }
                cbStadium.SelectedIndex = index;
            }
        }

        private void txtEdit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            float price = (float)nmPrice.Value;
            int phone;
            string checkIn = txtTimeS.Text;
            string checkOut = txtTimeE.Text;
            int id = Convert.ToInt32(txtID.Text);
            int idStadium = (cbStadium.SelectedItem as Stadium).ID;
            // Kiểm tra và chuyển đổi số điện thoại
            if (!int.TryParse(txtPhone.Text, out phone))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập một số nguyên.");
                return;
            }

            // Gọi phương thức insertCus từ CustomerDAO
            if (CustomerDAO.Instance.updateCus(id, name, phone, price, checkIn, checkOut, idStadium))
            {
                MessageBox.Show("Cập nhật thành công");
                LoadList(); // Cập nhật danh sách khách hàng sau khi thêm thành công
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật khách hàng.");
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(txtID.Text);

            //// Gọi phương thức insertCus từ CustomerDAO
            //if (CustomerDAO.Instance.checkOut(id))
            //{
            //    MessageBox.Show("Thanh toán thành công");
            //    LoadList(); // Cập nhật danh sách khách hàng sau khi thêm thành công
            //}
            //else
            //{
            //    MessageBox.Show("Có lỗi khi thanh toán khách hàng.");
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text);

            // Gọi phương thức insertCus từ CustomerDAO
            if (CustomerDAO.Instance.deleteCus(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadList(); // Cập nhật danh sách khách hàng sau khi thêm thành công
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa khách hàng.");
            }
        }
    }
}
