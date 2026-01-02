using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Keyless]
    [Table("qq")]
    public partial class Qq
    {
        public int? QuestionId { get; set; }
    }
}
