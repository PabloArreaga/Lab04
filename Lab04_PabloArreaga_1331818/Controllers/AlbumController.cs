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
				string ext = Path.GetExtension(PostAlbum.FileName);
				PostAlbum.SaveAs(ArchivoAlbum);
				string csvData = System.IO.File.ReadAllText(ArchivoAlbum);
				int aux = 0;
				foreach (string row in csvData.Split('\n'))
				{
					if (!string.IsNullOrEmpty(row))
					{
						if (aux != 0)
						{
							String[] fields = row.Split('|');
							string id = fields[0];
							string falta = fields[1];
							var listR = new List<int>();
							var listC = new List<int>();
							var listT = new List<int>();
							if (!string.IsNullOrEmpty(falta))
							{
								foreach (var item in falta.Split(','))
								{
									int val = Convert.ToInt32(item);
									listR.Add(val);
								}
							}
							string Coleccionadas = fields[2];
							if (!string.IsNullOrEmpty(Coleccionadas))
							{
								foreach (var item in Coleccionadas.Split(','))
								{
									int val = Convert.ToInt32(item);
									listC.Add(val);
								}
							}
							string Cambios = fields[3];
							if (!string.IsNullOrEmpty(Cambios))
							{
								foreach (var item in Cambios.Split(','))
								{
									int val = Convert.ToInt32(item);
									listT.Add(val);
								}
							}
							Data.Instance.DicAlbum.Add(id, new Models.AlbumModel
							{
								LisFaltan = listR,
								LisColeccion = listC,
								LisCambio = listT,
							});

						}
						aux++;
					}
				}
			}
			return RedirectToAction("SubirDetalle");
		}

		public ActionResult SubirDetalle()
		{
			return View();
		}
		[HttpPost]
		public ActionResult SubirDetalle (HttpPostedFileBase PostAdquirida)
		{
			string archivoCon = string.Empty;
			if (PostAdquirida != null)
			{
				string ArchivoE = Server.MapPath("~/Uploads/");
				if (!Directory.Exists(ArchivoE))
				{
					Directory.CreateDirectory(ArchivoE);
				}
				archivoCon = ArchivoE + Path.GetFileName(PostAdquirida.FileName);
				string ext = Path.GetExtension(PostAdquirida.FileName);
				PostAdquirida.SaveAs(archivoCon);
				string csvData = System.IO.File.ReadAllText(archivoCon);
				int aux = 0;
				foreach (string fila in csvData.Split('\n'))
				{
					if (!string.IsNullOrEmpty(fila))
					{
						bool Esta = false;
						if (aux != 0)
						{
							String[] campos = fila.Split('|');
							string ident = campos[0];			
							string col = campos[1];
							if (col == "true\r")
							{
								Esta = true;
							}
							else if (col == "false\r")
							{
								Esta = false;
							}
							Data.Instance.DicColec.Add(ident, Esta);
						}
						aux++;
					}
				}
			}
			return RedirectToAction("Menu");
		}
		public ActionResult Menu()
		{
			return View();
		}

		public ActionResult ClaseB()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ClaseB(Models.BuscarModel collection)
		{
			string Esta = collection.Iden;
			return RedirectToAction("BuscarClase", new { BClase = Esta });
		}

		public ActionResult BuscarClase(string BClase)
		{
			try
			{
				foreach (var author in Data.Instance.DicAlbum)
				{
					if (author.Key == BClase)
					{
						var res = String.Join(",", author.Value.LisFaltan.ToArray());
						Data.Instance.ListMostrar.Add(new Models.MostrarModel
						{
							Clase = author.Key,
							Faltan = res
						});
					}
				}
				return RedirectToAction("Estado");
			}
			catch (Exception)
			{
				throw new DriveNotFoundException();
			}
		}

		public ActionResult Estado()
		{
			return View(Data.Instance.ListMostrar);
		}

		public ActionResult AlbumB()
		{
			return View();
		}
		[HttpPost]
		public ActionResult AlbumB(Models.BuscarModel collection)
		{
			string Esta = collection.Iden;
			return RedirectToAction("BuscarAlbum", new { BAlbum = Esta });
		}

		public ActionResult BuscarAlbum(string BAlbum)
		{
			try
			{
				foreach (KeyValuePair<string, bool> author in Data.Instance.DicColec)
				{
					if (author.Key.Contains(BAlbum))
					{
						if (author.Value)
						{
							Data.Instance.LisMostrarEqui.Add(new Models.MostrarEquiModel
							{
								Nombre = author.Key,
								Adquirida = "true"
							});
						}
						else
						{
							Data.Instance.LisMostrarEqui.Add(new Models.MostrarEquiModel
							{
								Nombre = author.Key,
								Adquirida = "false"
							});
						}

					}
				}
				return RedirectToAction("GridMostrar");
			}
			catch (Exception)
			{
				throw new DriveNotFoundException();
			}
		}

		public ActionResult GridMostrar()
		{
			return View(Data.Instance.LisMostrarEqui);
		}

		public ActionResult Agregar()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Agregar(Models.BuscarModel collection)
		{
			string TipoEstampa = collection.Iden;
			string Numero = collection.NoEstampa;
			return RedirectToAction("ListAgregar", new { BAlbumr = TipoEstampa, NumeroEstampa = Numero });
		}
		public ActionResult ListAgregar(string BAlbum, string NumeroEstampa)
		{
			try
			{
				foreach (var author in Data.Instance.DicAlbum)
				{
					if (author.Key == BAlbum)
					{
						int NumeroABuscar = Convert.ToInt32(NumeroEstampa);
						if (author.Value.LisFaltan.Contains(NumeroABuscar))
						{
							author.Value.LisFaltan.Remove(NumeroABuscar);
							author.Value.LisColeccion.Add(NumeroABuscar);
							author.Value.LisColeccion.Sort();
						}
						var result = String.Join(",", author.Value.LisFaltan.ToArray());
						foreach (var item in Data.Instance.ListMostrar)
						{
							if (item.Clase == BAlbum)
							{
								item.Faltan = result;
							}
						}

					}
				}
				string IdentificadorEstampaEspecifica = BAlbum + "_" + NumeroEstampa;
				if (Data.Instance.DicColec.ContainsKey(IdentificadorEstampaEspecifica))
				{
					Data.Instance.DicColec.Remove(IdentificadorEstampaEspecifica);
					Data.Instance.DicColec.Add(IdentificadorEstampaEspecifica, true);
				}
				foreach (var item in Data.Instance.LisMostrarEqui)
				{
					if (item.Nombre == IdentificadorEstampaEspecifica)
					{
						item.Adquirida = "true";
					}
				}
				return RedirectToAction("Estado");
			}
			catch (Exception)
			{
				throw new DriveNotFoundException();
			}
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
