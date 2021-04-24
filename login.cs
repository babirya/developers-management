using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace developers
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        ado d = new ado(); 
        private void button1_Click(object sender, EventArgs e)
        {
            bool tr = false; 
            d.cmd.CommandText = "select id,name from developers ";
            d.conecter();
            d.cmd.Connection = d.con;
            d.dr = d.cmd.ExecuteReader(); 
            while(d.dr.Read())
            {
                if(  textBox1.Text.Equals(d.dr[1].ToString()) && textBox2.Text.Equals(d.dr[0].ToString()) )
                {
                    tr = true;
                    break;
                }
            } 
            if(tr==true)
            {
                this.Hide();
                Form1 f1 = new Form1();
                f1.Show();
            }
            else
            {
                MessageBox.Show("nom de utilisateur ou mot de pass ne pas correct ");
            }
            d.dr.Close(); // we must stop it pour ne break pas vous connection 
        }
    }
}
