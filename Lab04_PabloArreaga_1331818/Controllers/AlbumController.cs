using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text.RegularExpressions;
using Lab04_PabloArreaga_1331818.Helpers;
using System.Dynamic;

namespace Lab04_PabloArreaga_1331818.Controllers
{
    public class AlbumController : Controller
    {
        // GET: Album
        public ActionResult Index()
        {
            return View();
        }

        // GET: Album/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Album/Create
        public ActionResult Create()
        {
            return View();
        }
		//--------------------
		public ActionResult SubirDatos()
		{
			return View();
		}
		[HttpPost]
		public ActionResult SubirDatos(HttpPostedFileBase PostAlbum)
		{
			string ArchivoAlbum = string.Empty;
			if (PostAlbum != null)
			{
				string Archivo = Server.MapPath("~/Uploads/");
				if (!Directory.Exists(Archivo))
				{
					Directory.CreateDirectory(Archivo);
				}
				ArchivoAlbum = Archivo + Path.GetFileName(PostAlbum.FileName);
				string extension = Path.GetExtension(PostAlbum.FileName);
				PostAlbum.SaveAs(ArchivoAlbum);
				string csvData = System.IO.File.ReadAllText(ArchivoAlbum);
				int numeroAux = 0;
				foreach (string row in csvData.Split('\n'))
				{
					if (!string.IsNullOrEmpty(row))
					{
						if (numeroAux != 0)
						{
							String[] fields = row.Split('|');
							string Llave = fields[0];
							string Faltantes = fields[1];
							var ListRemaining = new List<int>();
							var ListCollected = new List<int>();
							var TradingList = new List<int>();
							if (!string.IsNullOrEmpty(Faltantes))
							{
								foreach (var item in Faltantes.Split(','))
								{
									int Valor = Convert.ToInt32(item);
									ListRemaining.Add(Valor);
								}
							}
							string Coleccionadas = fields[2];
							if (!string.IsNullOrEmpty(Coleccionadas))
							{
								foreach (var item in Coleccionadas.Split(','))
								{
									int Valor = Convert.ToInt32(item);
									ListCollected.Add(Valor);
								}
							}
							string Cambios = fields[3];
							if (!string.IsNullOrEmpty(Cambios))
							{
								foreach (var item in Cambios.Split(','))
								{
									int Valor = Convert.ToInt32(item);
									TradingList.Add(Valor);
								}
							}
						}
						numeroAux++;
					}
				}
			}
			return RedirectToAction("SubirDatos");
		}

        // POST: Album/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Album/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Album/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Album/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Album/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
