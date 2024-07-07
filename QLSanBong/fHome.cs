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
            txtID.DataBindings.Add(new Binding("Text", dtgvBill.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtName.DataBindings.Add(new Binding("Text", dtgvBill.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtPhone.DataBindings.Add(new Binding("Text", dtgvBill.DataSource, "Phone", true, DataSourceUpdateMode.Never));
            txtTimeS.DataBindings.Add(new Binding("Text",dtgvBill.DataSource, "DateCheckIn", true, DataSourceUpdateMode.Never));
            txtTimeE.DataBindings.Add(new Binding("Text", dtgvBill.DataSource, "DateCheckOut", true, DataSourceUpdateMode.Never));
            txtPrice.DataBindings.Add(new Binding("Text", dtgvBill.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            float price = (Convert.ToInt32(txtTimeE.Text) - Convert.ToInt32(txtTimeS.Text)) * 100;
            int phone;
            string checkIn = txtTimeS.Text;
            string checkOut = txtTimeE.Text;
            string stadium = cbStadium.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(checkIn) || string.IsNullOrWhiteSpace(checkOut) || string.IsNullOrWhiteSpace(stadium))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

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
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvBill.SelectedCells.Count > 0)
            {
                if (dtgvBill.SelectedCells.Count > 0)
                {
                    var selectedCell = dtgvBill.SelectedCells[0];
                    var owningRow = selectedCell.OwningRow;
                    if (owningRow.Cells["idStadium"].Value != null)
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

            }
        }

        private void txtEdit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            float price = (Convert.ToInt32(txtTimeE.Text) - Convert.ToInt32(txtTimeS.Text)) *100;
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
            int id;
            if (int.TryParse(txtID.Text, out id))
            {
                // Gọi phương thức insertCus từ CustomerDAO
                if (CustomerDAO.Instance.checkOut(id))
                {
                    MessageBox.Show("Thanh toán thành công");
                    LoadList(); // Cập nhật danh sách khách hàng sau khi thêm thành công
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thanh toán khách hàng.");
                }
            }
            else
            {
                MessageBox.Show("Có lỗi");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id;

            if(int.TryParse(txtID.Text,out id))
            {
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
            else
            {
                MessageBox.Show("Có lỗi");
            }
            
        }

        List<Customer> Search(int phone)
        {
            List<Customer> customers = CustomerDAO.Instance.SearchPhone(phone);

            return customers;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List.DataSource = Search(Convert.ToInt32(txtSearchName.Text));
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadList();
        }
    }
}
