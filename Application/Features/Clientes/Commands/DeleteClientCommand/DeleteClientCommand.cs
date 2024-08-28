using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.Clientes.Commands.DeleteClientCommand
{
    public class DeleteClientCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteClienteCommandHandler : IRequestHandler<DeleteClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;
        private readonly IMapper _mapper;

        public DeleteClienteCommandHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repositoryAsync.GetByIdAsync(request.Id);

            if (cliente == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
 
                await _repositoryAsync.DeleteAsync(cliente);
                return new Response<int>(cliente.Id);
            }

        }
    }
}
