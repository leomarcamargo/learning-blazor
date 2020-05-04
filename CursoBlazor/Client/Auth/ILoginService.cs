using System.Threading.Tasks;
using CursoBlazor.Shared.DTO;

namespace CursoBlazor.Client.Auth
{
    public interface ILoginService
    {
        Task Login(UserToken userToken);
        Task Logout();
        Task ConduzirRenovacaoToken();
    }
}
