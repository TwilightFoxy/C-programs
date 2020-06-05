using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Sklad
{
    public partial class add_new_pr : Form
    {
        public add_new_pr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = "";
            string connectionString = "server=localhost;user=root;database=Sklad;password=0000;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string sql = "SELECT MAX(`id_product`) AS id_product FROM `products`";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader[0].ToString();
            }
            reader.Close();
            connection.Close();
            string name = textBox1.Text;
            string e_u_m = textBox4.Text;
            string live = textBox3.Text;
            string Price = textBox2.Text;

            if (id != "" && name != "" && e_u_m != "" && live != "" && Price != "")
            {
                DatabaseManager _databaseManager = new DatabaseManager();
                MySqlCommand _mySqlCommand = new MySqlCommand("INSERT INTO `products` (`id_product`, `Name`, `U_o_m`, `Shelf_live`, `Price`)" +
                    " VALUES (@id_product,@Name,@U_o_m,@Shelf_live,@Price)", _databaseManager.GetConnection);

                try
                {
                    int idd = Convert.ToInt32(id) + 1;
                    _mySqlCommand.Parameters.Add("@id_product", MySqlDbType.VarChar).Value = idd;
                    _mySqlCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = name;
                    _mySqlCommand.Parameters.Add("@U_o_m", MySqlDbType.VarChar).Value = e_u_m;
                    _mySqlCommand.Parameters.Add("@Shelf_live", MySqlDbType.VarChar).Value = live;
                    _mySqlCommand.Parameters.Add("@Price", MySqlDbType.VarChar).Value = Price;

                    _databaseManager.OpenConnection();

                    if (_mySqlCommand.ExecuteNonQuery() == 1)
                    {
                        DialogResult result = MessageBox.Show(
                            "Закончить дообавление записей?", "Успех",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                        if (result == DialogResult.Yes)
                            this.Close();

                        //MessageBox.Show("Запись добавлена", "Успех!");
                    }
                    else
                        MessageBox.Show("Ошибка!", "Что-то пошло не так!");
                }
                catch
                {
                    MessageBox.Show("Ошибка при работе с БД", "Что-то пошло не так!");

                }
                finally
                {
                    _databaseManager.CloseConnection();
                }


                /*
                string connectionString1 =   "server=localhost;user=root;database=Sklad;password=0000;";
                MySqlConnection connection1 = new MySqlConnection(connectionString1);
                connection1.Open();
                string sql1 = "INSERT INTO `products` (`id_product`, `Name`, `U_o_m`, `Shelf_live`, `Price`) VALUES (" + idd + ", " + name + ", " + e_u_m + ", " + live + ", " + Price + ")";
                MySqlCommand command1 = new MySqlCommand(sql1, connection1);
                command1.ExecuteNonQuery();
                connection1.Close();
                */
            }
            else
                MessageBox.Show("Заполните все поля!");
        }
    }
}
