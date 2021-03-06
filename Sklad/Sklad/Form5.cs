﻿using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Sklad
{
    public partial class add_to_sklad : Form
    {
        int IIDD = -1;
        public add_to_sklad()
        {
            InitializeComponent();
            string connectionString = "server=localhost;user=root;database=Sklad;password=0000;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string sql = "SELECT `Name` FROM `products`";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
            }
            reader.Close();

            string sql2 = "SELECT `company` FROM `buyers`";
            command = new MySqlCommand(sql2, connection);
            MySqlDataReader reader2 = command.ExecuteReader();
            while (reader2.Read())
            {
                comboBox2.Items.Add(reader2[0].ToString());
                comboBox3.Items.Add(reader2[0].ToString());
            }
            reader2.Close();

            string sql1 = "SELECT MAX(`N_nakl`) AS `N_nakl` FROM `nakladnaya`";
            MySqlCommand command1 = new MySqlCommand(sql1, connection);
            MySqlDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                if (reader1[0].ToString() != "")
                {
                    IIDD = Convert.ToInt32(reader1[0].ToString()) + 1;
                }
                else
                    IIDD = 0;
            }
            reader1.Close();
            if (IIDD ==9||IIDD>9)
            {
                string sql_del = "DELETE FROM `sklad`.`nakladnaya`";
                MySqlCommand command2 = new MySqlCommand(sql_del, connection);
                command2.ExecuteNonQuery();

            }
            connection.Close();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() != "Наша компания")
                comboBox3.SelectedItem = "Наша компания";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int new_sum = 0;
            double kol_buy = 0;
            int skidka = 0;
            int cost = 0;
            if (comboBox2.SelectedItem.ToString() != "Наша компания" && comboBox2.SelectedIndex > -1)
            {
                string connectionString = "server=localhost;user=root;database=Sklad;password=0000;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string sql = "SELECT `company`,`kol_buy`,`summ_sell` FROM `buyers`";
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[0].ToString() == comboBox3.SelectedItem.ToString())
                    {
                        kol_buy = Convert.ToInt32(reader[1].ToString());
                        new_sum = Convert.ToInt32(reader[2].ToString());
                    }
                }
                reader.Close();
                connection.Close();
            }

            if (comboBox1.SelectedIndex > -1 && comboBox2.SelectedIndex > -1 && comboBox3.SelectedIndex > -1)
            {
                if (comboBox2.SelectedItem.ToString() == comboBox3.SelectedItem.ToString())
                    MessageBox.Show("Нельзя выбирать одинаковые компании!", "Ошибка!");
                else if (comboBox2.SelectedItem.ToString() != "Наша компания" && comboBox3.SelectedItem.ToString() != "Наша компания")
                    MessageBox.Show("Вы не можете отвечать за другие компании!", "Ошибка!");
                else
                {
                    int kol = Convert.ToInt32(textBox1.Text);
                    if (kol_buy != 0)
                        skidka = Convert.ToInt32(Math.Round(kol_buy * 0.05));
                    string ss = "";
                    string connectionString = "server=localhost;user=root;database=Sklad;password=0000;";
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();
                    string sql = "SELECT `id_product`,`Name`,`Price` FROM `products`";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader[1].ToString() == comboBox1.SelectedItem.ToString())
                        {
                            cost = Convert.ToInt32(reader[2].ToString());
                            ss = reader[0].ToString();
                        }
                    }
                    cost *= kol;
                    cost = cost - skidka;//Теперь у нас известна финальная цена.
                    reader.Close();

                    int old_kol = 0;
                    string sql_kol = "SELECT `kol`,`id_product` FROM `warehouse` WHERE (`id_product` = '" + ss + "')";
                    MySqlCommand command_kol = new MySqlCommand(sql_kol, connection);
                    MySqlDataReader reader_kol = command_kol.ExecuteReader();
                    while (reader_kol.Read())
                    {
                        if (reader_kol[0].ToString() != "")
                            old_kol = Convert.ToInt32(reader_kol[0].ToString());
                        else
                            old_kol = 0;
                    }
                    reader_kol.Close();

                    //Теперь подтверждение сделки.
                    DialogResult result = MessageBox.Show(
                        "Совершить сделку?", "Успех",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    bool prov = true;
                    if (result == DialogResult.Yes)
                    {
                        new_sum += cost; string sql3 = "", sql4 = "";
                        string sql1 = "INSERT INTO `sklad`.`nakladnaya`(`N_nakl`, `Date`, `Time`, `id_product`, `kol`, `cost`,`buyer`,`seller`) VALUES('" + IIDD + "', '0', '0', '" + ss + "', '" + textBox1.Text + "', '" + cost + "', '" + comboBox2.SelectedItem.ToString() + "','" + comboBox3.SelectedItem.ToString() + "' )";
                        string sql2 = "UPDATE `sklad`.`buyers` SET `kol_buy` = '" + (kol_buy + 1) + "' WHERE (`company` = '" + comboBox3.SelectedItem.ToString() + "')";
                        if (comboBox2.SelectedItem.ToString() != "Наша компания")
                            sql3 = "UPDATE `sklad`.`buyers` SET `summ_sell` = '" + (new_sum) + "' WHERE (`company` = '" + comboBox3.SelectedItem.ToString() + "')";
                        else
                            sql3 = "UPDATE `sklad`.`buyers` SET `summ_sell` = '" + (Math.Round(new_sum * 1.25))+ "' WHERE (`company` = '" + comboBox3.SelectedItem.ToString() + "')";
                        if (comboBox2.SelectedItem.ToString() != "Наша компания")
                            sql4 = "UPDATE `sklad`.`warehouse` SET `kol` = '" + (old_kol + Convert.ToInt32(textBox1.Text)) + "' WHERE (`id_product` = '" + ss + "')";
                        else if (old_kol - Convert.ToInt32(textBox1.Text) >= 0)
                            sql4 = "UPDATE `sklad`.`warehouse` SET `kol` = '" + (old_kol - Convert.ToInt32(textBox1.Text)) + "' WHERE (`id_product` = '" + ss + "')";
                        else
                            prov = false;
                        if (prov)
                        {
                            MySqlCommand command1 = new MySqlCommand(sql1, connection);
                            MySqlCommand command2 = new MySqlCommand(sql2, connection);
                            MySqlCommand command3 = new MySqlCommand(sql3, connection);
                            MySqlCommand command4 = new MySqlCommand(sql4, connection);
                            command1.ExecuteNonQuery();
                            command2.ExecuteNonQuery();
                            command3.ExecuteNonQuery();
                            command4.ExecuteNonQuery();
                            MessageBox.Show("Сделка совершена!", "Успешно!");
                            this.Close();
                        }
                        else 
                        {
                            MessageBox.Show("У вас нет столько товара чтобы его продовать!", "Ошибка!");
                            this.Close();
                        }
                    }
                    connection.Close();
                }
            }
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}
