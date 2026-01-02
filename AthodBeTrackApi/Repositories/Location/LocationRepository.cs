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
    public class LocationRepository : BaseRepository, ILocationRepository
    {
        private readonly IConfiguration _configuration;
        public LocationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> UpdateState(StateModel request)
        {
            var flag = false;
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
                        CommandText = "UpdateState"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@StateId", request.StateId);
                    dbSqlCommand.Parameters.AddWithValue("@StateName", request.StateName);
                    dbSqlCommand.Parameters.AddWithValue("@StateCode", request.StateCode);
                    dbSqlCommand.Parameters.AddWithValue("@IsActive", request.IsActive);
                    dbSqlCommand.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);
                    dbSqlCommand.Parameters.Add("@Output", SqlDbType.Bit);
                    dbSqlCommand.Parameters["@Output"].Direction = ParameterDirection.Output;

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    await dbSqlCommand.ExecuteNonQueryAsync();
                    flag = Convert.ToBoolean(dbSqlCommand.Parameters["@Output"].Value);

                    await dbSqlconnection.CloseAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;


        }

        public async Task<bool> UpdateDistrict(DistrictModel request)
        {
            var flag = false;
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
                        CommandText = "UpdateDistrict"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@StateId", request.StateId);
                    dbSqlCommand.Parameters.AddWithValue("@DistrictId", request.DistrictId);
                    dbSqlCommand.Parameters.AddWithValue("@DistrictName", request.DistrictName);
                    dbSqlCommand.Parameters.AddWithValue("@DistrictCode", request.DistrictCode);
                    dbSqlCommand.Parameters.AddWithValue("@IsActive", request.IsActive);
                    dbSqlCommand.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);
                    dbSqlCommand.Parameters.Add("@Output", SqlDbType.Bit);
                    dbSqlCommand.Parameters["@Output"].Direction = ParameterDirection.Output;

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    await dbSqlCommand.ExecuteNonQueryAsync();
                    flag = Convert.ToBoolean(dbSqlCommand.Parameters["@Output"].Value);

                    await dbSqlconnection.CloseAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;


        }

        public async Task<bool> UpdateBlock(BlockModel request)
        {
            var flag = false;
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
                        CommandText = "UpdateBlock"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@StateId", request.StateId);
                    dbSqlCommand.Parameters.AddWithValue("@DistrictId", request.DistrictId);
                    dbSqlCommand.Parameters.AddWithValue("@BlockId", request.BlockId);
                    dbSqlCommand.Parameters.AddWithValue("@BlockName", request.BlockName);
                    dbSqlCommand.Parameters.AddWithValue("@BlockCode", request.BlockCode);
                    dbSqlCommand.Parameters.AddWithValue("@IsActive", request.IsActive);
                    dbSqlCommand.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);
                    dbSqlCommand.Parameters.Add("@Output", SqlDbType.Bit);
                    dbSqlCommand.Parameters["@Output"].Direction = ParameterDirection.Output;

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    await dbSqlCommand.ExecuteNonQueryAsync();
                    flag = Convert.ToBoolean(dbSqlCommand.Parameters["@Output"].Value);

                    await dbSqlconnection.CloseAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;


        }

        public async Task<bool> UpdateVillage(VillageModel request)
        {
            var flag = false;
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
                        CommandText = "UpdateVillage"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@StateId", request.StateId);
                    dbSqlCommand.Parameters.AddWithValue("@DistrictId", request.DistrictId);
                    dbSqlCommand.Parameters.AddWithValue("@BlockId", request.BlockId);
                    dbSqlCommand.Parameters.AddWithValue("@VillageId", request.VillageId);
                    dbSqlCommand.Parameters.AddWithValue("@VillageName", request.VillageName);
                    dbSqlCommand.Parameters.AddWithValue("@VillageCode", request.VillageCode);
                    dbSqlCommand.Parameters.AddWithValue("@IsActive", request.IsActive);
                    dbSqlCommand.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);
                    dbSqlCommand.Parameters.Add("@Output", SqlDbType.Bit);
                    dbSqlCommand.Parameters["@Output"].Direction = ParameterDirection.Output;

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        await dbSqlconnection.OpenAsync();
                    await dbSqlCommand.ExecuteNonQueryAsync();
                    flag = Convert.ToBoolean(dbSqlCommand.Parameters["@Output"].Value);

                    await dbSqlconnection.CloseAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
    }
}
