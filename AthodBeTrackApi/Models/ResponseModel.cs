using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ResponseModel<T>
    {
        public T data { get; set; }
        public string status { get; set; }
    }
}
