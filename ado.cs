using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Data; 
using System.Data.SqlClient; 

namespace developers
{
    class ado
    { 
        // 1 make global variables here 
        public SqlConnection con = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();
        public SqlDataReader dr;
        public DataTable dt = new DataTable(); 

        // 2 methode connecter 
         public void conecter ()
        {
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
            {
                con.ConnectionString = @"Data Source=DESKTOP-3GHSQJ4\SQLEXPRESS;Initial Catalog=devtech;Integrated Security=True";
                con.Open(); 
            }
        }
        // 3 methode deconnecter  
        public void deconecter ()
         {

            if(con.State==ConnectionState.Open)
            {
                con.Close(); 
            }
         }

       
    }
}
