using System.Threading.Tasks;
using Nop.Common.Enums;
using Nop.Common.Models;

namespace Nop.Common.Services
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