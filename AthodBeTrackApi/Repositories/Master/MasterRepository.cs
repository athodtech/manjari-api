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
    public class MasterRepository : BaseRepository, IMasterRepository
    {
        private readonly IConfiguration _configuration;

        public MasterRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DataTable GetQuestionBank(int? questionId)
        {
            DataTable dt = new();
            try
            {
                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(_configuration.GetConnectionString("DbConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand
                    {
                        Connection = dbSqlconnection,
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "GetQuestionBank"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@questionId", questionId);


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

        public async Task DeleteQuestionTagMappingAsync(int questionId)
        {
            try
            {
                SqlCommand dbSqlCommand;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(_configuration.GetConnectionString("DbConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand
                    {
                        Connection = dbSqlconnection,
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "DeleteQuestionTagMapping"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@questionId", questionId);

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    await dbSqlCommand.ExecuteNonQueryAsync();
                    await dbSqlconnection.CloseAsync();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region Village

        public async Task<DataTable> GetVillagesAsync(string where)
        {
            DataTable dt = new();
            try
            {
                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(_configuration.GetConnectionString("DbConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand
                    {
                        Connection = dbSqlconnection,
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "GetVillages"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@where", where);


                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    dbSqlAdapter.Fill(dt);
                    await dbSqlconnection.CloseAsync();

                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataTable> GetFilterVillageCountAsync(string where)
        {
            DataTable dt = new();
            try
            {
                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(_configuration.GetConnectionString("DbConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand
                    {
                        Connection = dbSqlconnection,
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "GetFilterVillageCount"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@where", where);


                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    dbSqlAdapter.Fill(dt);
                    await dbSqlconnection.CloseAsync();

                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataTable> GetUserLoationDetailsAsync(int userId, string stateIds, string districtIds, string blockIds, string villageIds, string whereClause, string limitQuery)
        {
            DataTable dt = new();
            try
            {
                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(_configuration.GetConnectionString("DbConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand
                    {
                        Connection = dbSqlconnection,
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "GetUserLoationDetails"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@stateIds", stateIds);
                    dbSqlCommand.Parameters.AddWithValue("@districtIds", districtIds);
                    dbSqlCommand.Parameters.AddWithValue("@blockIds", blockIds);
                    dbSqlCommand.Parameters.AddWithValue("@villageIds", villageIds);
                    dbSqlCommand.Parameters.AddWithValue("@whereClause", whereClause);
                    dbSqlCommand.Parameters.AddWithValue("@limitQuery", limitQuery);


                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    dbSqlAdapter.Fill(dt);
                    await dbSqlconnection.CloseAsync();

                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataTable> GetUserLoationDetailsCountAsync(int userId, string stateIds, string districtIds, string blockIds, string villageIds, string whereClause)
        {
            DataTable dt = new();
            try
            {
                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(_configuration.GetConnectionString("DbConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand
                    {
                        Connection = dbSqlconnection,
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "GetUserLoationCount"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@stateIds", stateIds);
                    dbSqlCommand.Parameters.AddWithValue("@districtIds", districtIds);
                    dbSqlCommand.Parameters.AddWithValue("@blockIds", blockIds);
                    dbSqlCommand.Parameters.AddWithValue("@villageIds", villageIds);
                    dbSqlCommand.Parameters.AddWithValue("@whereClause", whereClause);

                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    dbSqlAdapter.Fill(dt);
                    await dbSqlconnection.CloseAsync();

                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Application Logs
        public async Task<DataTable> GetApplicationLogsAsync(string activityCategoryIds, string userIds, string strwhere)
        {
            DataTable dt = new();
            try
            {
                SqlCommand dbSqlCommand;
                SqlDataAdapter dbSqlAdapter;
                SqlConnection dbSqlconnection;
                dbSqlconnection = new SqlConnection(_configuration.GetConnectionString("DbConnection"));

                using (dbSqlconnection)
                {
                    dbSqlCommand = new SqlCommand
                    {
                        Connection = dbSqlconnection,
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "GetApplicationLogs"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryIds", activityCategoryIds);
                    dbSqlCommand.Parameters.AddWithValue("@userIds", userIds);
                    dbSqlCommand.Parameters.AddWithValue("@strwhere", strwhere);                  

                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    dbSqlAdapter.Fill(dt);
                    await dbSqlconnection.CloseAsync();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
