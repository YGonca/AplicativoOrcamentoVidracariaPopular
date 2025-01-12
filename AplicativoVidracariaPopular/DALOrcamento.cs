using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Xml;
using System.Data;

namespace AplicativoVidracariaPopular
{
    public class DALOrcamento
    {
        public static string path = Directory.GetCurrentDirectory() + @"\documentos\VidracariaPopularDB.sqlite";
        private static SQLiteConnection sqliteConnection;

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
                if(!File.Exists(path))
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
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Orcamento(id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, nome Varchar(50))";
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public static int GetId()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "Select max(id) from Orcamento";
                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    return id;
                }
            }
            catch
            {
                throw;
            }
        }

        public static void Add(string nome)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Orcamento(nome) values (@nome)";
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
