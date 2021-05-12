using Microsoft.AspNetCore.Components;
using Nop.Domain.Members;
using Nop.Front.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Nop.Front.Application.Services
{
    public class AccountService : IAccountService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _userKey = "nop_user";

        public MemberInfo member { get; private set; }
        public AccountService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }
        public async Task Initialize()
        {
            member = await _localStorageService.GetItem<MemberInfo>(_userKey);
        }

        public async Task Login(MemberInfo model)
        {
            member = await _httpService.Post<MemberInfo>("/users/authenticate", model);
            await _localStorageService.SetItem(_userKey, member);
        }

        public async Task Logout()
        {
            member = null;
            await _localStorageService.RemoveItem(_userKey);
            _navigationManager.NavigateTo("account/login");
        }



    }

}
