using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class TitleService : INotifyPropertyChanged
    {
        //public string CurrentPageTitle { get; set; } = "Case Management System";

        private string _currentPageTitle;

        public string CurrentPageTitle
        {
            get => _currentPageTitle;
            set
            {
                if (_currentPageTitle != value)
                {
                    _currentPageTitle = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPageTitle)));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        //public async Task ChangeCurrentPageTitle(string PageTitle)
        //{
        //    if (HeaderTitleUpdateEvent != null)
        //    {
        //        await HeaderTitleUpdateEvent.Invoke(PageTitle);
        //    }
        //}
    }
}
