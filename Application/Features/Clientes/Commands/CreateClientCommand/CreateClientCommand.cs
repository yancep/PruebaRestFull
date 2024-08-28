using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Commands.CreateClientCommand
{
    public class CreateClientCommand : IRequest<Response<int>>
    {
        public string Nombre { get; set; }

        public string Telefono { get; set; }
    }

    public class ClienteCommandHandler : IRequestHandler<CreateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;
        private readonly IMapper _mapper;

        public ClienteCommandHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper) {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {

            var nuevoRegistro = _mapper.Map<Cliente>(request);
            var data = _repositoryAsync.AddAsync(nuevoRegistro);

            return new Response<int>(data.Id);
        }
    }
}
  