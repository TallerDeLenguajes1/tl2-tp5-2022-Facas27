using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CadeteriaWeb.ViewModels;
using CadeteriaWeb.Models.CadetesModels;
using AutoMapper;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using CadeteriaWeb.Models.PedidosModels;
using CadeteriaWeb.Models.ClientesModels;




namespace CadeteriaWeb.Controllers
{
    
    
    public class CadetesController : Controller
    {

        
 private readonly ILogger<PedidosController> _logger;
        private readonly IMapper _mapper;
        private readonly IPedidosRepositorio PediRepo;
        private readonly IClientesRepositorio ClienteRepo;
        private readonly ICadetesRepositorio CadeRepo;

        public CadetesController(ILogger<PedidosController> logger, IMapper mapper, IPedidosRepositorio pediRepo, IClientesRepositorio clienterepo, ICadetesRepositorio caderepo)
        {
            _logger = logger;
            _mapper = mapper;
            PediRepo = pediRepo;
            ClienteRepo = clienterepo;
            CadeRepo = caderepo;

        }
       
    
        public IActionResult Index()
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
       //enviamos un nuevo viewmodel con la vista
        public IActionResult FormAlta(){
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new CadetesViewModels());
        }
        //RECIBIMOS LOS DATOS DEL SUBMIT ENVIADO EN EL FORM DE LA VISTA FORMALTA
        [HttpPost]
        public IActionResult FormAlta(CadetesViewModels CadeteView){
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            //SI EL MODELO QUE RECIBIMOS CUMPLE CON LAS CONDICIONES LO MAPEAMOS
               if(ModelState.IsValid)
                {
                    //Hacer el mapping
                    Cadetes Cadete = _mapper.Map<Cadetes>(CadeteView);
                    CadeRepo.SubirCadetes(Cadete);
                }

                return View("Index");
        }
        public IActionResult MostrarCadetes(){
            //MANDAMOS A LA VISTA LA LISTA DE TODOS LOS CADETES
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(CadeRepo.TodosCadetes());
        }
        public IActionResult EliminarCadetes(){
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
        //recibe el id que sale del mostrarcadetes para eliminar 
        [HttpGet]
        [Route("/Cadetes/EliminarCadetes/{id}")]
        public IActionResult EliminarCadetes(string ID){
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            if (CadeRepo.EliminarCadetes(ID))
            {
                //MANDAMOS EL RESULTADO DE LA ACCION AL INDEX 
                ViewData["Resultado"] = "Eliminado con exito";
                return View("Index");
            }else
            {
                //MANDAMOS EL RESULTADO DE LA ACCION AL INDEX 
                ViewData["Resultado"] = "No se pudo eliminar";
                return View("Index");
            }


        }
        //LO MISMO CON EL ELIMINAR
        [HttpGet]
        //esto es por el route-id en mostrarcadetes
        [Route("/Cadetes/EditarCadetes/{id}")]
        public IActionResult EditarCadetes(string id){
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            //LO BUSCAMOS AL CADETE POR EL ID
            Cadetes Cadete = CadeRepo.CadetePorID(Int32.Parse(id));
            //LUEGO LO VOLVELMEOS VIEWMODEL Y LO PASAMOS
            CadetesViewModels CadeteViewM = _mapper.Map<CadetesViewModels>(Cadete);
            



            return View(CadeteViewM);
        }
        //LO MISMO QUE CON EL CREAR para mas detalles entrar a la vista
        [HttpPost]
        public IActionResult EditarCadetes(CadetesViewModels CadeteView){
             if (string.IsNullOrEmpty(HttpContext.Session.GetString("name")))
            {
                return RedirectToAction("Index", "Logging");
            }

            if (HttpContext.Session.GetInt32("rol") != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            //COMPROBAMOS QUE SEAN VALIDOS LOS CAMBIOS Y LO MANDAMOS AL INDEX
            if(ModelState.IsValid)
                {
                    //Hacer el mapping
                    Cadetes Cadete = _mapper.Map<Cadetes>(CadeteView);
                    CadeRepo.ActualizarCadetes(Cadete);
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