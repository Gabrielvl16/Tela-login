using CadastroAlunos;
using MySql.Data.MySqlClient; // Caso use MySQL
using System;
using System.Windows.Forms;

namespace Tela_de_login
{
    public partial class formsLogin : Form
    {
        // Caso use MySQL
        private string connectionString = "Server=localhost;Database=Escola;Uid=root;Pwd=''";

        public formsLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Verifica se os campos estão vazios
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtSenha.Text))
            {
                MessageBox.Show("Por favor, preencha ambos os campos de usuário e senha.");
                return;
            }

            // Usando o MySQL com parâmetros adequados
            using (MySqlConnection conn = new MySqlConnection(connectionString)) // MySqlConnection se for MySQL
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario AND Senha = @Senha";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                        cmd.Parameters.AddWithValue("@Senha", txtSenha.Text);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Acesso liberado!");
                            Form1 form1 = new Form1();
                            form1.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Usuário ou senha incorretos!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro de conexão: " + ex.Message);
                }
            }
        }
    }
}
