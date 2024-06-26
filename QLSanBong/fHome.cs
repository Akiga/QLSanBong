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
    

        public fHome()
        {

            InitializeComponent();
            LoadStadiumIntoComboBox(cbStadium);
            LoadList();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile();
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
            dtgvBill.DataSource = CustomerDAO.Instance.GetList();
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

    }
}
