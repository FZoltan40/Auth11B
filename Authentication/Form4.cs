using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Authentication
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
            ReadUserData();
        }

        private const string ConnectionString = "Server=localhost;Database=auth11b;Uid=root; Password=;SslMode=None";
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Register register = new Register();
                register.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ReadUserData()
        {
            try
            {
                string sql = "SELECT * FROM users WHERE Role = 0";

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(sql, connection))
                    {
                        MySqlDataReader dr = command.ExecuteReader();

                        while (dr.Read())
                        {
                            var newUser = new
                            {
                                Id = dr.GetInt32(0),
                                UserName = dr.GetString(1),
                                Password = dr.GetString(3),
                                Email = dr.GetString(2),
                                Role = dr.GetInt32(4)
                            };

                            listBox1.Items.Add(newUser);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
