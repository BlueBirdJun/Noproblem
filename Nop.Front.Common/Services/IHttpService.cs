using System.Threading.Tasks;

namespace Nop.Front.Common.Services
{
    public interface IHttpService
    {
        Task Delete(string uri);
        Task<T> Delete<T>(string uri);
        Task<T> Get<T>(string uri);
        Task Post(string uri, object value);
        Task<T> Post<T>(string uri, object value);
        Task Put(string uri, object value);
        Task<T> Put<T>(string uri, object value);
    }
}