using Proyecto.Models;

namespace Proyecto.Services;

public interface IUserService
{
    Task<IEnumerable<Usuario>> GetAllUsersAsync();
    Task<Usuario?> GetUserByIdAsync(int id);
    Task<Usuario> CreateUserAsync(Usuario usuario);
    Task<Usuario> UpdateUserAsync(Usuario usuario);
    Task<bool> UsernameExistsAsync(string username, int? excludeId = null);
    Task<Usuario?> AuthenticateAsync(string username, string password);
}