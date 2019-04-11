using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab04_PabloArreaga_1331818.Models;

namespace Lab04_PabloArreaga_1331818.Helpers
{
	public class Data
	{
		private static Data _instance = null;
		public static Data Instance
		{
			get
			{
				if (_instance == null) _instance = new Data();
				{
					return _instance;
				}
			}
		}
		public Dictionary<string, AlbumModel> DicAlbum = new Dictionary<string, AlbumModel>();
		public Dictionary<string, bool> DicColec = new Dictionary<string, bool>();
		public List<MostrarModel> ListMostrar = new List<MostrarModel>();
		public List<MostrarEquiModel> LisMostrarEqui = new List<MostrarEquiModel>();

	}
}