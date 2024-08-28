using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.Clientes.Commands.UpdateClientCommand
{
    public class UpdateClientCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
    }

    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateClienteCommandHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repositoryAsync.GetByIdAsync(request.Id);

            if (cliente == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                cliente.Nombre = request.Nombre;
                cliente.Telefono = request.Telefono;
                await _repositoryAsync.UpdateAsync(cliente);
                return new Response<int>(cliente.Id);
            }
           
        }
    }
}
