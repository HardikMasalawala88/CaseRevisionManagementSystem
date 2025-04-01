using CMS.Data.FormModels;
using Microsoft.AspNetCore.Components;

namespace CMS.FluentUI.Components.Pages.Account
{
    //public partial class Login
    //{
    //    //[SupplyParameterFromForm]
    //    //private Starship starship { get; set; } = new();

    //    //protected override void OnInitialized()
    //    //{
    //    //    starship = new();
    //    //    starship.ProductionDate = DateTime.Now;
    //    //}

    //    //private void HandleValidSubmit()
    //    //{
    //    //    var data = starship;
    //    //    // Process the valid form
    //    //}

    //    //[Inject] public IAccountService AccountService { get; set; }
    //    //[Inject] public NavigationManager NavigationManager { get; set; }
    //    //[Inject] public ProtectedSessionStorage Session { get; set; }
    //    //[Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    //    //[SupplyParameterFromForm]
    //    //public LoginFM loginFM { get; set; } = new();

    //    //private bool loading;

    //    //private async void OnValidSubmit()
    //    //{
    //    //    loading = true;
    //    //    try
    //    //    {
    //    //        var loggedInUser = AccountService.Login(loginFM);
    //    //        var customAuthStateProvider = (CustomAuthStateProvider)AuthenticationStateProvider;

    //    //        if (loggedInUser is not null)
    //    //        {
    //    //            await customAuthStateProvider.UpdateAuthneticationState(new UserSession
    //    //            {
    //    //                Username = loggedInUser.Username,
    //    //                Role = loggedInUser.Role
    //    //            });

    //    //            await Session.SetAsync("User", loggedInUser.Username);
    //    //        }

    //    //        NavigationManager.NavigateTo("/", true);
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        loading = false;
    //    //        StateHasChanged();
    //    //    }
    //    //}
    //}
}
