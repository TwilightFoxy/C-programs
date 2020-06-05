using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Sklad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            money_update_v2_0(); 
        }
        public void money_update() 
        {
            string connectionString = "server=localhost;user=root;database=Sklad;password=0000;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            int money = 0;

            string sql = "SELECT `money` FROM `buyers` WHERE (`company` = 'Наша компания')";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                money = Convert.ToInt32(reader[0].ToString());
            }
            reader.Close();
            label1.Text = "На вашем счету: " + money;
        }
        public void money_update_v2_0()
        {
            int money = 0;
            string connectionString = "server=localhost;user=root;database=Sklad;password=0000;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string sql = "SELECT `summ_sell`,`money`,`company` FROM `buyers` ";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            string sql01 = "";
            while (reader.Read())
            {
                if (reader[2].ToString() == "Наша компания")
                {
                    sql01 += "UPDATE `sklad`.`buyers` SET `summ_sell` = '0' WHERE (`company` = '" + reader[2].ToString() + "');";
                    money = money - Convert.ToInt32(reader[0].ToString()) + Convert.ToInt32(reader[1].ToString());
                }
                else
                {
                    money += Convert.ToInt32(reader[0].ToString());
                    sql01 += "UPDATE `sklad`.`buyers` SET `summ_sell` = '0' WHERE (`company` = '" + reader[2].ToString() + "');";
                }
            }
            reader.Close();

            MySqlCommand command01 = new MySqlCommand(sql01, connection);
            command01.ExecuteNonQuery();

            string sql2 = "UPDATE `sklad`.`buyers` SET `money` = '" + money + "' WHERE (`company` = 'Наша компания')";
            MySqlCommand command2 = new MySqlCommand(sql2, connection);
            command2.ExecuteNonQuery();
            money_update();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Sklad sk = new Sklad();
            sk.ShowDialog();
            money_update_v2_0();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            add_new_pr pr = new add_new_pr();
            pr.ShowDialog();
            money_update_v2_0();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add_buyer ad = new add_buyer();
            ad.ShowDialog();
            money_update_v2_0();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            add_to_sklad add = new add_to_sklad();
            add.ShowDialog();
            money_update_v2_0();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user=root;database=Sklad;password=0000;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            int money = 0;

            string sql = "SELECT `money` FROM `buyers` WHERE (`company` = 'Наша компания')";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                money = Convert.ToInt32(reader[0].ToString());
            }
            reader.Close();
            string sql2 = "UPDATE `sklad`.`buyers` SET `money` = '" + (money + 50000) + "' WHERE (`company` = 'Наша компания')";
            MySqlCommand command2 = new MySqlCommand(sql2, connection);
            command2.ExecuteNonQuery();
            money_update_v2_0();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user=root;database=Sklad;password=0000;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            int money = 0;
            string sql2 = "UPDATE `sklad`.`buyers` SET `money` = '" + (money + 50000) + "' WHERE (`company` = 'Наша компания')";
            MySqlCommand command2 = new MySqlCommand(sql2, connection);
            command2.ExecuteNonQuery();
            money_update_v2_0();
        }
    }
}
