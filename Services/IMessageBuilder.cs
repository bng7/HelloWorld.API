using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloWorld.API.Services
{
    public interface IMessageBuilder
    {
        Task<string> BuildMessage();
    }
}