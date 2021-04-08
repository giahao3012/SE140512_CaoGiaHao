using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongAssemblies
{
    public class Song
    {
        public int SongID { get; set; }
        public string SongName { get; set; }
        public int Duration { get; set; }
        public string Singer { get; set; }
    }
    public class SongDAO
    {
        string strConnection;
        public SongDAO()
        {
            getConnectionString();
        }
        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["PE03"].ConnectionString;
            return strConnection;
        }
        public DataTable getSongs()
        {
            string SQL = "select * from Songs";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtProduct = new DataTable();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                { cnn.Open(); }
                adapter.Fill(dtProduct);
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return dtProduct;
        }
        public bool addSong(Song song)
        {
            string SQL = "Insert Songs values(@ID,@Name,@Duration,@Singer)";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            bool result;

            cmd.Parameters.AddWithValue("@ID", song.SongID);
            cmd.Parameters.AddWithValue("@Name", song.SongName);
            cmd.Parameters.AddWithValue("@Duration", song.Duration);
            cmd.Parameters.AddWithValue("@Singer", song.Singer);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                { cnn.Open(); }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }
        public bool updateSong(Song song)
        {
            string SQL = "Update Songs set SongName=@Name,Duration=@Duration,Singer=@Singer" +
                " where SongID=@ID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            bool result;

            cmd.Parameters.AddWithValue("@ID", song.SongID);
            cmd.Parameters.AddWithValue("@Name", song.SongName);
            cmd.Parameters.AddWithValue("@Duration", song.Duration);
            cmd.Parameters.AddWithValue("@Singer", song.Singer);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                { cnn.Open(); }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public int findSongbyID(int SongID)
        {
            string SQL = "Select * from Songs where SongID=@ID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            int result;

            cmd.Parameters.AddWithValue("@ID", SongID);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                { cnn.Open(); }
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = int.Parse(reader["SongID"].ToString());

                    return result;
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return 0;
        }
        public DataTable findSongbyName(string SongName)
        {
            string SQL = "Select * from Songs where Songname like @Name";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Name", "%" + SongName + "%");
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtSong = new DataTable();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                { cnn.Open(); }
                adapter.Fill(dtSong);

                if(dtSong!=null && dtSong.Rows.Count!=0)
                { return dtSong; }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return null;
        }
    }

}
