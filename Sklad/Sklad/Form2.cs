using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Sklad
{
    public partial class Sklad : Form
    {
        public Sklad()
        {
            string name = "";
            bool prov = true;
            InitializeComponent();
            string connectionString = "server=localhost;user=root;database=Sklad;password=0000;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string sql = "SELECT `N_nakl`,`id_product`,`kol`,`stelash`,`Name`,`U_o_m`,`Price` FROM `warehouse` LEFT JOIN  `products` USING(`id_product`)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //if (reader[0].ToString() == reader[1].ToString())
                //    name = reader[1].ToString();
                //else
                //{
                //    
                //   
                //}
                if (reader[2].ToString() != "0")
                {
                    richTextBox1.Text += reader[4].ToString() + " содержится в колличестве " + reader[2].ToString() + " " + reader[5].ToString() + " на " + reader[3].ToString() + " стелаже.\n";
                    prov = false;
                }
            }
            if (prov)
                MessageBox.Show("Нет совпадений!");
            reader.Close();
            connection.Close();
        }
    }
}
