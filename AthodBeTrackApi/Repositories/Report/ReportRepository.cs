using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        private readonly IConfiguration _configuration;

        public ReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetCode(string tableTransaction)
        {
            string code = string.Empty;
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
                        CommandType = CommandType.StoredProcedure
                    };
                    dbSqlCommand.Parameters.AddWithValue("@Transaction", tableTransaction);
                    dbSqlCommand.Parameters.Add("@DocumentCode", SqlDbType.NVarChar, 50);
                    dbSqlCommand.Parameters["@DocumentCode"].Direction = ParameterDirection.Output;

                    dbSqlCommand.CommandText = "GenerateDocument";

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    code = Convert.ToString(dbSqlCommand.Parameters["@DocumentCode"].Value);
                    dbSqlconnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return code;
        }

        public DataTable GetReports(int? reportId, int? activityCategoryMappingId, bool? isActive, int? userId, int? reportStatus)
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
                        CommandText = "GetReports"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@reportId", reportId);
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@isActive", isActive);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@reportStatus", reportStatus);

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

        public DataTable GetAllReports(int? activityCategoryMappingId, bool? isActive, int? userId, int? reportStatus)
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
                        CommandText = "GetAllReports"
                    };                   
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@isActive", isActive);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@reportStatus", reportStatus);

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

        public DataTable GetGraphDataWithPagination(int reportId, int pageNumber, int rowsOfPage)
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
                        CommandText = "GetReportSummaryWithPagination"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@reportId", reportId);
                    dbSqlCommand.Parameters.AddWithValue("@pageNumber", pageNumber);
                    dbSqlCommand.Parameters.AddWithValue("@rowsOfPage", rowsOfPage);

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

        public async Task GenerateReportSummaryAsync(int reportId)
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
                        CommandText = "GenerateReportSummary"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@RId", reportId);

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

        public async Task<bool> SaveReprtTemplateAsync(int reportId, int userId, DataTable reportChartTemplate, DataTable reportItemTemplate)
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
                        CommandText = "SaveReportTemplate"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);
                    dbSqlCommand.Parameters.AddWithValue("@UserId", userId);
                    dbSqlCommand.Parameters.Add("@ReportChartTemplate", SqlDbType.Structured).Value = reportChartTemplate;
                    dbSqlCommand.Parameters.Add("@ReportItemTemplate", SqlDbType.Structured).Value = reportItemTemplate;
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    await dbSqlCommand.ExecuteNonQueryAsync();
                    await dbSqlconnection.CloseAsync();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetQuestionDetails(int reportId, int activityQuestionId)
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
                        CommandText = "RPT_GetQuestionDetails"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);
                    dbSqlCommand.Parameters.AddWithValue("@ActivityQuestionId", activityQuestionId);

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
        
        public async Task<DataTable> RPT_GetQuestionDetailsDynamic(int reportId, int activityQuestionId,string filter, bool isUniqueFilter,bool isPrimaryFilter, string where, string limitQuery)
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
                        CommandText = "RPT_GetQuestionDetailsDynamic"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);
                    dbSqlCommand.Parameters.AddWithValue("@ActivityQuestionId", activityQuestionId);
                    dbSqlCommand.Parameters.AddWithValue("@filter", filter);
                    dbSqlCommand.Parameters.AddWithValue("@isUniqueFilter", isUniqueFilter);
                    dbSqlCommand.Parameters.AddWithValue("@isPrimaryFilter", isPrimaryFilter);
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

        public DataTable RPT_GetQuestionDetailsDynamic2(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter, string where, string limitQuery)
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
                        CommandText = "RPT_GetQuestionDetailsDynamic"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);
                    dbSqlCommand.Parameters.AddWithValue("@ActivityQuestionId", activityQuestionId);
                    dbSqlCommand.Parameters.AddWithValue("@filter", filter);
                    dbSqlCommand.Parameters.AddWithValue("@isUniqueFilter", isUniqueFilter);
                    dbSqlCommand.Parameters.AddWithValue("@isPrimaryFilter", isPrimaryFilter);
                    dbSqlCommand.Parameters.AddWithValue("@whereClause", where);
                    dbSqlCommand.Parameters.AddWithValue("@limitQuery", limitQuery);

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


        public async Task<DataTable> RPT_ExportQuestionDetails(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter)
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
                        CommandText = "RPT_ExportQuestionDetails"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);
                    dbSqlCommand.Parameters.AddWithValue("@ActivityQuestionId", activityQuestionId);
                    dbSqlCommand.Parameters.AddWithValue("@filter", filter);
                    dbSqlCommand.Parameters.AddWithValue("@isUniqueFilter", isUniqueFilter);
                    dbSqlCommand.Parameters.AddWithValue("@isPrimaryFilter", isPrimaryFilter);                   

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


        public async Task<DataTable> RPT_GetQuestionDetailsCount(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter, string where)
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
                        CommandText = "RPT_GetQuestionDetailsCount"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);
                    dbSqlCommand.Parameters.AddWithValue("@ActivityQuestionId", activityQuestionId);
                    dbSqlCommand.Parameters.AddWithValue("@filter", filter);
                    dbSqlCommand.Parameters.AddWithValue("@isUniqueFilter", isUniqueFilter);
                    dbSqlCommand.Parameters.AddWithValue("@isPrimaryFilter", isPrimaryFilter);
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

        public DataSet GetReportLocation(int reportId)
        {
            DataSet ds = new();
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
                        CommandText = "RPT_GetReportLocation"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);

                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlAdapter.Fill(ds);
                    dbSqlconnection.Close();

                    return ds;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataTable GetUsersForShareReport(string ReportNo, int UserId)
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
                    dbSqlCommand = new SqlCommand
                    {
                        Connection = dbSqlconnection,
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "RPT_GetUsersForShareReport"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportNo", ReportNo);
                    dbSqlCommand.Parameters.AddWithValue("@UserId", UserId);

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

        public async Task<bool> DeleteSharedReport(string reportId)
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
                        CommandText = "RPT_DeleteSharedReport"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    await dbSqlCommand.ExecuteNonQueryAsync();
                    await dbSqlconnection.CloseAsync();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable RPT_GetUpdatedHousehold(int reportId)
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
                        CommandText = "RPT_GetUpdatedHousehold"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);

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

        public async Task<DataTable> RPT_GetUpdatedHouseholdList(int reportId, string where)
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
                        CommandText = "RPT_GetUpdatedHouseholdList"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@reportId", reportId);
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

        public async Task<DataTable> RPT_ExportUpdatedHouseholdList(int reportId)
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
                        CommandText = "RPT_ExportUpdatedHouseholdList"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@reportId", reportId);
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

        public async Task<DataTable> RPT_GetUpdatedHouseholdCount(int reportId, string where)
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
                        CommandText = "RPT_GetUpdatedHouseholdCount"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@reportId", reportId);
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

        public async Task<bool> MakeFevoriteReport(int reportId, int userId)
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
                        CommandText = "RPT_MakeFevoriteReport"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@reportId", reportId);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    await dbSqlCommand.ExecuteNonQueryAsync();
                    await dbSqlconnection.CloseAsync();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteReportTemplate(int reportId)
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
                        CommandText = "RPT_DeleteReportTemplate"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    await dbSqlCommand.ExecuteNonQueryAsync();
                    await dbSqlconnection.CloseAsync();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<DataTable> GenerateReportAsync(int reportId, int userId)
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
                        CommandText = "RPT_GenerateReportTemplate"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);
                    dbSqlCommand.Parameters.AddWithValue("@UserId", userId);

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

        public async Task<DataSet> GenerateReport(int reportId, int userId)
        {
            DataSet ds = new();
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
                        CommandText = "RPT_GenerateReportTemplate"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);
                    dbSqlCommand.Parameters.AddWithValue("@UserId", userId);

                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    dbSqlAdapter.Fill(ds);
                    await dbSqlconnection.CloseAsync();
                    return ds;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<DataTable> GetGenerateReportDetailsAsync(int reportId)
        {
            DataTable dataTable = new();
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
                        CommandText = "GetGenerateReportDetails"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);
                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    dbSqlAdapter.Fill(dataTable);
                    await dbSqlconnection.CloseAsync();

                    return dataTable;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ResetReportTemplate(int reportId)
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
                        CommandText = "ResetReportTemplate"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@reportId", reportId);

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

        public async Task<bool> DeleteReport(int reportId)
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
                        CommandText = "RPT_DeleteReport"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ReportId", reportId);

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    await dbSqlCommand.ExecuteNonQueryAsync();
                    await dbSqlconnection.CloseAsync();
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
