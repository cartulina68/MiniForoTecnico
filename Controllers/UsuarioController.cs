using Microsoft.AspNetCore.Mvc;
using MiniForoTecnico.Data;
using MiniForoTecnico.Models;
using Microsoft.Data.SqlClient;

namespace MiniForoTecnico.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Conexion _conexion;

        public UsuarioController(Conexion conexion)
        {
            _conexion = conexion;
        }

        public IActionResult Index()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            using (SqlConnection conn = _conexion.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarios", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaUsuarios.Add(new Usuario
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"]?.ToString() ?? "",
                                Correo = reader["Correo"]?.ToString() ?? "",
                                ClaveHash = reader["ClaveHash"]?.ToString() ?? "",
                                Rol = reader["Rol"]?.ToString() ?? ""
                            });
                        }
                    }
                }
            }

            return View(listaUsuarios);
        }
    }
}
