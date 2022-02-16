using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_AdoNet
{
    public class Programador
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string linguagem { get; set; }
        public string banco { get; set; }

        public bool gravarProgramador()
        {
            Banco bd = new Banco();
            SqlConnection cn = bd.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new SqlCommand();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            //command.CommandText = " insert into programadores values ('" + nome + "','" + linguagem + "','" + banco + "')";
            command.CommandText = " insert into programadores values (@nome, @linguagem, @banco)";
            command.Parameters.Add("@nome", SqlDbType.VarChar);
            command.Parameters.Add("@linguagem", SqlDbType.VarChar);
            command.Parameters.Add("@banco", SqlDbType.VarChar);
            command.Parameters[0].Value = nome;
            command.Parameters[1].Value = linguagem;
            command.Parameters[2].Value = banco;

            try
            {
                command.ExecuteNonQuery();
                tran.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return false;
            }

            finally
            {
                bd.fecharConexao();
            }

        }

        public bool excluirProgramador(int ID)
        {
            Banco bd = new Banco();

            SqlConnection cn = bd.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new SqlCommand();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from programadores where id = @ID";
            command.Parameters.Add("@ID", SqlDbType.Int);
            command.Parameters[0].Value = ID;
            try
            {
                command.ExecuteNonQuery();
                tran.Commit();

                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return false;
            }

            finally
            {
                bd.fecharConexao();
            }
        }

        public bool atualizarProgramador(int id, string nome, string linguagem, string banco)
        {
            Banco bd = new Banco();

            SqlConnection cn = bd.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();

            SqlCommand command = new SqlCommand();
            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;
            command.CommandText = "update programadores set nome=@nome,linguagem= @linguagem,banco=@banco where id = @ID";
            command.Parameters.Add("@ID", SqlDbType.Int);
            command.Parameters.Add("@nome", SqlDbType.VarChar);
            command.Parameters.Add("@linguagem", SqlDbType.VarChar);
            command.Parameters.Add("@banco", SqlDbType.VarChar);
            command.Parameters[0].Value = id;
            command.Parameters[1].Value = nome;
            command.Parameters[2].Value = linguagem;
            command.Parameters[3].Value = banco;

            try
            {
                command.ExecuteNonQuery();
                tran.Commit();
                MessageBox.Show("Programador Atualizado com Sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return false;
            }

            finally
            {
                bd.fecharConexao();
            }

        }

        public Programador retornaProgramador(int id)
        {
            Banco bd = new Banco();
            try
            {
                SqlConnection cn = bd.abrirConexao();
                SqlCommand command = new SqlCommand("SELECT * from programadores", cn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetInt32(0) == id)
                    {
                        this.id = reader.GetInt32(0);
                        nome = reader.GetString(1);
                        linguagem = reader.GetString(2);
                        banco = reader.GetString(3);

                        return this;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                bd.fecharConexao();
            }
        }
    }
}
