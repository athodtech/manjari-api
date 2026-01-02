using AthodBeTrackApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        private readonly IConfiguration _configuration;
        public TagRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GetQuestionTagMapping(int tagId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(_configuration.GetConnectionString("DbConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand();
                    dbSqlCommand.Connection = dbSqlconnection;
                    dbSqlCommand.CommandTimeout = 0;
                    dbSqlCommand.CommandType = CommandType.StoredProcedure;
                    dbSqlCommand.CommandText = "GetQuestionTagMapping";
                    dbSqlCommand.Parameters.AddWithValue("@tagId", tagId);

                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlAdapter.Fill(dt);
                    dbSqlconnection.Close();

                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetQuestionTagForMapping(int tagId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(_configuration.GetConnectionString("DbConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand();
                    dbSqlCommand.Connection = dbSqlconnection;
                    dbSqlCommand.CommandTimeout = 0;
                    dbSqlCommand.CommandType = CommandType.StoredProcedure;
                    dbSqlCommand.CommandText = "GetQuestionTagForMapping";
                    dbSqlCommand.Parameters.AddWithValue("@tagId", tagId);
                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlAdapter.Fill(dt);
                    dbSqlconnection.Close();

                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateQuestionTagMapping(int tagId, int userId, DataTable QuestionTagMapping)
        {
            try
            {
                SqlCommand dbSqlCommand;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(_configuration.GetConnectionString("DbConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand();
                    dbSqlCommand.Connection = dbSqlconnection;
                    dbSqlCommand.CommandTimeout = 0;
                    dbSqlCommand.CommandType = CommandType.StoredProcedure;
                    dbSqlCommand.CommandText = "UpdateQuestionTagMapping";
                    dbSqlCommand.Parameters.AddWithValue("@tagId", tagId);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.Add("@QuestionTagMapping", SqlDbType.Structured).Value = QuestionTagMapping;
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    dbSqlconnection.Close();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
