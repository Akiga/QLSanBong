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
        public Bill(int id, string name, int phone, float price, string dateCheckIn, string dateCheckOut, int idStadium, DateTime? checkOut1, DateTime? checkIn1)
        {
            this.ID = id;
            this.Name = name;
            this.Phone = phone;
            this.Price = price;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.IdStadium = idStadium;
            this.CheckOut1 = checkOut1;
            this.CheckIn1 = checkIn1;
        }


        public Bill(DataRow row)
        {
            this.ID = (int)row["ID"];
            this.Name = row["CustomerName"].ToString();
            this.Phone = (int)row["CustomerPhone"];
            this.Price = (float)Convert.ToDouble(row["Price"].ToString());
            this.DateCheckIn = row["CheckIn"].ToString();
            this.DateCheckOut = row["CheckOut"].ToString();
            this.IdStadium = (int)row["idStadium"];
            this.CheckIn1 = (DateTime?)row["dateCheckIn"];

            var dateCheckOutTemp = row["dateCheckOut"];
            if (dateCheckOutTemp.ToString() != "")
                this.CheckOut1 = (DateTime?)dateCheckOutTemp;
        }

        private int idStadium;

        private DateTime? CheckOut;

        private DateTime? CheckIn;

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
        public DateTime? CheckOut1 { get => CheckOut; set => CheckOut = value; }
        public DateTime? CheckIn1 { get => CheckIn; set => CheckIn = value; }
    }
}
