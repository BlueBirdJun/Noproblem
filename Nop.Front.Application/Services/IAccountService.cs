using Nop.Domain.Members;
using System.Threading.Tasks;

namespace Nop.Front.Application.Services
{
    public interface IAccountService
    {
        MemberInfo member { get; }

        Task Initialize();
        Task Login(MemberInfo model);
        Task Logout();
    }
}