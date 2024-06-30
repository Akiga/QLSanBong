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

namespace QLSanBong
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            LoadDateTimePickerBill();
            LoadBillListByDate(dtpkFormDate.Value, dtpkToDate.Value);
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
    }
}
