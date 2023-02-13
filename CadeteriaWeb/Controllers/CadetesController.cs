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




namespace CadeteriaWeb.Controllers
{
    
    
    public class CadetesController : Controller
    {

        
        private readonly ILogger<CadetesController> _logger;
        private readonly IMapper _mapper;
        private readonly ICadetes CadeRepo;

        public CadetesController(ILogger<CadetesController> logger, IMapper mapper, ICadetes cadeRepo)
        {
            _logger = logger;
            //MAPPER
            _mapper = mapper;
            //DEPENDENCIA
            CadeRepo = cadeRepo;
        }
       
    
        public IActionResult Index()
        {
            
            return View();
        }
       //enviamos un nuevo viewmodel con la vista
        public IActionResult FormAlta(){
            return View(new CadetesViewModels());
        }
        //RECIBIMOS LOS DATOS DEL SUBMIT ENVIADO EN EL FORM DE LA VISTA FORMALTA
        [HttpPost]
        public IActionResult FormAlta(CadetesViewModels CadeteView){
            //SI EL MODELO QUE RECIBIMOS CUMPLE CON LAS CONDICIONES LO MAPEAMOS
               if(ModelState.IsValid)
                {
                    //Hacer el mapping
                    Cadetes Cadete = _mapper.Map<Cadetes>(CadeteView);
                    CadetesRepositorio CadRepo = new CadetesRepositorio();
                    CadRepo.SubirCadetes(Cadete);
                }

                return View("Index");
        }
        public IActionResult MostrarCadetes(){
            //MANDAMOS A LA VISTA LA LISTA DE TODOS LOS CADETES
            return View(new CadetesRepositorio().TodosCadetes());
        }
        public IActionResult EliminarCadetes(){
            return View();
        }
        //recibe el id que sale del mostrarcadetes para eliminar 
        [HttpGet]
        [Route("/Cadetes/EliminarCadetes/{id}")]
        public IActionResult EliminarCadetes(string ID){
            CadetesRepositorio CadeRepo = new CadetesRepositorio();
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
            CadetesRepositorio CadeRepo = new CadetesRepositorio();
            //LO BUSCAMOS AL CADETE POR EL ID
            Cadetes Cadete = CadeRepo.CadetePorID(Int32.Parse(id));
            //LO BORRAMOS 
            CadeRepo.EliminarCadetes(id);
            //LUEGO LO VOLVELMEOS VIEWMODEL Y LO PASAMOS
            CadetesViewModels CadeteViewM = _mapper.Map<CadetesViewModels>(Cadete);



            return View(CadeteViewM);
        }
        //LO MISMO QUE CON EL CREAR para mas detalles entrar a la vista
        [HttpPost]
        public IActionResult EditarCadetes(CadetesViewModels CadeteView){
            //COMPROBAMOS QUE SEAN VALIDOS LOS CAMBIOS Y LO MANDAMOS AL INDEX
            if(ModelState.IsValid)
                {
                    //Hacer el mapping
                    Cadetes Cadete = _mapper.Map<Cadetes>(CadeteView);
                    CadetesRepositorio CadRepo = new CadetesRepositorio();
                    CadRepo.SubirCadetes(Cadete);
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