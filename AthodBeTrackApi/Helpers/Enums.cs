using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Helpers
{
    public enum ReportStatus
    {
        Draft = 1,
        Submitted = 2,
        Shared = 3,
    }

    public enum notificationStatus
    {
        Unread = 0,
        Readed = 1,
    }

    public enum LogPeriods
    {
        Today = 1,
        Yesterday = 2,
        Last7Days = 3,
        Last30Days = 4,
        ThisMonth = 5,
        LastMonth = 6
    }

    public enum ReportPeriods
    {
        AllTime = 0,
        Last7Days = 1,
        Last30Days = 2,
        Last90Days = 3,
        Last1Year = 4
    }

}
