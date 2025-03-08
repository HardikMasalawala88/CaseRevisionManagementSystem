using CMS.Data.Enum;

namespace CMS.Data.ParameterModels
{
    public class PaginationSettings
    {
        private int _page;
        private int _pageSize;
        private string _sortLabel;
        private EnumListSortDirection _sortDirection;

        public int Page
        {
            get { return _page; }
            set { _page = value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public string SortLabel
        {
            get { return _sortLabel; }
            set { _sortLabel = value; }
        }

        public EnumListSortDirection SortDirection
        {
            get { return _sortDirection; }
            set { _sortDirection = value; }
        }

        // Constructor with optional parameters
        public PaginationSettings(int page, int pageSize, string sortLabel = null, EnumListSortDirection sortDirection = EnumListSortDirection.None)
        {
            _page = page;
            _pageSize = pageSize;
            _sortLabel = sortLabel;
            _sortDirection = sortDirection;
        }
    }
}
