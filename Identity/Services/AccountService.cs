using Application.DTOs.Users;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
using Identity.Helpers;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace Identity.Services
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _createdTimeService;
            
        public AccountService(UserManager<ApplicationUser> userManager, JWTSettings jWTSettings, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager,IOptions<JWTSettings> jwtSettings, IDateTimeService createdTimeService)
        {
            _userManager = userManager;
            _jwtSettings = jWTSettings;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _createdTimeService = createdTimeService;
        }
        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email);
            if (usuario == null)
            {
                throw new ApiException($"No hay una cuenta registrada con el Email {request.Email}");
            }

            var result = await _signInManager.PasswordSignInAsync(request.UserName,request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded) {
                throw new ApiException($"Las credenciales del usuario {request.UserName} no son validas");
            }
            JwtSecurityToken jwtSecurityToken = await GenerateJWYToken(usuario);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = usuario.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = usuario.Email;
            response.UserName = usuario.UserName;

            var rolesList = await _userManager.GetRolesAsync(usuario).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = usuario.EmailConfirmed;

            var refreshToken = GenerateRefreshToken(ipAddress);

            response.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(response, $"Usuario Autenticado {usuario.UserName}");
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                RemplacedByToken = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress,
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-","");
        }

        private async Task<JwtSecurityToken> GenerateJWYToken(ApplicationUser usuario)
        {
            var userClaims = await _userManager.GetClaimsAsync(usuario);
            var roles = await _userManager.GetRolesAsync(usuario);

            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            string ipAddress = IpHelper.GetIpAddreess();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim("uid", usuario.Id),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var usuarioUseNameIgual = await _userManager.FindByNameAsync(request.UserName);
            if (usuarioUseNameIgual != null)
            {
                throw new ApiException($"Usuario {request.UserName} ya fue registrado");
            }
            var usuario = new ApplicationUser
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                UserName = request.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var usuarioConElMismoCorreo = await _userManager.FindByEmailAsync(request.Email);
            if (usuarioConElMismoCorreo != null)
            {
                throw new ApiException($"El email {request.Email} ya fue registrado previamente.");
            }
            else
            {
                var result = await _userManager.CreateAsync(usuario, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usuario, Roles.Basic.ToString());
                    return new Response<string>(usuario.Id, message: $"Usuario registrado exitosamente. {request.UserName}");
                }
                else
                {
                    throw new ApiException($"{result.Errors}.");
                }
            }
        }
    }
}
