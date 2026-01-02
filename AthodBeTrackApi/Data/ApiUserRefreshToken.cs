using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ApiUserRefreshToken")]
    [Microsoft.EntityFrameworkCore.Index(nameof(Token), nameof(UserId), Name = "UC_ApiUserRefreshToken", IsUnique = true)]
    public partial class ApiUserRefreshToken
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        public string Token { get; set; }
        [StringLength(250)]
        public string RefreshToken { get; set; }
        [StringLength(256)]
        public string SessionId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExpirationDate { get; set; }
        [StringLength(50)]
        public string IpAddress { get; set; }
        public bool? IsInvalidated { get; set; }
        public int UserId { get; set; }
    }
}
