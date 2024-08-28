using Application.Features.Clientes.Commands.CreateClientCommand;
using Application.Features.Clientes.Commands.DeleteClientCommand;
using Application.Features.Clientes.Commands.UpdateClientCommand;
using Application.Features.Clientes.Queries.GetAllClientes;
using Application.Features.Clientes.Queries.GetClienteById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClienteController : BaseApiController
    {

        //GET api/<controller>/1
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetAllClientesParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllClientesQuery() { 
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Nombre = filter.Nombre ,
                Telefono = filter.Telefono
            }));
        }


        //GET api/<controller>/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetClienteByIdQuery() {Id = id }));
        }

        //POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task <IActionResult> Post(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //PUT api/<controller>/1
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateClientCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        //DELETE api/<controller>/1
        [HttpDelete("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteClientCommand() { Id = id}));
        }


    }
}
