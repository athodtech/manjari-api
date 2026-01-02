using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories.Generic
{
  public interface IDatabaseTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
