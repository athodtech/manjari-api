using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("Notification")]
    public partial class Notification
    {
        [Key]
        [Column("NotificationID")]
        public int NotificationId { get; set; }
        [Column("Notification")]
        [StringLength(500)]
        public string Notification1 { get; set; }
        public int? NotificationTo { get; set; }
        [StringLength(500)]
        public string Url { get; set; }
        public int? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.NotificationCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(NotificationTo))]
        [InverseProperty(nameof(User.NotificationNotificationToNavigations))]
        public virtual User NotificationToNavigation { get; set; }
    }
}
