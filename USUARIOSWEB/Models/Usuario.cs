using System;
using System.Collections.Generic;
using X.PagedList;

namespace USUARIOSWEB.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? Sexo { get; set; }

	public class UsuarioViewModel
	{
		public IPagedList<Usuario> Usuarios { get; set; }
	}
}
