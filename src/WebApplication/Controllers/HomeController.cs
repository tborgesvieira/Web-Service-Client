using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]        
        public ActionResult Index(Models.Cep cep)
        {
            if (!ModelState.IsValid)
            {
                return View(cep);
            }

            using (var correios = new Correios.AtendeClienteClient())
            {
                var consulta = correios.consultaCEP(cep.Codigo.Replace("-", ""));

                if (consulta != null)
                {
                    ViewBag.Endereco = new Models.Endereco()
                    {
                        Descricao = consulta.end,
                        Complemento = consulta.complemento,
                        Bairro = consulta.bairro,
                        Cidade = consulta.cidade,
                        UF = consulta.uf
                    };
                }                

            }

            return View(cep);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}