using Application.DTOs.Users;
using Application.Features.Users.Commands.AuthenticateUserCommand;
using Application.Features.Users.Commands.RegisterUserCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await Mediator.Send(new AuthenticateUserCommand
            {
                Email = request.Email,
                Password = request.Password,
                IpAddress = GenerateIpAdress(),
            }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await Mediator.Send(new RegisterUserCommand
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                Origin = Request.Headers ["origin"],
            }));
        }
        
        private string GenerateIpAdress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
