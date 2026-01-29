using ContaCorrenteAPI.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

[ApiController]
[Route("api/[controller]")]
public class AutenticacaoController : ControllerBase
{
    private readonly IUsuarioRepository _userRepository;
    private readonly JwtService _jwtService;

    public AutenticacaoController(IUsuarioRepository userRepository, JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userRepository.ObterUsuarioParaAutenticacao(model.NumeroContaCorrente);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Senha, user.Senha))
        { 
            return Unauthorized("USER_UNAUTHORIZED");
        }

        var token = _jwtService.GerarToken(user);
        return Ok(new { Token = token });
    } 
}