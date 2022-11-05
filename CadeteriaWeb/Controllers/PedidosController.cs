using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CadeteriaWeb.ViewModels;
using CadeteriaWeb.Models.PedidosModels;

namespace CadeteriaWeb.Controllers
{
    
    public class PedidosController : Controller
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IMapper _mapper;

        public PedidosController(ILogger<PedidosController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FormAlta(){

            return View(new PedidosViewModels());
        }

        [HttpPost]
        public IActionResult FormAlta(PedidosViewModels PedidoView){
            if(ModelState.IsValid)
                {
                    //Hacer el mapping
                    Pedidos Pedido = _mapper.Map<Pedidos>(PedidoView);
                    PedidosRepositorio PedRepo = new PedidosRepositorio();
                    PedRepo.SubirPedido(Pedido);
                    
                }

                return View("Index");



        }
        public IActionResult MostrarPedidos(){
            
            return View(new PedidosRepositorio().TodosPedidos());
        }
        public IActionResult EliminarPedidos(){
            return View();
        }
        [HttpGet]
        [Route("/Pedidos/EliminarPedidos/{nro}")]
        public IActionResult EliminarPedidos(string nro){
            PedidosRepositorio PediRepo = new PedidosRepositorio();
            if (PediRepo.EliminarPedido(nro))
            {
                ViewData["Resultado"] = "Eliminado con exito";
                return View("Index");
            }else
            {
                ViewData["Resultado"] = "No se pudo eliminar";
                return View("Index");
            }


        }
        [HttpGet]
        [Route("/Pedidos/EditarPedidos/{nro}")]
        public IActionResult EditarPedidos(string nro){
            PedidosRepositorio PediRepo = new PedidosRepositorio();
            Pedidos Pedido = PediRepo.PedidoPorNro(Int32.Parse(nro));
            PediRepo.EliminarPedido(nro);
            PedidosViewModels PedidoViewM = _mapper.Map<PedidosViewModels>(Pedido);



            return View(PedidoViewM);
        }
        [HttpPost]
        public IActionResult EditarPedidos(PedidosViewModels PedidoView){
            if(ModelState.IsValid)
                {
                    //Hacer el mapping
                    Pedidos Pedido = _mapper.Map<Pedidos>(PedidoView);
                    PedidosRepositorio PedRepo = new PedidosRepositorio();
                    PedRepo.SubirPedido(Pedido);
                }

            



            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}