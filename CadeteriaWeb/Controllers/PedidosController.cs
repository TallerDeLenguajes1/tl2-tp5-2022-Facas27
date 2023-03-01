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
using CadeteriaWeb.Models.CadetesModels;
using CadeteriaWeb.Models.ClientesModels;

namespace CadeteriaWeb.Controllers
{
    
    public class PedidosController : Controller
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IMapper _mapper;
        private readonly IPedidosRepositorio PediRepo;
        private readonly IClientesRepositorio ClienteRepo;
        private readonly ICadetesRepositorio CadeRepo;

        public PedidosController(ILogger<PedidosController> logger, IMapper mapper, IPedidosRepositorio pediRepo, IClientesRepositorio clienterepo, ICadetesRepositorio caderepo)
        {
            _logger = logger;
            _mapper = mapper;
            PediRepo = pediRepo;
            ClienteRepo = clienterepo;
            CadeRepo = caderepo;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FormAlta(){
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["clientes"] = new List<Clientes>(ClienteRepo.TodosCliente());
            ViewData["cadetes"] = new List<Cadetes>(CadeRepo.TodosCadetes());

            return View(new PedidosViewModels());
        }

        [HttpPost]
        public IActionResult FormAlta(PedidosViewModels PedidoView){
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            if(ModelState.IsValid)
                {
                    //Hacer el mapping
                    Pedidos Pedido = _mapper.Map<Pedidos>(PedidoView);
                    PediRepo.SubirPedido(Pedido);
                    
                }

                return View("Index");



        }
        public IActionResult MostrarPedidos(){
            ViewData["clientes"] = new List<Clientes>(ClienteRepo.TodosCliente());
            ViewData["cadetes"] = new List<Cadetes>(CadeRepo.TodosCadetes());
            return View(PediRepo.TodosPedidos());
        }
        public IActionResult EliminarPedidos(){
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        [Route("/Pedidos/EliminarPedidos/{nro}")]
        public IActionResult EliminarPedidos(string nro){
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
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
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            Pedidos Pedido = PediRepo.PedidoPorNro(Int32.Parse(nro));

            PedidosViewModels PedidoViewM = _mapper.Map<PedidosViewModels>(Pedido);
           
            ViewData["clientes"] = new List<Clientes>(ClienteRepo.TodosCliente());
            ViewData["cadetes"] = new List<Cadetes>(CadeRepo.TodosCadetes());


            
            return View(PedidoViewM);
        }
        [HttpPost]
        [Route("/Pedidos/EditarPedidos/{nro}")]
        public IActionResult EditarPedidos(PedidosViewModels PedidoViewM ){
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            if(ModelState.IsValid)
                {
                    //Hacer el mapping
                    Pedidos Pedido = _mapper.Map<Pedidos>(PedidoViewM);
                      

                        
                    PediRepo.ActualizarPedidos(Pedido);
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