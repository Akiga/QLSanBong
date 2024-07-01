using QLSanBong.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace QLSanBong.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        AccountDAO() { }

        public bool Login(string username, string password)
        {
            string query = "USP_Login @username , @passWord";
            DataTable result = dataProvider.Instance.ExecuteQuery(query, new object[] { username, password });

            return result.Rows.Count > 0;
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data = dataProvider.Instance.ExecuteQuery("select * from Account where userName= '" + userName + "'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        public DataTable GetListAccount()
        {
            return dataProvider.Instance.ExecuteQuery("select UserName, DisplayName, type from Account");
        }

        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = dataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword ", new object[] { userName, displayName, pass, newPass });

            return result > 0;
        }

        public bool insertAccount(string userName, string displayName, int type)
        {
            string query = string.Format("insert Account (Username , DisplayName , type) values ('{0}' , '{1}' , {2})", userName , displayName, type);

            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool updateAccount(string userName, string displayName, int type)
        {
            string query = string.Format("Update Account set Displayname = N'{1}' , type = {2} where userName = N'{0}'", userName, displayName, type);

            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool deleteAccount(string name)
        {
            string query = string.Format("delete Account where Username = N'{0}'",name);

            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool resetPass(string name)
        {
            string query = string.Format("update Account set password = N'0' where Username = N'{0}'", name);

            int result = dataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
