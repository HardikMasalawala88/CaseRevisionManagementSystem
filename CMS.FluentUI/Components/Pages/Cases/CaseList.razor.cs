using CMS.Data.ContextModels;
using CMS.Services;
using CMS.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CMS.FluentUI.Components.Pages.Cases
{
    public partial class CaseList
    {
        [Inject] public ICaseService CaseService {get; set;}
        [Inject] public NavigationManager NavigationManager {get; set;}
        [Inject] public IJSRuntime Js {get; set;}

        private List<Case> caseList = new();
        private List<Case> searchCaseData = new();
        private Case caseData = new();
        private DateTime fromDate;
        private DateTime toDate;
        DateTime date = DateTime.Now;
        HorizontalAlignment Horizontal;

        protected override void OnInitialized()
        {
            GetCase();
            fromDate = new DateTime(date.Year, date.Month, 1);
            toDate = fromDate.AddMonths(1).AddDays(-1);
        }

        private void GetCase()
        {
            caseList = CaseService.ListCaseDetail();
            searchCaseData = caseList;
        }

        private void OnClick(string redirectUrl)
        {
            NavigationManager.NavigateTo(redirectUrl);
        }

        private async Task PrintCaseList()
        {
            await Js.InvokeVoidAsync("printTable");
        }

        protected void FilterCases()
        {
            caseList = searchCaseData
                .Where(x => x.HearingDate.Date >= fromDate && x.HearingDate.Date <= toDate)
                .ToList();
        }

        protected void DeleteConfirm(long caseId)
        {
            bool isDeleted = CaseService.RemoveCaseDetail(caseId);
            NavigationManager.NavigateTo("case/caselist");
        }
    }
}
