using System.Threading.Tasks;
using Nop.Front.Common.Enums;
using Nop.Front.Common.Models;

namespace Nop.Front.Common.Services
{
    public interface IWebProxyService<T>
    {
        string CallPath { get; set; }
        HttpMethods HttpMethodValue { get; set; }
        string SendValue { get; set; }
        bool XmlYN { get; set; }

        Task<ApiEntityModel<T>> AsyncCallData();
        void Dispose();
    }
}