using QLSanBong.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSanBong.DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO instance;

        public static CustomerDAO Instance
        {
            get { if (instance == null) instance = new CustomerDAO(); return CustomerDAO.instance; }
            private set => instance = value;
        }

        private CustomerDAO() { }

        public List<Customer> GetList()
        {
            List<Customer> list = new List<Customer>();


            DataTable data = dataProvider.Instance.ExecuteQuery("select * from customer");

            foreach (DataRow item in data.Rows)
            {
                Customer cus = new Customer(item);
                list.Add(cus);
            }

            return list;
        }




        public bool insertCus(string name, int phone, float price, string checkIn, string checkOut, string stadium)
        {
            int result = dataProvider.Instance.ExecuteNonQuery("exec AddCustomer @CustomerName , @CustomerPhone , @Price , @DateCheckIn , @DateCheckOut , @StadiumName", new object[] {name, phone, price, checkIn, checkOut, stadium});
            return result > 0;
        }

        public bool updateCus(int id,string name, int phone, float price, string checkIn, string checkOut)
        {
            int result = dataProvider.Instance.ExecuteNonQuery("exec UpdateCustomer @CustomerID , @CustomerName , @CustomerPhone , @Price , @DateCheckIn , @DateCheckOut", new object[] {id, name, phone, price, checkIn, checkOut});
            return result > 0;
        }
    }
}
