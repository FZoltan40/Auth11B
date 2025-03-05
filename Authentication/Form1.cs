using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Authentication
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private const string ConnectionString = "Server=localhost;Database=auth11b;Uid=root; Password=;SslMode=None";


        private void label3_Click(object sender, EventArgs e)
        {
            Register form2 = new Register();
            form2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginUser(textBox1.Text, textBox2.Text);
        }

        //INSERT INTO `users`(`UserName`, `Email`, `Password`) VALUES ('kata01','kata01@gmail.com','Alma')
        private void LoginUser(string username, string password)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                string sql = "SELECT `Id`,`Role` FROM `users` WHERE `UserName`= @username AND `Password`= @password;";

                connection.Open();
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    MySqlDataReader dr = command.ExecuteReader();

                    var result = dr.Read();

                    MessageBox.Show(result.ToString());
                }
                connection.Close();
            }
        }
    }
}
