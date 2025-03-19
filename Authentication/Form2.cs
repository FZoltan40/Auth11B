using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Authentication
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private const string ConnectionString = "Server=localhost;Database=auth11b;Uid=root; Password=;SslMode=None";
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                MessageBox.Show(AddNewUser(textBox1.Text, textBox4.Text, textBox2.Text));
                this.Close();
            }
            else
            {
                MessageBox.Show("A két jelszó eltér!");
            }
        }

        private string AddNewUser(string username, string password, string email)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    string sql = "INSERT INTO `users`(`UserName`, `Email`, `Password`) VALUES (@username,@password,@email)";

                    connection.Open();
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@email", email);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    return "Sikeres regisztráció.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
