using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



namespace IMS
{
    class DBConnection
    {
        public SqlConnection GetConnection()
        {
            // Create a new SqlConnection object
            SqlConnection con = new SqlConnection("server=DESKTOP-8N9FM70\\AHMEDOSMAN;database=IMS;integrated security=true");

            // Return the SqlConnection object
            return con;
        }
    }
}
