using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSanBong.DTO
{
    public class Stadium
    {
        public Stadium(int id, string name)
        {
            this.iD = id;
            this.name = name;
        }

        public Stadium(DataRow row)
        {
            this.ID = (int)row["id"];
            this.name = row["Ten"].ToString();
        }

        private int iD;
        public int ID
        {
            get { return iD; }
            set => iD = value;
        }

        public string Name { get => name; set => name = value; }

        private string name;
    }
}
