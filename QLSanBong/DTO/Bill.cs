using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSanBong.DTO
{
    public class Bill
    {
        public Bill(int id, string name, int phone, float price, string dateCheckIn, string dateCheckOut, int idStadium)
        {
            this.ID = id;
            this.Name = name;
            this.Phone = phone;
            this.Price = price;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.IdStadium = idStadium;
        }


        public Bill(DataRow row)
        {
            this.ID = (int)row["ID"];
            this.Name = row["CustomerName"].ToString();
            this.Phone = (int)row["CustomerPhone"];
            this.Price = (float)Convert.ToDouble(row["Price"].ToString());
            this.DateCheckIn = row["DateCheckIn"].ToString();
            this.DateCheckOut = row["DateCheckOut"].ToString();
            this.IdStadium = (int)row["idStadium"];
        }

        private int idStadium;

        private float price;

        private int iD;

        private string name;

        private int phone;

        private string dateCheckOut;

        private string dateCheckIn;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public int Phone { get => phone; set => phone = value; }
        public float Price { get => price; set => price = value; }
        public string DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public string DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public int IdStadium { get => idStadium; set => idStadium = value; }
    }
}
