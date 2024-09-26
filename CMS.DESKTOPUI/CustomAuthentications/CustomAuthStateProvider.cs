using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.DESKTOPUI.CustomAuthentications;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace CMS.DESKTOPUI.CustomAuthentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal currentUser;
        private readonly ProtectedSessionStorage _session;

        public CustomAuthStateProvider(ProtectedSessionStorage session)
        {
            // Initialize your authentication state as needed
            currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            _session = session;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSessionStorageData = await _session.GetAsync<UserSession>("UserSession");
                var userSession = userSessionStorageData.Success ? userSessionStorageData.Value : null;

                if (userSession == null)
                {
                    return await Task.FromResult(new AuthenticationState(currentUser));
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.Username),
                new Claim(ClaimTypes.Role, userSession.Role)
                // Add additional claims as needed
            }, "CustomAuth"));

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(currentUser));
            }
        }
          
        public async Task UpdateAuthneticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if(userSession != null)
            {
                await _session.SetAsync("UserSession", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Username),
                    new Claim(ClaimTypes.Role, userSession.Role)
                    // Add additional claims as needed
                }));
            }
            else
            {
                await _session.DeleteAsync("UserSession");
                claimsPrincipal = currentUser;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task LogInAsync(ClaimsPrincipal user)
        {
            //var loginTask = LogInAsyncCore();
            currentUser = user;
            //var authenticationState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync()); //Task.FromResult(new AuthenticationState(user)));

            await Task.FromResult(user);
        }

        //private async Task<AuthenticationState> LogInAsyncCore()
        //{
        //    var user = await LoginWithExternalProviderAsync();
        //    currentUser = user;

        //    return new AuthenticationState(currentUser);
        //}

        //private Task<ClaimsPrincipal> LoginWithExternalProviderAsync()
        //{
        //    var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity());

        //    return Task.FromResult(authenticatedUser);
        //}

        public void Logout()
        {
            currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(currentUser)));
        }
    }
}
