using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class PagedClientesSpecification: Specification<Cliente>
    {
        public PagedClientesSpecification(int pageSize, int pageNumber, string nombre, string telefono)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.Nombre, "%" + nombre + "%");
            if (!string.IsNullOrEmpty(telefono))
                Query.Search(x => x.Telefono, "%" + telefono + "%");

        }
    }
}
