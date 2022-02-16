using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_AdoNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConexao_Click(object sender, EventArgs e)
        {

            try
            {
                Banco banco = new Banco();
                banco.abrirConexao();
                MessageBox.Show("Você esta Conectado!", "CONEXÂO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Erro na Conexão!", "CONEXÂO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Programador programador = new Programador();
            programador.nome = txtNome.Text;
            programador.linguagem = txtLinguagem.Text;
            programador.banco = txtBanco.Text;

            bool retorno = programador.gravarProgramador();

            if (retorno)
            {
                MessageBox.Show("Gravado com Sucesso!", "CONEXÂO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Erro ao gravar!", "CONEXÂO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Banco db = new Banco();
                string sql = "select * from programadores";

                DataTable dt = new DataTable();
                dt = db.executarColsultaGenerica(sql);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Programador programador = new Programador();
                programador = programador.retornaProgramador(int.Parse(txtId.Text));
                if (programador != null)
                {
                    txtNome.Text = programador.nome;
                    txtLinguagem.Text = programador.linguagem;
                    txtBanco.Text = programador.banco;
                }
                else
                {
                    MessageBox.Show("Programador não encontrado!");
                }

                //MessageBox.Show(programador.nome);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            finally
            {
                Console.WriteLine("Banco fechado");
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Programador programador = new Programador();
            programador = programador.retornaProgramador(int.Parse(txtId.Text));

            if (programador != null)

                try
                {
                    if (programador != null)
                    {
                        programador.atualizarProgramador(int.Parse(txtId.Text), txtNome.Text, txtLinguagem.Text, txtBanco.Text);
                    }

                    else
                    {
                        MessageBox.Show("Programador não encontrado!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Programador programador = new Programador();
            programador.id = int.Parse(txtId.Text);
            programador = programador.retornaProgramador(programador.id);
            if (programador == null)
            {
                MessageBox.Show("Erro ao excluir: O programador não foi encontrado (404)!");
                return;
            }
            bool retorno = programador.excluirProgramador(programador.id);
            if (retorno == true)
            {
                MessageBox.Show("Excluído com sucesso!");
                limparCampos();
            }
            else
            {
                MessageBox.Show("Erro ao executar a exclusão!");
            }

        }

        //Preencher os campos com o click na linha selecionada do dataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                try
                {
                    Programador programador = new Programador();
                    int idProgramador = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    programador = programador.retornaProgramador(idProgramador);
                    txtId.Text = programador.id.ToString();
                    txtNome.Text = programador.nome;
                    txtLinguagem.Text = programador.linguagem;
                    txtBanco.Text = programador.banco;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void limparCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtLinguagem.Text = "";
            txtBanco.Text = "";
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Programador programador = new Programador();
                programador = programador.retornaProgramador(int.Parse(txtId.Text));
                if (programador != null)
                {
                    txtNome.Text = programador.nome;
                    txtLinguagem.Text = programador.linguagem;
                    txtBanco.Text = programador.banco;
                }
                else
                {
                    MessageBox.Show("Programador não encontrado!");
                }

                //MessageBox.Show(programador.nome);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            finally
            {
                Console.WriteLine("Banco fechado");
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void consultarTodos()
        {
            try
            {
                Banco db = new Banco();
                string sql = "select * from programadores";

                DataTable dt = new DataTable();
                dt = db.executarColsultaGenerica(sql);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            consultarTodos();
        }
    }
}
