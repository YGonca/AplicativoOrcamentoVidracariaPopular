using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.Pkcs11Interop.Common;
using Xceed.Document.NET;

namespace AplicativoVidracariaPopular
{
    public class DALClientes
    {
        public static string path = Directory.GetCurrentDirectory() + @"\documentos\VidracariaPopularClientesDB.sqlite";
        private static SQLiteConnection sqliteConnection;

        public static List<Cliente> listaClientes = new List<Cliente>();

        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=" + path);
            sqliteConnection.Open();
            return sqliteConnection;
        }

        public static void CriarBancoSQLite()
        {
            try
            {
                if (!File.Exists(path))
                {
                    SQLiteConnection.CreateFile(path);
                }
            }
            catch
            {
                throw;
            }
        }

        public static void CriarTabelaSQLite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Clientes(id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, nome Varchar(50), endereco Varchar(50), telefone Varchar(50))";
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public static void CriarLista()
        {
            try
            {
                listaClientes.Clear();

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT id, nome, endereco, telefone FROM Clientes ORDER BY nome";

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                Id = reader["id"].ToString(),
                                Nome = reader["nome"].ToString(),
                                Endereco = reader["endereco"].ToString(),
                                Telefone = reader["telefone"].ToString()
                            };

                            listaClientes.Add(cliente);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static void Adicionar(string nome, string endereco, string telefone)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Clientes(nome, endereco, telefone) values (@nome, @endereco, @telefone)";
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@endereco", endereco);
                    cmd.Parameters.AddWithValue("@telefone", telefone);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public static void Deletar(string id)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Clientes WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
            
        }

        public static void Procurar(string nome)
        {
            try
            {
                listaClientes.Clear();

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT id, nome, endereco, telefone FROM Clientes WHERE nome LIKE @nome ORDER BY nome";
                    cmd.Parameters.AddWithValue("@nome", nome + "%");

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                Id = reader["id"].ToString(),
                                Nome = reader["nome"].ToString(),
                                Endereco = reader["endereco"].ToString(),
                                Telefone = reader["telefone"].ToString()
                            };

                            listaClientes.Add(cliente);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
