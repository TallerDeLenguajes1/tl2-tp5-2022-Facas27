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

namespace CadeteriaWeb.Controllers
{
    
    public class CadetesController : Controller
    {
        private readonly ILogger<CadetesController> _logger;
        private readonly IMapper _mapper;

        public CadetesController(ILogger<CadetesController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
       
    
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FormAlta(){
            return View(new CadetesViewModels());
        }
        [HttpPost]
        public IActionResult FormAlta(CadetesViewModels CadeteView){
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
            
            return View(new CadetesRepositorio().TodosCadetes());
        }
        public IActionResult EliminarCadetes(){
            return View();
        }
        [HttpPost]
        public IActionResult EliminarCadetes(string ID){
            CadetesRepositorio CadeRepo = new CadetesRepositorio();
            if (CadeRepo.EliminarCadetes(ID))
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
        [Route("/Cadetes/EditarCadetes/{id}")]
        public IActionResult EditarCadetes(string id){
            CadetesRepositorio CadeRepo = new CadetesRepositorio();
            Cadetes Cadete = CadeRepo.CadetePorID(Int32.Parse(id));
            CadeRepo.EliminarCadetes(id);
            CadetesViewModels CadeteViewM = _mapper.Map<CadetesViewModels>(Cadete);



            return View(CadeteViewM);
        }
        [HttpPost]
        public IActionResult EditarCadetes(CadetesViewModels CadeteView){
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