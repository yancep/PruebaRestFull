using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.RegisterUserCommand
{
    public class RegisterUserCommand : IRequest<Response<string>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string ConfirmPassword { get; set; }

        public string Origin { get; set; }

        public class RegisterUserCommandHander : IRequestHandler<RegisterUserCommand, Response<string>>
            {
            private IAccountService _accountService;

            public RegisterUserCommandHander(IAccountService accountService)
            {
                _accountService = accountService;
            }
            public async Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                return await _accountService.RegisterAsync(new RegisterRequest
                {
                    Email = request.Email,
                    Password = request.Password,
                    ConfirmPassword = request.ConfirmPassword,
                    UserName = request.UserName,
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                }, request.Origin);
            }
        }
    }
}
