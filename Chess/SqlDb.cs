using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class SqlDb
    {
        string connectionString = @"Server=DESKTOP-RMTFQVP\MYSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public void InsertFigure(Figure figure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // connection.Open();
                string query = "INSERT Into [CHESS].[dbo].[Last_Position] (/*cColor,*/ cSymbol, cXpos, cYpos) " + "VALUES (/*@Color,*/@Symbol,@Xpos,@Ypos)";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    // define parameters and their values
                    cmd.CommandText = query;
                    //cmd.Parameters.Add("@Color",SqlDbType.Bit).Value= (int)figure.Color;
                    cmd.Parameters.Add("@Symbol",SqlDbType.NChar).Value= figure.GetSymbol();
                    cmd.Parameters.AddWithValue("@Xpos", SqlDbType.Int).Value= figure.X;
                    cmd.Parameters.AddWithValue("@Ypos", SqlDbType.Int).Value= figure.Y;

                    cmd.CommandType = CommandType.Text;
                    // open connection, execute INSERT, close connection
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
        }
        public char[,] Reader()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                ChessBoard cb = new ChessBoard();
                connection.Open();
                string query = "SELECT cSymbol,cXpos,cYpos FROM [CHESS].[dbo].[Last_Position]";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            string cSymbol = sqlDataReader["cSymbol"].ToString();
                            char charSymbol = cSymbol[0];
                            int cXpos = (int)sqlDataReader["cXpos"];
                            int cYpos = (int)sqlDataReader["cYpos"];
                            cb.Board[cXpos, cYpos] = charSymbol;
                        }
                    }
                    sqlDataReader.Close();
                }
                connection.Close();
                return cb.Board;
            }
        }
    }
}
