using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public class ActivityQuestionSetRepository : BaseRepository, IActivityQuestionSetRepository
    {
        private readonly IConfiguration _configuration;

        public ActivityQuestionSetRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GetActivityQuestionSet(int activityCategoryMappingId, int userId, string days, DateTime? fromDate, DateTime? toDate)
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
                        CommandText = "GetActivityQuestionSet"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@days", days);
                    dbSqlCommand.Parameters.AddWithValue("@fromDate", fromDate);
                    dbSqlCommand.Parameters.AddWithValue("@toDate", toDate);

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

        public DataTable GetActivityQuestionSet(int activityCategoryMappingId, int userId, string days, DateTime? fromDate, DateTime? toDate, string stateId, string districtId, string blockId, string villageId)
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
                        CommandText = "GetActivityQuestionSetNew"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@days", days);
                    dbSqlCommand.Parameters.AddWithValue("@fromDate", fromDate);
                    dbSqlCommand.Parameters.AddWithValue("@toDate", toDate);
                    dbSqlCommand.Parameters.AddWithValue("@stateId", stateId);
                    dbSqlCommand.Parameters.AddWithValue("@districtId", districtId);
                    dbSqlCommand.Parameters.AddWithValue("@blockId", blockId);
                    dbSqlCommand.Parameters.AddWithValue("@villageId", villageId);

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


        public async Task<DataTable> GetHouseholdSetsAsync(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string lFilter, string whereClause, string limitQuery, int userId)
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
                        CommandText = "GetHouseholdSets"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@days", days);
                    dbSqlCommand.Parameters.AddWithValue("@fromDate", fromDate);
                    dbSqlCommand.Parameters.AddWithValue("@toDate", toDate);
                    dbSqlCommand.Parameters.AddWithValue("@lFilter", lFilter);
                    dbSqlCommand.Parameters.AddWithValue("@whereClause", whereClause);
                    dbSqlCommand.Parameters.AddWithValue("@limitQuery", limitQuery);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);

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

        public async Task<DataTable> GetHouseholdSetsCountAsync(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string lFilter, string whereClause, int userId)
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
                        CommandText = "GetHouseholdSetsCount"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@days", days);
                    dbSqlCommand.Parameters.AddWithValue("@fromDate", fromDate);
                    dbSqlCommand.Parameters.AddWithValue("@toDate", toDate);
                    dbSqlCommand.Parameters.AddWithValue("@lFilter", lFilter);
                    dbSqlCommand.Parameters.AddWithValue("@whereClause", whereClause);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);

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


        public async Task<DataTable> GetHouseholdDeletedSetsAsync(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string lFilter, string whereClause, string limitQuery, int userId)
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
                        CommandText = "GetHouseholdDeletedSets"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@days", days);
                    dbSqlCommand.Parameters.AddWithValue("@fromDate", fromDate);
                    dbSqlCommand.Parameters.AddWithValue("@toDate", toDate);
                    dbSqlCommand.Parameters.AddWithValue("@lFilter", lFilter);
                    dbSqlCommand.Parameters.AddWithValue("@whereClause", whereClause);
                    dbSqlCommand.Parameters.AddWithValue("@limitQuery", limitQuery);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);

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

        public async Task<DataTable> GetHouseholdDeletedSetsCountAsync(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string lFilter, string whereClause, int userId)
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
                        CommandText = "GetHouseholdDeletedSetsCount"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@days", days);
                    dbSqlCommand.Parameters.AddWithValue("@fromDate", fromDate);
                    dbSqlCommand.Parameters.AddWithValue("@toDate", toDate);
                    dbSqlCommand.Parameters.AddWithValue("@lFilter", lFilter);
                    dbSqlCommand.Parameters.AddWithValue("@whereClause", whereClause);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);

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


        public async Task<DataTable> ExportHouseholdSetsAsync(int activityCategoryMappingId, int userId, int status)
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
                        CommandText = "ExportHouseholdSets"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
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


        public int CreateActivityQuestionSet(ActivityQuestionSetUniqueIdentityModel request)
        {
            int activityQuestionSetId = 0;
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
                    dbSqlCommand.CommandText = "CreateActivityQuestionSet";
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", request.ActivityCategoryMappingId);
                    dbSqlCommand.Parameters.AddWithValue("@userId", request.UserId);
                    dbSqlCommand.Parameters.AddWithValue("@StateId", request.Stateid);
                    dbSqlCommand.Parameters.AddWithValue("@DistrictId", request.DistrictId);
                    dbSqlCommand.Parameters.AddWithValue("@BlockId", request.BlockId);
                    dbSqlCommand.Parameters.AddWithValue("@VillageId", request.VillageId);
                    dbSqlCommand.Parameters.AddWithValue("@InterventionStartDate", request.InterventionStartDate);
                    dbSqlCommand.Parameters.AddWithValue("@Name", request.Name);
                    dbSqlCommand.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                    dbSqlCommand.Parameters.Add("@activityQuestionSetId", SqlDbType.Int);
                    dbSqlCommand.Parameters["@activityQuestionSetId"].Direction = ParameterDirection.Output;

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    activityQuestionSetId = Convert.ToInt32(dbSqlCommand.Parameters["@activityQuestionSetId"].Value);
                    dbSqlconnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return activityQuestionSetId;
        }

        public int UpdateActivityQuestionSetValue(int activityQuestionSetId, int userId, DataTable activityQuestionSetValue)
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
                    dbSqlCommand.CommandText = "UpdateActivityQuestionSetValue";
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("activityQuestionSetId", activityQuestionSetId);
                    dbSqlCommand.Parameters.Add("@ActivityQuestionSetValue", SqlDbType.Structured).Value = activityQuestionSetValue;
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    dbSqlconnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return activityQuestionSetId;
        }

        public DataTable GetActivityQuestionValue(int activityQuestionSetId)
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
                    dbSqlCommand.CommandText = "GetActivityQuestionSetValue";
                    dbSqlCommand.Parameters.AddWithValue("@ActivityQuestionSetId", activityQuestionSetId);

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

        public DataTable GetActivityQuestionSets(int activityQuestionSetId, int flag)
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
                    dbSqlCommand.CommandText = "GetActivityQuestionSets";
                    dbSqlCommand.Parameters.AddWithValue("@activityQuestionSetId", activityQuestionSetId);
                    dbSqlCommand.Parameters.AddWithValue("@flag", flag);

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

        public DataTable GetActivityQuestionValueSet(int activityQuestionSetId, int flag)
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
                    dbSqlCommand.CommandText = "GetActivityQuestionValueSet";
                    dbSqlCommand.Parameters.AddWithValue("@activityQuestionSetId", activityQuestionSetId);
                    dbSqlCommand.Parameters.AddWithValue("@flag", flag);

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

        public bool MarkActivityQuestionSetPrimary(int activityQuestionSetId, int activityQuestionId, int sno, bool primary)
        {
            bool result = false;
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
                    dbSqlCommand.CommandText = "MarkActivityQuestionSetPrimary";
                    dbSqlCommand.Parameters.AddWithValue("activityQuestionSetId", activityQuestionSetId);
                    dbSqlCommand.Parameters.AddWithValue("activityQuestionId", activityQuestionId);
                    dbSqlCommand.Parameters.AddWithValue("sno", sno);
                    dbSqlCommand.Parameters.AddWithValue("primary", primary);

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    dbSqlconnection.Close();
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool AddActivityQuestionSet(int activityQuestionSetId, int userId, int flag)
        {
            bool result = false;
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
                    dbSqlCommand.CommandText = "AddActivityQuestionSet";
                    dbSqlCommand.Parameters.AddWithValue("activityQuestionSetId", activityQuestionSetId);
                    dbSqlCommand.Parameters.AddWithValue("userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("flag", flag);

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    dbSqlconnection.Close();
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool DeleteActivityQuestionSet(int activityQuestionSetId, int userId, int sno, int flag)
        {
            bool result = false;
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
                    dbSqlCommand.CommandText = "DeleteActivityQuestionSet";
                    dbSqlCommand.Parameters.AddWithValue("activityQuestionSetId", activityQuestionSetId);
                    dbSqlCommand.Parameters.AddWithValue("userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("sno", sno);
                    dbSqlCommand.Parameters.AddWithValue("flag", flag);

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    dbSqlconnection.Close();
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public DataTable GetActivityUserLocation(int activityQuestionSetId)
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
                    dbSqlCommand.CommandText = "GetActivityUserLocation";
                    dbSqlCommand.Parameters.AddWithValue("@activityQuestionSetId", activityQuestionSetId);

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

        public void InsertActivityDocument(ActivityDocumentModel model)
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
                    dbSqlCommand.CommandText = "InsertActivityDocument";
                    dbSqlCommand.Parameters.AddWithValue("@activityQuestionSetId", model.activityQuestionSetId);
                    dbSqlCommand.Parameters.AddWithValue("@tagIds", model.tagIds);
                    dbSqlCommand.Parameters.AddWithValue("@originalDocumentName", model.originalDocumentName);
                    dbSqlCommand.Parameters.AddWithValue("@internalDocumentName", model.internalDocumentName);
                    dbSqlCommand.Parameters.AddWithValue("@documentDescription", model.documentDescription);
                    dbSqlCommand.Parameters.AddWithValue("@sort", model.sort);
                    dbSqlCommand.Parameters.AddWithValue("@isActive", model.isActive);
                    dbSqlCommand.Parameters.AddWithValue("@createdBy", model.createdBy);

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    dbSqlconnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetActivityDocument(int activityQuestionSetId)
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
                    dbSqlCommand.CommandText = "GetActivityDocument";
                    dbSqlCommand.Parameters.AddWithValue("@activityQuestionSetId", activityQuestionSetId);

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

        public void DeleteActivityDocument(int activityDocumentId)
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
                    dbSqlCommand.CommandText = "DeleteActivityDocument";
                    dbSqlCommand.Parameters.AddWithValue("@activityDocumentId", activityDocumentId);

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    dbSqlconnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ValidateActivityQuestionSet(int activityQuestionSetId)
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
                    dbSqlCommand.CommandText = "ValidateActivityQuestionSet";
                    dbSqlCommand.Parameters.AddWithValue("@activityQuestionSetId", activityQuestionSetId);

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

        public DataTable GetActivityQuestionDueHistory(int activityQuestionSetId, int activityQuestionId, int sno)
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
                    dbSqlCommand.CommandText = "GetActivityQuestionDueHistory";
                    dbSqlCommand.Parameters.AddWithValue("@activityQuestionSetId", activityQuestionSetId);
                    dbSqlCommand.Parameters.AddWithValue("@activityQuestionId", activityQuestionId);
                    dbSqlCommand.Parameters.AddWithValue("@sno", sno);

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

        public bool CheckUniqueHouseHold(string name, string mobile, int activityQuestionSetId)
        {
            bool isExists;
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
                    dbSqlCommand.CommandText = "CheckUniqueHouseHold";
                    dbSqlCommand.Parameters.AddWithValue("@name", name);
                    dbSqlCommand.Parameters.AddWithValue("@mobile", mobile);
                    dbSqlCommand.Parameters.AddWithValue("@activityQuestionSetId", activityQuestionSetId);
                    dbSqlCommand.Parameters.Add("@isExists", SqlDbType.Bit);
                    dbSqlCommand.Parameters["@isExists"].Direction = ParameterDirection.Output;

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    isExists = Convert.ToBoolean(dbSqlCommand.Parameters["@isExists"].Value);
                    dbSqlconnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return isExists;
        }

        public bool CheckUniqueHouseHoldBeforeCreation(int stateId, int districtId, int? blockId, int? villageId, string name, string mobile)
        {
            bool isExists;
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
                    dbSqlCommand.CommandText = "CheckUniqueHouseHoldBeforeCreation";
                    dbSqlCommand.Parameters.AddWithValue("@StateId", stateId);
                    dbSqlCommand.Parameters.AddWithValue("@DistrictId", districtId);
                    dbSqlCommand.Parameters.AddWithValue("@BlockId", blockId);
                    dbSqlCommand.Parameters.AddWithValue("@VillageId", villageId);
                    dbSqlCommand.Parameters.AddWithValue("@Name", name);
                    dbSqlCommand.Parameters.AddWithValue("@MobileNo", mobile);
                    dbSqlCommand.Parameters.Add("@isExists", SqlDbType.Bit);
                    dbSqlCommand.Parameters["@isExists"].Direction = ParameterDirection.Output;

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    isExists = Convert.ToBoolean(dbSqlCommand.Parameters["@isExists"].Value);
                    dbSqlconnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return isExists;
        }

        public bool SubmitActivityQuestionSet(int activityQuestionSetId, int status, int userId)
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
                    dbSqlCommand.CommandText = "SubmitActivityQuestionSet";
                    dbSqlCommand.Parameters.AddWithValue("@activityQuestionSetId", activityQuestionSetId);
                    dbSqlCommand.Parameters.AddWithValue("@status", status);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    dbSqlconnection.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable GetActivityQuestionSetGroup(int activityQuestionSetId)
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
                    dbSqlCommand.CommandText = "GetActivityQuestionSetGroup";
                    dbSqlCommand.Parameters.AddWithValue("@activityQuestionSetId", activityQuestionSetId); dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
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

        public bool AddActivityQuestionSetGroup(int activityQuestionSetId, int userId, string groupIds)
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
                    dbSqlCommand.CommandText = "InsertActivityQuestionSetGroup";
                    dbSqlCommand.Parameters.AddWithValue("@activityQuestionSetId", activityQuestionSetId);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@groupIds", groupIds);

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    dbSqlconnection.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable GetUserLocationCount(int userId)
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
                    dbSqlCommand.CommandText = "GetUserLocationCount";
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

        public DataTable GetActivityDates(int activityCategoryMappingId)
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
                    dbSqlCommand.CommandText = "GetActivityDates";
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMappingId", activityCategoryMappingId);

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

        public int GetActivityTotalDues(int activityQuestionSetId)
        {
            int isExists = 0;
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
                    dbSqlCommand.CommandText = "GetActivityTotalDues";
                    dbSqlCommand.Parameters.AddWithValue("@ActivityQuestionSetId", activityQuestionSetId);
                    dbSqlCommand.Parameters.Add("@count", SqlDbType.Int);
                    dbSqlCommand.Parameters["@count"].Direction = ParameterDirection.Output;

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    isExists = Convert.ToInt32(dbSqlCommand.Parameters["@count"].Value);
                    dbSqlconnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return isExists;
        }

        public DataTable GetHouseholdActionLog(string activityCategoryIds, string userIds, DateTime fromdate, DateTime todate)
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
                        CommandText = "GetHouseholdActionLog"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryIds", activityCategoryIds);
                    dbSqlCommand.Parameters.AddWithValue("@userIds", userIds);
                    dbSqlCommand.Parameters.AddWithValue("@StartDateTime", fromdate);
                    dbSqlCommand.Parameters.AddWithValue("@EndDateTime", todate);

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

        public DataTable GetHouseholdMonthlyActionLog(string activityCategoryIds, string userIds, DateTime fromdate, DateTime todate)
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
                        CommandText = "GetHouseholdMonthlyActionLog"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryIds", activityCategoryIds);
                    dbSqlCommand.Parameters.AddWithValue("@userIds", userIds);
                    dbSqlCommand.Parameters.AddWithValue("@StartDateTime", fromdate);
                    dbSqlCommand.Parameters.AddWithValue("@EndDateTime", todate);

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

        public async Task<bool> CreateUserActionLogHH(int userId, DateTime actionLogTime, int activityQuestionSetId, int status)
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
                        CommandText = "CreateUserActionLogHH"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@UserId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@ActionLogTime", actionLogTime);
                    dbSqlCommand.Parameters.AddWithValue("@ActivityQuestionSetId", activityQuestionSetId);
                    dbSqlCommand.Parameters.AddWithValue("@Status", status);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    await dbSqlCommand.ExecuteNonQueryAsync();
                    await dbSqlconnection.CloseAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteActivityQuestionSet(int activityQuestionSetId, int userId)
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
                        CommandText = "delete_ActivityQuestionSetId"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@ActivityQuestionSetId", activityQuestionSetId);                  
                    dbSqlCommand.Parameters.AddWithValue("@deleteBy", userId);                  
                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    await dbSqlCommand.ExecuteNonQueryAsync();
                    await dbSqlconnection.CloseAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
