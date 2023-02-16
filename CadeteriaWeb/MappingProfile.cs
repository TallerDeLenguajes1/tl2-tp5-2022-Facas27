using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CadeteriaWeb.ViewModels;
using CadeteriaWeb.Models.CadetesModels;
using CadeteriaWeb.Models.PedidosModels;
using CadeteriaWeb.Models.ClientesModels;


namespace CadeteriaWeb
{
    public class MappingProfile:Profile
    {
        public MappingProfile(){
            // Transforma De Origen a Destino
            CreateMap<CadetesViewModels, Cadetes>();
            CreateMap<Cadetes, CadetesViewModels>();
            CreateMap<PedidosViewModels, Pedidos>();
            CreateMap<Pedidos, PedidosViewModels>();
            CreateMap<Clientes, ClientesViewModels>();
            CreateMap<ClientesViewModels, Clientes>();
        }
    }
}