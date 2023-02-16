using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CadeteriaWeb.Models.ClientesModels;
using AutoMapper;
using CadeteriaWeb.ViewModels;
using CadeteriaWeb.Models.PedidosModels;
using CadeteriaWeb.Models.CadetesModels;

namespace CadeteriaWeb.Controllers
{
    
     public class ClientesController : Controller
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IMapper _mapper;
        private readonly IPedidos PediRepo;
        private readonly IClientes ClienteRepo;
        private readonly ICadetes CadeRepo;

        public ClientesController(ILogger<PedidosController> logger, IMapper mapper, IPedidos pediRepo, IClientes clienterepo, ICadetes caderepo)
        {
            _logger = logger;
            _mapper = mapper;
            PediRepo = pediRepo;
            ClienteRepo = clienterepo;
            CadeRepo = caderepo;

        }

        public ActionResult Index()
        {
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

        public ActionResult SubirClientes(){
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new ClientesViewModels());

        }


        [HttpPost]
        public IActionResult SubirClientes(ClientesViewModels ClienteView){
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
                    Clientes cliente = _mapper.Map<Clientes>(ClienteView);
                    ClienteRepo.SubirClientes(cliente);
                    
                }

                return View("Index");



        }

        public ActionResult MostrarClientes(){

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(ClienteRepo.TodosCliente());
        }

        [HttpGet]
        [Route("/Clientes/EditarClientes/{ID}")]
        public ActionResult EditarClientes(string id){
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            Clientes cliente = ClienteRepo.ClientePorID(Int32.Parse(id));
            ClienteRepo.EliminarClientes(id);
            ClientesViewModels ClienteViewM = _mapper.Map<ClientesViewModels>(cliente);

            return View(ClienteViewM);  
        }

        [HttpPost]
        public IActionResult EditarClientes(ClientesViewModels ClienteView){
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
                    Clientes cliente = _mapper.Map<Clientes>(ClienteView);
                    ClienteRepo.SubirClientes(cliente);
                }

            return View("Index");
        }
        public IActionResult EliminarClientes(){
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
        [Route("/Clientes/EliminarClientes/{ID}")]
        public IActionResult EliminarClientes(string id){
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ClienteRepo.EliminarClientes(id))
            {
                ViewData["Resultado"] = "Eliminado con exito";
                return View("Index");
            }else
            {
                ViewData["Resultado"] = "No se pudo eliminar";
                return View("Index");
            }  
         }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View("Error!");
            }
 }
}