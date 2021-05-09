using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Manager_DPStuff
{
    public class DataClass
    {
        public SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=DPStuff;Integrated Security=True");

        public DataSet getData(string query)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                con.Open();
                da.Fill(ds);
                con.Close();
                return ds;
            }
            catch { return ds; }
            finally { con.Close(); }
        }

        public void executeQuery(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally { con.Close(); }
        }

        public List<string> GetListItems(string query)
        {
            List<string> listItems = new List<string>();
            foreach (DataRow i in getData(query).Tables[0].Rows)
            {
                listItems.Add(i.ItemArray[0].ToString());
            }
            return listItems;
        }
    }
}
