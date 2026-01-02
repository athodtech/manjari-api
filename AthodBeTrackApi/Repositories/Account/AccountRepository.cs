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
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        private readonly IConfiguration _configuration;

        public AccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public bool VerifyUser(string sUserName, out string dbPassword)
        {
            bool isExists = false;
            dbPassword = "";
            using (_dbContext = new AthodDbContext())
            {
              
                var user = _dbContext.Users.Where(x => (x.UserName == sUserName && x.IsActive == true)).FirstOrDefault();
                if (user != null)
                {
                    if (user.UserName == sUserName)
                    {
                        dbPassword = user.Password;
                        isExists = true;
                    }
                }

            }
            return isExists;
        }

        public List<MenuItem> GetMenu(int roleId, int userId)
        {
            List<MenuItem> menuItems = new();
            var userRight = _dbContext.UserRights.FirstOrDefault(x => x.RoleId == roleId && x.UserId == userId);
            if (userRight == null)
            {

                var items = (from t1 in _dbContext.Menus
                             join t2 in _dbContext.RoleRights
                             on t1.Id equals t2.MenuId
                             where t2.RoleId == roleId && t1.IsActive == true
                             && t1.ParentId == null && t2.ShowMenu == true
                             select t1).OrderBy(x => x.Sort).ToList();

                foreach (var item in items)
                {
                    var menu = new MenuItem
                    {
                        subItems = new List<SubItem>(),
                        id = item.Id,
                        isTitle = item.IsTitle,
                        label = item.Label,
                        icon = item.Icon,
                        link = item.Link,
                        isView = item.IsView ?? false
                    };

                    var subMenu = (from t1 in _dbContext.Menus
                                   join t2 in _dbContext.RoleRights
                                   on t1.Id equals t2.MenuId
                                   where t2.RoleId == roleId && t1.IsActive == true
                                   && t1.ParentId == item.Id && t2.ShowMenu == true
                                   select t1).OrderBy(x => x.Sort).ToList();

                    foreach (var sitem in subMenu)
                    {
                        var subItem = new SubItem
                        {
                            subItems = new List<SubSubItem>(),
                            id = sitem.Id,
                            label = sitem.Label,
                            link = sitem.Link,
                            parentId = sitem.ParentId ?? 0,
                            isView = sitem.IsView ?? false
                        };

                        var ssubMenu = (from t1 in _dbContext.Menus
                                        join t2 in _dbContext.RoleRights
                                        on t1.Id equals t2.MenuId
                                        where t2.RoleId == roleId && t1.IsActive == true
                                        && t1.ParentId == sitem.Id && t2.ShowMenu == true
                                        select t1).OrderBy(x => x.Sort).ToList();

                        foreach (var ssitem in ssubMenu)
                        {
                            var ssubItem = new SubSubItem
                            {
                                id = ssitem.Id,
                                label = ssitem.Label,
                                link = ssitem.Link,
                                parentId = ssitem.ParentId ?? 0,
                                isView = ssitem.IsView ?? false
                            };
                            subItem.subItems.Add(ssubItem);
                        }
                        menu.subItems.Add(subItem);
                    }
                    menuItems.Add(menu);
                }
                return menuItems;
            }
            else
            {
                var items = (from t1 in _dbContext.Menus
                             join t2 in _dbContext.UserRights
                            on t1.Id equals t2.MenuId
                             where t2.RoleId == roleId && t2.UserId == userId && t1.IsActive == true
                             && t1.ParentId == null && t2.ShowMenu == true
                             select t1).OrderBy(x => x.Sort).ToList();

                foreach (var item in items)
                {
                    var menu = new MenuItem
                    {
                        subItems = new List<SubItem>(),
                        id = item.Id,
                        isTitle = item.IsTitle,
                        label = item.Label,
                        icon = item.Icon,
                        link = item.Link,
                        isView = item.IsView ?? false
                    };

                    var subMenu = (from t1 in _dbContext.Menus
                                   join t2 in _dbContext.UserRights
                                   on t1.Id equals t2.MenuId
                                   where t2.RoleId == roleId && t2.UserId == userId && t1.IsActive == true
                                   && t1.ParentId == item.Id && t2.ShowMenu == true
                                   select t1).OrderBy(x => x.Sort).ToList();

                    foreach (var sitem in subMenu)
                    {
                        var subItem = new SubItem
                        {
                            subItems = new List<SubSubItem>(),
                            id = sitem.Id,
                            label = sitem.Label,
                            link = sitem.Link,
                            parentId = sitem.ParentId ?? 0,
                            isView = sitem.IsView ?? false
                        };

                        var ssubMenu = (from t1 in _dbContext.Menus
                                        join t2 in _dbContext.UserRights
                                        on t1.Id equals t2.MenuId
                                        where t2.RoleId == roleId && t2.UserId == userId && t1.IsActive == true
                                        && t1.ParentId == sitem.Id && t2.ShowMenu == true
                                        select t1).OrderBy(x => x.Sort).ToList();

                        foreach (var ssitem in ssubMenu)
                        {
                            var ssubItem = new SubSubItem
                            {
                                id = ssitem.Id,
                                label = ssitem.Label,
                                link = ssitem.Link,
                                parentId = ssitem.ParentId ?? 0,
                                isView = ssitem.IsView ?? false
                            };
                            subItem.subItems.Add(ssubItem);
                        }
                        menu.subItems.Add(subItem);
                    }
                    menuItems.Add(menu);
                }
                return menuItems;
            }
        }

        public DataTable GetUserLocation(int userId)
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
                        CommandText = "GetUserLocation"
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

        public DataTable GetUserLogs(int? userId, int? lastDays)
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
                    dbSqlCommand.CommandText = "GetUserLogs";
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@days", lastDays);

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

        public async Task<DataTable> GetUserLogDetails(int? userId)
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
                    dbSqlCommand.CommandText = "GetUserLogDetails";
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


        public DataTable GetUserLogsWithPagination(int? userId, int pageNumber, int rowsOfPage)
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
                    dbSqlCommand.CommandText = "GetUserLogsWithPagination";
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
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
        public bool LogOutOtherDeviceUser(string userName)
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
                    dbSqlCommand.CommandText = "LogOutOtherDeviceUser";
                    dbSqlCommand.Parameters.AddWithValue("@userName", userName);
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

        public async Task<bool> LogOutOtherDeviceUserAsync(string userName)
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
                    dbSqlCommand.CommandText = "LogOutOtherDeviceUser";
                    dbSqlCommand.Parameters.AddWithValue("@userName", userName);
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
        public DataTable GetRoleRights(int roleId)
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
                    dbSqlCommand.CommandText = "GetRoleRights";
                    dbSqlCommand.Parameters.AddWithValue("@roleId", roleId);

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

        public bool AssignRoleRights(int roleId, int userId, DataTable roleRights)
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
                    dbSqlCommand.CommandText = "InsertRoleRights";
                    dbSqlCommand.Parameters.AddWithValue("@roleId", roleId);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.Add("@roleRights", SqlDbType.Structured).Value = roleRights;
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

        public DataTable GetUserRights(int roleId, int userId)
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
                    dbSqlCommand.CommandText = "GetUserRights";
                    dbSqlCommand.Parameters.AddWithValue("@roleId", roleId);
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

        public bool AssignUserRights(int roleId, int userId, int cretedBy, DataTable userRights)
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
                    dbSqlCommand.CommandText = "InsertUserRights";
                    dbSqlCommand.Parameters.AddWithValue("@roleId", roleId);
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.AddWithValue("@cretedBy", cretedBy);
                    dbSqlCommand.Parameters.Add("@userRights", SqlDbType.Structured).Value = userRights;
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

        public DataTable GetButtonRights(int roleId, int userId)
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
                    dbSqlCommand.CommandText = "GetButtonRights";
                    dbSqlCommand.Parameters.AddWithValue("@roleId", roleId);
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

        public bool DeleteUser(int userId)
        {
            bool flag = false;
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
                    dbSqlCommand.CommandText = "DeleteUser";
                    dbSqlCommand.Parameters.AddWithValue("@userId", userId);
                    dbSqlCommand.Parameters.Add("@output", SqlDbType.Bit);
                    dbSqlCommand.Parameters["@output"].Direction = ParameterDirection.Output;

                    if (dbSqlconnection.State == ConnectionState.Closed)
                        dbSqlconnection.Open();
                    dbSqlCommand.ExecuteNonQuery();
                    flag = Convert.ToBoolean(dbSqlCommand.Parameters["@output"].Value);
                    dbSqlconnection.Close();
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ResetUserRightsAsync(int roleId, int userId)
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
                        CommandText = "ResetUserRights"
                    };
                    dbSqlCommand.Parameters.AddWithValue("@roleId", roleId);
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


    }
}
