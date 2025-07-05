using System.Data.SqlClient;

namespace StudentskaSlužba.src.sqlutills
{
    public class SqlUtills
    {
        private static readonly string ConnectionString = @"Data Source=DESKTOP-9QG5JCA\SQLEXPRESS;Initial Catalog=studentska_sluzba;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static SqlConnection OpenSqlConnection()
        {
            SqlConnection c = new (ConnectionString);
            c.Open();
            return c;
        }

        public static void SqlUpdateString(string table, string field, string s, int id, SqlConnection c) =>
            new SqlCommand($@"update {table} set {field} = {s} where id = {id}", c);

        public static void SqlUpdateInt(string table, string field, int i, int id, SqlConnection c) =>
            new SqlCommand($@"update {table} set {field} = {i} where id = {id}", c);

        public static SqlCommand SqlLoadTable(string table, SqlConnection c) =>
            new SqlCommand($@"select * from {table}", c);

        public static void SqlInsertInto(string table, string fields, string values, SqlConnection c) =>
            new SqlCommand($@"INSERT INTO {table} ({fields}) VALUES ({values});", c).ExecuteNonQuery();

        public static void SqlDelte(string table, int id, SqlConnection c) =>
            new SqlCommand($"delete from {table} where id = {id}", c).ExecuteNonQuery();
    }
}
