using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace PLUploadMVC.Controllers
{
    public class HomeController : Controller
    {
        /*Exibe a página inicial do nosso exemplo*/
        public ActionResult Index()
        {
            string path = HttpContext.Server.MapPath("~/Content/Uploads/");
            var diretorio = new DirectoryInfo(path);
            var arquivos = diretorio.GetFiles("*.*");
            IList<String> listaDeArquivos = arquivos.Select(fileinfo => fileinfo.Name).ToList();
            ViewBag.Arquivos = listaDeArquivos;
            return View();
        }

        /*Exibe um exemplo mais simples de uso do plupload*/
        public ActionResult Personalizado()
        {
            return View();
        }

        /*Exibe o exemplo completo de uso do plupload*/
        public ActionResult Completo()
        {
            return View();
        }

        /*Action usada pelo plupload para enviar os arquivos*/
        [HttpPost]
        public ActionResult Upload(int? chunk, string name)
        {
            //Arquivo que o PlUpload envia.
            var fileUpload = Request.Files[0];
            //Local onde vai ficar as fotos enviadas.
            var uploadPath = Server.MapPath("~/Content/Uploads");
            //Faz um checagem se o arquivo veio correto.
            chunk = chunk ?? 0;
  
            var uploadedFilePath = Path.Combine(uploadPath, name);

            //faz o upload literalmetne do arquivo.
            using (var fs = new FileStream(uploadedFilePath, chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }

            return Content("Success", "text/plain");
        } 

    }
}
