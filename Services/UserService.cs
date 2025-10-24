using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;
using Bycript;

namespace Proyecto.Services;

public class UserService : IUserService
{
    private readonly ProyectoContext _context;
    private readonly IBCryptService _bcryptService;

    public UserService(ProyectoContext context, IBCryptService bcryptService)
    {
        _context = context;
        _bcryptService = bcryptService;
    }

    public async Task<IEnumerable<Usuario>> GetAllUsersAsync()
    {
        return await _context.Usuarios
            .OrderBy(u => u.Username)
            .ToListAsync();
    }

    public async Task<Usuario?> GetUserByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario> CreateUserAsync(Usuario usuario)
    {
        usuario.Password = _bcryptService.HashText(usuario.Password);
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario> UpdateUserAsync(Usuario usuario)
    {
        var existingUser = await _context.Usuarios.FindAsync(usuario.Id);
        if (existingUser == null)
            throw new ArgumentException("Usuario no encontrado");

        existingUser.Username = usuario.Username;
        existingUser.Isadmin = usuario.Isadmin;
        
        if (!string.IsNullOrEmpty(usuario.Password))
        {
            existingUser.Password = _bcryptService.HashText(usuario.Password);
        }

        await _context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<bool> UsernameExistsAsync(string username, int? excludeId = null)
    {
        var query = _context.Usuarios.Where(u => u.Username == username);

        if (excludeId.HasValue)
        {
            query = query.Where(u => u.Id != excludeId.Value);
        }

        return await query.AnyAsync();
    }

    public async Task<Usuario?> AuthenticateAsync(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return null;

        var user = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
            return null;

        if (!user.Isadmin.HasValue || !user.Isadmin.Value)
            return null;

        bool isPasswordValid = _bcryptService.VerifyText(password, user.Password);

        if (!isPasswordValid)
            return null;

        return user;
    }
}