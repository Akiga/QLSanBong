using QLSanBong.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSanBong.DAO
{
    public class StadiumDAO
    {
        private static StadiumDAO instance;

        public static StadiumDAO Instance
        {
            get { if (instance == null) instance = new StadiumDAO(); return StadiumDAO.instance; }
            private set { StadiumDAO.instance = value; }
        }
        private StadiumDAO() { }

        public List<Stadium> getListStadium()
        {
            List<Stadium> list = new List<Stadium>();

            string query = "select * from Stadium";

            DataTable data = dataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Stadium stadium = new Stadium(item);
                list.Add(stadium);
            }

            return list;
        }

        public Stadium getStadiumById(int id)
        {
            Stadium stadium = null;

            string query = "select * from Stadium where id =" + id;

            DataTable data = dataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                stadium = new Stadium(item);
                return stadium;
            }

            return stadium;
        }
    }
}
