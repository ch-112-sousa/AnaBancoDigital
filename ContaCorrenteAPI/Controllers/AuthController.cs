using ContaCorrenteAPI.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Senha, user.Senha))
        { 
            return Unauthorized("USER_UNAUTHORIZED");
        }

        var token = _jwtService.GenerateToken(user);
        return Ok(new { Token = token });
    } 
}