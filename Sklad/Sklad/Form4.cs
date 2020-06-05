using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Sklad
{
    public partial class add_buyer : Form
    {
        public add_buyer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                DatabaseManager _databaseManager = new DatabaseManager();
                MySqlCommand _mySqlCommand = new MySqlCommand("INSERT INTO `buyers` (`company`, `discont`, `kol_buy`, `adres`, `summ_sell`)" +
                    " VALUES (@company,@discont,@kol_buy,@adres,@summ_sell)", _databaseManager.GetConnection);

                try
                {
                    _mySqlCommand.Parameters.Add("@company", MySqlDbType.VarChar).Value = textBox1.Text;
                    _mySqlCommand.Parameters.Add("@discont", MySqlDbType.VarChar).Value = 0;
                    _mySqlCommand.Parameters.Add("@kol_buy", MySqlDbType.VarChar).Value = 0;
                    _mySqlCommand.Parameters.Add("@adres", MySqlDbType.VarChar).Value = textBox2.Text;
                    _mySqlCommand.Parameters.Add("@summ_sell", MySqlDbType.VarChar).Value = 0;

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
            }
        }
    }
}
