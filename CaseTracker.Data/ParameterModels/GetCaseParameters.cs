using CaseTracker.Data.Enum;
using System;
using System.Collections.Generic;

namespace CaseTracker.Data.ParameterModels
{
    public class GetCaseParameters : PaginationSettings
    {
        public GetCaseParameters(int page, int pageSize, string sortLabel = null, EnumListSortDirection sortDirection = EnumListSortDirection.None,
            DateTime? startDate = null, DateTime? endDate = null) : base(page, pageSize, sortLabel, sortDirection)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
