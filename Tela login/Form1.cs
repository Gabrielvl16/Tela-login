using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CadastroAlunos
{
    public partial class Form1 : Form
    {

        private string connectionString = "Server=localhost;Database=Escola;Integrated Security=True;" + "Uid=root" + "pwd=''";

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Alunos", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Alunos (Nome, DataNascimento, Curso, Telefone) VALUES (@Nome, @DataNascimento, @Curso, @Telefone)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@DataNascimento", dtpNascimento.Value);
                    cmd.Parameters.AddWithValue("@Curso", txtCurso.Text);
                    cmd.Parameters.AddWithValue("@Telefone", txtTelefone.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um aluno para editar.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Alunos SET Nome = @Nome, DataNascimento = @DataNascimento, Curso = @Curso, Telefone = @Telefone WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", dataGridView1.SelectedRows[0].Cells["Id"].Value);
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@DataNascimento", dtpNascimento.Value);
                    cmd.Parameters.AddWithValue("@Curso", txtCurso.Text);
                    cmd.Parameters.AddWithValue("@Telefone", txtTelefone.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um aluno para excluir.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Alunos WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", dataGridView1.SelectedRows[0].Cells["Id"].Value);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtCurso.Text = row.Cells["Curso"].Value.ToString();
                txtTelefone.Text = row.Cells["Telefone"].Value.ToString();
                dtpNascimento.Value = Convert.ToDateTime(row.Cells["DataNascimento"].Value);
            }
        }
    }
}
