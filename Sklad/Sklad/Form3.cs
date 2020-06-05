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
            string id = "0";
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
            if (id == "") id = "0";
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

                int new_N = 0;
                string connectionString1 = "server=localhost;user=root;database=Sklad;password=0000;";
                MySqlConnection connection1 = new MySqlConnection(connectionString1);
                connection1.Open();
                string sql_N_nakl = "SELECT MAX(`N_nakl`) AS N_nakl FROM `warehouse`";
                MySqlCommand N_nakl = new MySqlCommand(sql_N_nakl, connection1);
                MySqlDataReader reader_N_nakl = N_nakl.ExecuteReader();
                while (reader_N_nakl.Read())
                {
                    if (reader_N_nakl[0].ToString() != "")
                        new_N = Convert.ToInt32(reader_N_nakl[0].ToString()) + 1;
                    else
                        new_N = 0;
                }
                reader_N_nakl.Close();
                int stelash = 0;
                string sql_new_stelash = "SELECT MAX(`stelash`) AS stelash FROM `warehouse`";
                MySqlCommand new_stelash = new MySqlCommand(sql_new_stelash, connection);
                MySqlDataReader reader_new_stelash = new_stelash.ExecuteReader();
                while (reader_new_stelash.Read())
                {
                    if (reader_new_stelash[0].ToString() != "")
                        stelash = Convert.ToInt32(reader_new_stelash[0].ToString()) + 1;
                    else
                        stelash = 0;
                }
                reader_new_stelash.Close();
                string sql4 = "INSERT INTO `sklad`.`warehouse`(`N_nakl`, `id_product`, `kol`, `stelash`) VALUES('" + new_N + "', '"+(Convert.ToInt32(id) + 1)+"', '0', '"+ stelash+"')";
                MySqlCommand command4 = new MySqlCommand(sql4, connection1);
                command4.ExecuteNonQuery();
                connection1.Close();
            }
            else
                MessageBox.Show("Заполните все поля!");
        }
    }
}
