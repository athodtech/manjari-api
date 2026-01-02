using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public class DropdownRepository : BaseRepository, IDropdownRepository
    {
        private readonly IConfiguration _configuration;

        public DropdownRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GetUserLocationLevel(int userId)
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
                        CommandText = "GetUserLocationDropdown"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@flag", 5);

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

        public DataTable RPT_GetUserLocationStates(int userId)
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
                        CommandText = "GetUserLocationDropdown"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@flag", 0);

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

        public DataTable GetUserLocationStates(int userId)
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
                        CommandText = "GetUserLocationDropdown"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@flag", 1);

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
        public DataTable GetUserLocationDistricts(int userId, int stateId)
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
                        CommandText = "GetUserLocationDropdown"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@flag", 2);
                    dbSqlCommand.Parameters.AddWithValue("@stateId", stateId);

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

        public DataTable GetUserLocationBlocks(int userId, int stateId, int districtId)
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
                        CommandText = "GetUserLocationDropdown"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@flag", 3);
                    dbSqlCommand.Parameters.AddWithValue("@stateId", stateId);
                    dbSqlCommand.Parameters.AddWithValue("@districtId", districtId);

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

        public DataTable GetUserLocationVillages(int userId, int stateId, int districtId, int blockId)
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
                        CommandText = "GetUserLocationDropdown"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@flag", 4);
                    dbSqlCommand.Parameters.AddWithValue("@stateId", stateId);
                    dbSqlCommand.Parameters.AddWithValue("@districtId", districtId);
                    dbSqlCommand.Parameters.AddWithValue("@blockId", blockId);

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

        public DataTable GetActivityQuestionDll()
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
                        CommandText = "GetActivityQuestionDll"
                    };
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

        public DataTable GetActivityQuestionByGroupId(string groupIds)
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
                    dbSqlCommand = new SqlCommand();
                    dbSqlCommand.Connection = dbSqlconnection;
                    dbSqlCommand.CommandTimeout = 0;
                    dbSqlCommand.CommandType = CommandType.StoredProcedure;
                    dbSqlCommand.CommandText = "RPT_GetActivityQuestionByGroupId";
                    dbSqlCommand.Parameters.AddWithValue("@groupIds", groupIds);
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

        public DataTable RPT_GetLocation(int? userId)
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
                    dbSqlCommand = new SqlCommand();
                    dbSqlCommand.Connection = dbSqlconnection;
                    dbSqlCommand.CommandTimeout = 0;
                    dbSqlCommand.CommandType = CommandType.StoredProcedure;
                    dbSqlCommand.CommandText = "RPT_GetLocation";
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

        public DataTable GetAssignUserLocationDropdown(string flag, string ids)
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
                    dbSqlCommand = new SqlCommand();
                    dbSqlCommand.Connection = dbSqlconnection;
                    dbSqlCommand.CommandTimeout = 0;
                    dbSqlCommand.CommandType = CommandType.StoredProcedure;
                    dbSqlCommand.CommandText = "GetAssignUserLocationDropdown";
                    dbSqlCommand.Parameters.AddWithValue("@flag", flag);
                    dbSqlCommand.Parameters.AddWithValue("@ids", ids);
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

        public DataTable RPT_GetAssignUserLocationDropdown(int userId,string flag, string ids)
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
                        CommandText = "RPT_GetAssignUserLocationDropdown"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@flag", flag);
                    dbSqlCommand.Parameters.AddWithValue("@ids", ids);
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

        public DataTable RPT_GetAssignUserLocationDropdown2(int userId, string flag, string sIds, string dIds, string bIds)
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
                        CommandText = "RPT_GetAssignUserLocationDropdown2"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@flag", flag);
                    dbSqlCommand.Parameters.AddWithValue("@sIds", sIds);
                    dbSqlCommand.Parameters.AddWithValue("@dIds", dIds);
                    dbSqlCommand.Parameters.AddWithValue("@bIds", bIds);
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

        public DataTable RPT_GetTagsdll(string groupIds)
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
                        CommandText = "RPT_GetTagsdll"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@groupIds", groupIds);
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

        public DataTable RPT_GetQuestiondll(string tagIds, int activityCategoryMapping)
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
                        CommandText = "RPT_GetQuestiondll"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@tagIds", tagIds);
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMapping", activityCategoryMapping);
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
      
        public DataTable RPT_GetQuestiondll(string tagIds, int activityCategoryMapping, string strWhere)
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
                        CommandText = "RPT_GetQuestiondll"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@tagIds", tagIds);
                    dbSqlCommand.Parameters.AddWithValue("@activityCategoryMapping", activityCategoryMapping);
                    dbSqlCommand.Parameters.AddWithValue("@strWhere", strWhere);
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


        public async Task<DataTable> RPT_GetQuestionGroupAsync(int userId)
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
                        CommandText = "RPT_GetQuestionGroup"
                    };
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

        public async Task<DataTable> RPT_GetTagsAsync(string groupIds, int userId)
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
                        CommandText = "RPT_GetTags"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@groupIds", groupIds);
                    dbSqlAdapter = new SqlDataAdapter(dbSqlCommand);
                    if (dbSqlconnection.State == ConnectionState.Closed)
                      await  dbSqlconnection.OpenAsync();
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

        public DataTable GetActivityWithMappingidDll(int? userId)
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
                        CommandText = "GetActivityWithMappingidDll"
                    };
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


    }

}
