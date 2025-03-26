using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Authentication
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            ReadUserData();
        }
        private const string ConnectionString = "Server=localhost;Database=auth11b;Uid=root; Password=;SslMode=None";
        private static int UserId = 0;

        private void listBox1_Click(object sender, EventArgs e)
        {
            UserId = int.Parse(listBox1.SelectedItem.GetType().GetProperty("Id").GetValue(listBox1.SelectedItem, null).ToString());
            textBox1.Text = listBox1.SelectedItem.GetType().GetProperty("UserName").GetValue(listBox1.SelectedItem, null).ToString();
            textBox2.Text = listBox1.SelectedItem.GetType().GetProperty("Password").GetValue(listBox1.SelectedItem, null).ToString();
            textBox3.Text = listBox1.SelectedItem.GetType().GetProperty("Email").GetValue(listBox1.SelectedItem, null).ToString();
            comboBox1.Text = listBox1.SelectedItem.GetType().GetProperty("Role").GetValue(listBox1.SelectedItem, null).ToString();


        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var user = listBox1.SelectedItem;

            if (user != null)
            {
                var userObj = new
                {
                    Id = user.GetType().GetProperty("Id").GetValue(listBox1.SelectedItem, null).ToString(),
                    UserName = user.GetType().GetProperty("UserName").GetValue(listBox1.SelectedItem, null).ToString(),
                    Email = user.GetType().GetProperty("Email").GetValue(listBox1.SelectedItem, null).ToString(),
                    Password = user.GetType().GetProperty("Password").GetValue(listBox1.SelectedItem, null).ToString(),
                    Role = user.GetType().GetProperty("Role").GetValue(listBox1.SelectedItem, null).ToString()
                };

                DeleteUSerData(int.Parse(userObj.Id));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateUSerData(UserId, textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Register register = new Register();
                register.ShowDialog();
                listBox1.Items.Clear();
                ReadUserData();
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
                string sql = "SELECT * FROM users";

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
                                Email = dr.GetString(2),
                                Password = dr.GetString(3),
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

        private void DeleteUSerData(int id)
        {
            DialogResult dr = MessageBox.Show("Biztos törölni akarja a usert",
                       "Törlés", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    try
                    {
                        string sql = "DELETE FROM users WHERE Id = @id";
                        using (var connection = new MySqlConnection(ConnectionString))
                        {
                            connection.Open();
                            using (var command = new MySqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@id", id);

                                command.ExecuteNonQuery();
                            }
                            connection.Close();
                            listBox1.Items.Clear();
                            ReadUserData();
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void UpdateUSerData(int id, string username, string email, string password, string role)
        {
            DialogResult dr = MessageBox.Show("Biztos hogy módisitani akarja a user adatokat",
                       "Modosítás", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    try
                    {
                        string sql = "UPDATE `users` SET `UserName`=@username,`Email`=@email,`Password`=@password,`Role`= @role WHERE Id = @id";

                        using (var connection = new MySqlConnection(ConnectionString))
                        {
                            connection.Open();
                            using (var command = new MySqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@id", id);
                                command.Parameters.AddWithValue("@username", username);
                                command.Parameters.AddWithValue("@email", email);
                                command.Parameters.AddWithValue("@password", password);
                                command.Parameters.AddWithValue("@role", role);

                                command.ExecuteNonQuery();
                            }
                            connection.Close();
                            listBox1.Items.Clear();
                            ReadUserData();
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    break;
                case DialogResult.No:
                    break;
            }
        }
    }
}
