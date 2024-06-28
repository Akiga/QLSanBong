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

        public bool updateCus(int id,string name, int phone, float price, string checkIn, string checkOut, int idStadium)
        {
            string query = string.Format("Update Customer set CustomerName = N'{0}', CustomerPhone = {1}, price = {2}, DateCheckIn = N'{3}', DateCheckOut = N'{4}', idStadium = {5} where id = {6}", name, phone, price, checkIn, checkOut, idStadium, id);


            int result = dataProvider.Instance.ExecuteNonQuery(query);
            //int result = dataProvider.Instance.ExecuteNonQuery("exec UpdateCustomer @CustomerID , @CustomerName , @CustomerPhone , @Price , @DateCheckIn , @DateCheckOut", new object[] {id, name, phone, price, checkIn, checkOut});
            return result > 0;
        }

        public bool deleteCus(int id)
        {
            string query = string.Format("delete Customer where id = " + id);

            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool checkOut(int id)
        {
            int result = dataProvider.Instance.ExecuteNonQuery("MoveCusToBill @id = " + id);
            return result > 0;
        }
    }
}
