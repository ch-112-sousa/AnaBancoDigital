namespace ContaCorrenteAPI.Authentication
{
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System;

    public class JwtService
    {
        private readonly string _secret;
        private readonly int _expiryMinutes;
        private readonly string _audience;
        private readonly string _issuer;

        public JwtService(string secret, int expiryMinutes, string audience, string issuer) 
        {
            _secret = secret;
            _expiryMinutes = expiryMinutes;
            _audience = audience;
            _issuer = issuer;
        }

        public string GerarToken(UsuarioModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.NumeroContaCorrente.ToString()),
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Role, user.Perfil)  
            }),
                Expires = DateTime.UtcNow.AddMinutes(_expiryMinutes),  
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret)), SecurityAlgorithms.HmacSha256Signature),
                Audience = _audience,
                Issuer = _issuer
            };
            

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}