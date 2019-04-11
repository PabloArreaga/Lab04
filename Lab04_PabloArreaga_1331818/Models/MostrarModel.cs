using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lab04_PabloArreaga_1331818.Models
{
	public class MostrarModel
	{
		[Display(Name = "Clase de la Estampa")]
		public string Clase { get; set; }
		public string Faltan { get; set; }
	}
}