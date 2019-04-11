using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lab04_PabloArreaga_1331818.Models
{
	public class MostrarEquiModel
	{
		[Display(Name = "Nombre de la Estampa")]
		public string Nombre { get; set; }
		[Display(Name = "Adquirida")]
		public string Adquirida { get; set; }
	}
}