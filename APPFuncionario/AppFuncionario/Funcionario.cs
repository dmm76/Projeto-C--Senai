using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace AppFuncionario
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Turno { get; set; }
        public string Data_nascimento { get; set; }
        public string Matricula { get; set; }

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\SENAI_C#\\APPFuncionario\\AppFuncionario\\DbFuncionario.mdf;Integrated Security=True");

        public List<Funcionario> ListaFuncionario()
        {
            List<Funcionario> li = new List<Funcionario>();
            string sql = "SELECT * FROM Funcionario";
            if(con.State == ConnectionState.Open) //fecha o banco se ele ja estiver aberto, para nao der erro
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader(); //dr = data reader
            while(dr.Read())
            {
                Funcionario func = new Funcionario(); //chama a classe
                func.Id = (int)dr["Id"]; 
                func.Nome = dr["Nome"].ToString();
                func.Turno = dr["Turno"].ToString();
                func.Data_nascimento = dr["Data_nascimento"].ToString();
                func.Matricula = dr["Matricula"].ToString();
                li.Add(func); // addiciona os dados na lista
             }
            dr.Close();
            con.Close();
            return li;
        }

        public void Inserir(string Nome, string Turno, string Data_nascimento, string Matricula)
        {
            try
            {
                string sql = "INSERT INTO Funcionario(Nome, Turno, Data_nascimento, Matricula) VALUES ('" + Nome + "', '" + Turno + "', '" + Data_nascimento + "', '" + Matricula + "')";
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand (sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }

        public void Localizar(int Id)
        {
            try
            {
                string sql = "SELECT * FROM Funcionario Where = Id='" + Id + "'";
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con); 
                SqlDataReader dr = cmd.ExecuteReader(); 
                while(dr.Read())
                {
                    Nome = dr["Nome"].ToString();
                    Turno = dr["Turno"].ToString();
                    Data_nascimento = dr["Data_nascimento"].ToString();
                    Matricula = dr["Matricula"].ToString() ;
                }
                dr.Close();
                con.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        public void Atualizar(int Id, string Nome, string Turno, string Data_nascimento, string Matricula)
        {
            try
            {
                string sql = "UPDATE Funcionario SET Nome='" + Nome + "', Turno='" + Turno + "', Data_nascimento='" + Data_nascimento + "', Matricula='" + Matricula + "' WHERE Id='"+Id+"'";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }

        public void Excluir(int Id)
        {
            try
            {
                string sql = "DELETE FROM Funcionario WHERE Id='" + Id + "'";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
        }

        public bool RegistroRepetido(string Matricula)
        {
            string sql = "SELECT * FROM Funcionario WHERE Matricula='" + Matricula + "'";
            if(con.State == ConnectionState.Open) 
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if(result != null)
            {
                return (int)result > 0;
            }
            con.Close();
            return false;
        }

        public bool ExisteId(int Id)
        {
            string sql = "SELECT * FROM Funcionario WHERE Id='" + Id + "'";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return (int)result > 0;
            }
            con.Close();
            return false;
        }

        public bool Aniversario(string Data)
        {
            string sql = "SELECT Data_nascimento FROM Funcionario";
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            var dataNiver = Data;
            string data1 = dataNiver.Substring(0, 2);
            string data2 = dataNiver.Substring(3, 2);
            while (dr.Read())
            {
                var datanasc = dr["Data_nascimento"].ToString();
                string datan1 = datanasc.Substring(0, 2);
                string datan2 = datanasc.Substring(3, 2);
                if (data1.ToString() == datan1.ToString() && data2.ToString() == datan2.ToString())
                {
                    return true;
                }
                
            }
            dr.Close();
            con.Close();
            return false;
        }
    }

 }
