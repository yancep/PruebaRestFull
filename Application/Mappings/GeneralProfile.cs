using Application.DTOs;
using Application.Features.Clientes.Commands.CreateClientCommand;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() {
            #region DTOs
            CreateMap<Cliente, ClienteDto>();
            #endregion
            #region Commands
            CreateMap<CreateClientCommand, Cliente>();
            #endregion
        }

    }
}
