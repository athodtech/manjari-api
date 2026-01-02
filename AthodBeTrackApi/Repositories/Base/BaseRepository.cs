using AthodBeTrackApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected internal AthodDbContext _dbContext = null;
        public BaseRepository()
        {
            _dbContext = new AthodDbContext();
        }
        public void Dispose()
        {

        }
    }
}
