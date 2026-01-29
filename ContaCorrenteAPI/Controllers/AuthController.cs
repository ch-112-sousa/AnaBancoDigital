using ContaCorrenteAPI.Authentication;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;

    public AuthController(IUserRepository userRepository, JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userRepository.GetUserForAuthentication(model.NumeroContaCorrente);
        var passwordHash = HashPassword(model.Senha);

        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Senha, passwordHash))
        {
            return Unauthorized("Invalid credentials");
        }

        var token = _jwtService.GenerateToken(user);
        return Ok(new { Token = token });
    }

    private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
}

