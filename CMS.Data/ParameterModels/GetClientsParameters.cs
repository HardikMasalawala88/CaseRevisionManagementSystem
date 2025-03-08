using CMS.Data.Enum;
using System;
using System.Collections.Generic;

namespace CMS.Data.ParameterModels
{
    public class GetClientsParameters : PaginationSettings
    {
        public GetClientsParameters(int page, int pageSize, string sortLabel = null, EnumListSortDirection sortDirection = EnumListSortDirection.None, string searchStr = null,
            List<int> statusFilter = null, List<int> campaignFilter = null, string userId = "", List<string> responseLeadStatus = null,
            DateTime? startDate = null, DateTime? endDate = null, bool isRecentActivity = false) : base(page, pageSize, sortLabel, sortDirection)
        {
            SearchStr = searchStr;
            StatusFilter = statusFilter;
            CampaignFilter = campaignFilter;
            UserId = userId;
            ResponseLeadStatus = responseLeadStatus;
            StartDate = startDate;
            EndDate = endDate;
            IsRecentActivity = isRecentActivity;
        }

        public string UserId { get; set; }
        public string SearchStr { get; set; }
        public List<int> StatusFilter { get; set; }
        public List<int> CampaignFilter { get; set; }
        public List<string> ResponseLeadStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsRecentActivity { get; set; }
    }
}
