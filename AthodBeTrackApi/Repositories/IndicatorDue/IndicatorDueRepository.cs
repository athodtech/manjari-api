using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace AthodBeTrackApi.Repositories
{
    public class IndicatorDueRepository : BaseRepository, IIndicatorDueRepository
    {
        private readonly IConfiguration _configuration;
        public IndicatorDueRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<DataTable> GetIndicatorDueSummary(int userId, int activityCategoryMappingId, int? stateId, int? districtId, int? blockId, int? villageId, int? groupId, int? reportingFrequencyId, string status)
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
                        CommandText = "GetDueReportSummary"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@stateId", stateId);
                    dbSqlCommand.Parameters.AddWithValue("@districtId", districtId);
                    dbSqlCommand.Parameters.AddWithValue("@blockId", blockId);
                    dbSqlCommand.Parameters.AddWithValue("@villageId", villageId);
                    dbSqlCommand.Parameters.AddWithValue("@groupId", groupId);
                    dbSqlCommand.Parameters.AddWithValue("@reportingFrequencyId", reportingFrequencyId);
                    dbSqlCommand.Parameters.AddWithValue("@status", status);
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


        public async Task<DataTable> GetIndicatorDueDetailsDynamic(int userId, int? stateId, int? districtId, int? blockId, int? villageId, int activityCategoryMappingId, int ActivityQuestionId, int? groupId, string status, string where, string limitQuery)
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
                        CommandText = "GetDueReportDetailDynamic"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@stateId", stateId);
                    dbSqlCommand.Parameters.AddWithValue("@districtId", districtId);
                    dbSqlCommand.Parameters.AddWithValue("@blockId", blockId);
                    dbSqlCommand.Parameters.AddWithValue("@villageId", villageId);
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@ActivityQuestionId", ActivityQuestionId);
                    dbSqlCommand.Parameters.AddWithValue("@groupId", groupId);
                    dbSqlCommand.Parameters.AddWithValue("@status", status);
                    dbSqlCommand.Parameters.AddWithValue("@whereClause", where);
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
        public async Task<DataTable> GetIndicatorDueDetailsCountDynamic(int userId, int? stateId, int? districtId, int? blockId, int? villageId, int activityCategoryMappingId, int ActivityQuestionId, int? groupId, string status, string where)
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
                        CommandText = "GetDueReportDetailCountDynamic"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@stateId", stateId);
                    dbSqlCommand.Parameters.AddWithValue("@districtId", districtId);
                    dbSqlCommand.Parameters.AddWithValue("@blockId", blockId);
                    dbSqlCommand.Parameters.AddWithValue("@villageId", villageId);
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@ActivityQuestionId", ActivityQuestionId);
                    dbSqlCommand.Parameters.AddWithValue("@groupId", groupId);
                    dbSqlCommand.Parameters.AddWithValue("@status", status);
                    dbSqlCommand.Parameters.AddWithValue("@whereClause", where);


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

        public async Task RefreshDueIndicator()
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DbConnection");

                await using var dbSqlconnection = new SqlConnection(connectionString);
                await using var dbSqlCommand = new SqlCommand("GenerateDueReportSummary", dbSqlconnection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 0
                };

                // Add parameters with DBNull.Value for null values
                dbSqlCommand.Parameters.AddWithValue("@status", DBNull.Value);
                dbSqlCommand.Parameters.AddWithValue("@ActivityCategoryMappingId", DBNull.Value);
                dbSqlCommand.Parameters.AddWithValue("@GroupId", DBNull.Value);

                await dbSqlconnection.OpenAsync();
                await dbSqlCommand.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                // Example: _logger.LogError(ex, "Error in RefreshDueIndicator");
                throw;
            }
        }

        public async Task<bool> CheckDueIndicatorReportGeneratedAsync()
        {
            try
            {
                using (_dbContext = new AthodDbContext())
                {
                    return await _dbContext.DueReportDetails.AnyAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
