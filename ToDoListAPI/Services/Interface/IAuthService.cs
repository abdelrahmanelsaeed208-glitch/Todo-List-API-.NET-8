using ToDoListAPI.Common;
using ToDoListAPI.DTOs.Auth;

namespace ToDoListAPI.Services.Interface
{
    public interface IAuthService
    {
        Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto);
        Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto);
    }
}
