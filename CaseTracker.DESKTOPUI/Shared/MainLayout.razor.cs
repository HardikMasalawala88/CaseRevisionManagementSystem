using CaseTracker.DESKTOPUI.CustomAuthentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

namespace CaseTracker.DESKTOPUI.Shared
{
    public partial class MainLayout
    {
        private bool collapseNavMenu = true;
        private bool _drawerOpen = true;
        private ErrorBoundary errorBoundary = new ErrorBoundary();

        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private async Task Logout()
        {
            var customAuthStateProvider = (CustomAuthStateProvider)AuthenticationStateProvider;
            await customAuthStateProvider.UpdateAuthneticationState(null);
            NavigationManager.NavigateTo("/", true);
        }
    }
}
