using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // input output working with filieds 

namespace developers
{
    public partial class Form1 : Form
    { 
        // 1 calling ado class 

        ado d = new ado();  
        //this global declaration is for nagivation buttons  
        public int fox;  
         // make a precurude for nagivation 
        public void navigation()
        {
            textBox1.Text = d.dt.Rows[fox][0].ToString();
            textBox2.Text = d.dt.Rows[fox][1].ToString();
            textBox3.Text = d.dt.Rows[fox][2].ToString();
            textBox4.Text = d.dt.Rows[fox][3].ToString();

        } 
        // make a precodure for combo box  
          public void remplircombo()
            {
                comboBox1.Items.Clear(); // pour eviter le problem de reputition 
                d.conecter();
                d.cmd.CommandText = "select id from developers ";
                d.cmd.Connection = d.con;
                d.dr = d.cmd.ExecuteReader();
                while(d.dr.Read())
                {
                    comboBox1.Items.Add(d.dr[0]);
                }
                d.dr.Close();
              }
        // 2 methode remplir dataGridView  
        public void remplir ()
        {  
            // this  one for eviter repitation 
            if(d.dt.Rows.Count!=null)
            {
                d.dt.Rows.Clear(); 
            }

            d.conecter();
            d.cmd.CommandText = "select * from developers ";
            d.cmd.Connection = d.con;
            d.dr = d.cmd.ExecuteReader();
            d.dt.Load(d.dr);
            dataGridView1.DataSource = d.dt;
            d.dr.Close(); 


        } 

        // 3 methode vider 
        public void vider (Control f)
        {
              foreach(Control ct in f.Controls)
              {
                  if(ct.GetType()==typeof(TextBox))
                  {
                      ct.Text = ""; 
                  } 
                  
                  // this for sous controls

                    if(ct.Controls.Count!=0)
                    {
                        vider(ct); 
                    }
              }
            

        } 
        
        // 4 methode number 
        public int number ()
        {
            d.conecter();
            d.cmd.CommandText = "select count (id) from developers where id ='"+textBox1.Text+"'";
            d.cmd.Connection = d.con;
            int cpt;  
            cpt= (int) d.cmd.ExecuteScalar();
            return cpt;
        }  
        // 5 methode for ajouter  
        public bool ajouter()
         {
             if (number() == 0)
             {
                 d.cmd.CommandText = "insert into developers values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text +"') ";

                 d.cmd.Connection = d.con;
                 d.cmd.ExecuteNonQuery();
                 
                 
                 return true; 
             }
             return false; 

          }
        // 6 methode for modifier 
        public bool modifier()
        {
             if(number()!=0)
             {
                 if(MessageBox.Show("vuez modifier ce developer ","confirmation",MessageBoxButtons.YesNo)==DialogResult.Yes)
                 {
                     d.cmd.CommandText = "update developers set name='" + textBox2.Text + "',mission= '" + textBox3.Text + "', age='" + textBox4.Text + "' where id='" + textBox1.Text + "'  ";
                     d.cmd.Connection = d.con;
                     d.cmd.ExecuteNonQuery();
                     return true;
                     
                 }
             }
             return false; 
        }

        // 7 methode for suprimmer
        public bool suprimmer()
        {
            if (number() != 0)
            { 
                if(MessageBox.Show("vouez suprimer ce developer ?","comfirmation",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {

               
                d.cmd.CommandText = " delete from developers where id = '"+textBox1.Text+"'";

                d.cmd.Connection = d.con;
                d.cmd.ExecuteNonQuery();


                return true; 
                }
            }
            return false;

        } 

        /// <summary>
        /// ////////////
        /// </summary> 
        /// ///////////////
        /// ///////                 part  2>>>>>>>>>>>> expotrer 
        /// 
            
              //1 >>> exporter via XML 
        DataSet datas = new DataSet();
        void exporterXml()
        {
            d.cmd.CommandText = "select * from developers";
            d.cmd.Connection = d.con;
            d.dr = d.cmd.ExecuteReader();
            datas.Tables.Add("st");
            datas.Tables["st"].Load(d.dr);
            string chemin = "";
            saveFileDialog1.Filter = "XLM FILES |.*xml"; 

            if(saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                chemin = saveFileDialog1.FileName; 
            }
            datas.WriteXml(chemin);
            MessageBox.Show("donne enriginster avec success"); 
        } 
        
         //  2 >>> exporter via text 
        DataTable data = new DataTable(); 
        void exoporterText()
        {
            d.cmd.CommandText = "select *from developers";
            d.cmd.Connection = d.con;
            d.dr = d.cmd.ExecuteReader();
            data.Load(d.dr); 
            if(MessageBox.Show("voulez vous enregister votre donnne ","confirmation",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                try
                {
                    string chemin1 = "";
                    saveFileDialog1.Filter = "Text FILES |.*txt";  
                    if(saveFileDialog1.ShowDialog()==DialogResult.OK)
                    {
                        chemin1 = saveFileDialog1.FileName; 
                    }
                    StreamWriter st = new StreamWriter(chemin1); 
                    for( int i=0; i<data.Rows.Count; i++)
                    {
                        st.WriteLine(data.Rows[i][0].ToString()+ "  "+data.Rows[i][1].ToString()+ "  "+data.Rows[i][2].ToString()+ "  "+data.Rows[i][3].ToString()  );
                    }
                    st.Close();
                    MessageBox.Show("donne enregistre avec success", "confirmation", MessageBoxButtons.OK);

                }catch
                {
                    MessageBox.Show("error d'exportation "); 
                }
            }



        } 

       // 3 exporter via html 
           
        DataTable data1 = new DataTable(); 
        void exporterHtml ()
        {
            d.cmd.CommandText = "select * from developers";
            d.cmd.Connection = d.con;
            d.dr = d.cmd.ExecuteReader();
            data1.Load(d.dr);
          
            try
            {
                string chemin2 = "";
                saveFileDialog1.Filter = "HTML FILES |.*html";  
                if(saveFileDialog1.ShowDialog()==DialogResult.OK)
                {
                    chemin2 = saveFileDialog1.FileName; 
                } 
                 using( StreamWriter sw = new StreamWriter(chemin2))
                 {
                     FileInfo info = new FileInfo(chemin2);
                     StringBuilder sb = new StringBuilder();
                     sb.AppendFormat("<html>");
                     sb.AppendFormat("<head>");
                     sb.AppendFormat("<title> dev</title>");
                     sb.AppendFormat("<meta charest='UTF-8'/>");
                     sb.AppendFormat("</head>");
                     sb.AppendFormat("<body>");
                     sb.AppendFormat("<table border=1 cellspacing=10 cellpadding=10 style='border-collapse :collapse'>");
                     sb.AppendFormat("<thead>");
                     sb.AppendFormat("<tr>");
                     foreach(DataColumn c in data1.Columns)
                     {
                         sb.AppendFormat("<th>{0}</th>", c.ColumnName.ToUpper());
                     }
                     sb.AppendFormat("</tr>");
                     sb.AppendFormat("</thead>");
                     sb.AppendFormat("<tbody>"); 
                      
                     foreach(DataRow r in  data1.Rows)
                     {
                         sb.AppendFormat("<tr>"); 
                         for(int i=0 ; i<data1.Columns.Count; i++)
                         {
                             sb.AppendFormat("<td>{0}</td>", r[i].ToString());
                         }
                         sb.AppendFormat("</tr>"); 
                     }
                     sb.AppendFormat("</tbody>");
                     sb.AppendFormat("</table>");
                     sb.AppendFormat("</body>");
                     sb.AppendFormat("</html>");

                     sw.WriteLine(sb.ToString());
                     MessageBox.Show("donee enregistre avec sucess"); 
                 }
               

            }
            catch
            {
                MessageBox.Show("error d'exportation "); 

            }


        }

        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            remplir();
            remplircombo();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if( MessageBox.Show("vous avez quitter","comfirmation",MessageBoxButtons.YesNo)==DialogResult.Yes) 
            { 
              d.deconecter();
              this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("vous avez vider ", "comfirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                vider(this); 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            // pour ajouter 
            //1 we must make function number with id 
            // 2 control de saisie here 
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
             {
                 MessageBox.Show("s'ill vous plait remplir everything");
                 return; 
             } 
            if(ajouter()==true)
            {
                MessageBox.Show("develope ajouter avec success");
                remplir(); 
            }else
            {
                MessageBox.Show("ce developer est deja exist  ");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        { 
            // for supprimer 
           if(textBox1.Text=="")
           {
               MessageBox.Show("s'ill vous plait enter id ");
               return; 
           } 

            if(suprimmer()==true)
            {

                MessageBox.Show("DEVELOPER SUPPRIMER AVEC SUCCESS");
                remplir();
            }
            else
            {
                MessageBox.Show("cette developers ne pas exist ");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        { 
            // for modifier 
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("s'ill vous plait remplir toutes les champs");
                return;
            }
            if (modifier() == true)
            {
                MessageBox.Show("developer modifier avec success");
                remplir();
            }
            else
            {
                MessageBox.Show("ce developer ne existe pas");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            fox = 0;
            navigation(); 
        }

        private void button9_Click(object sender, EventArgs e)
        {
            fox = d.dt.Rows.Count - 1;
            navigation();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                fox++;
                navigation();

            }catch
            {
                MessageBox.Show("vous etes sur le dernier enregistrement");
                fox--;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                fox--;
                navigation();

            }
            catch
            {
                MessageBox.Show("vous etes sur le premier enregistrement");
                fox++;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            exporterXml();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            exoporterText(); 
        }

        private void button11_Click(object sender, EventArgs e)
        {
            exporterHtml();
        }
    }
}
