using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public class DashboardRepository : BaseRepository, IDashboardRepository
    {
        private readonly IConfiguration _configuration;
        public DashboardRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GetUserAssignedActivity(int userId)
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
                    dbSqlCommand.CommandTimeout = 240;
                    dbSqlCommand.CommandType = CommandType.StoredProcedure;
                    dbSqlCommand.CommandText = "GetUserAssignedActivity";
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);


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

        public DataTable GetUserAssignedActivityCategory(int userId, int activityId)
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
                    dbSqlCommand.CommandTimeout = 240;
                    dbSqlCommand.CommandType = CommandType.StoredProcedure;
                    dbSqlCommand.CommandText = "GetUserAssignedActivityCategory";
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@activityId", activityId);


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

    }
}
