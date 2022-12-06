using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace pruebaCRUD.Pages.Clientes
{
    public class CrearModel : PageModel
    {
        public ClienteInfo clienteInfo = new ClienteInfo();
        public String mensajeError = "";
        public String mensajeExito = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clienteInfo.nombre = Request.Form["nombre"];
			clienteInfo.correo = Request.Form["correo"];
			clienteInfo.telefono = Request.Form["telefono"];
			clienteInfo.direccion = Request.Form["direccion"];

            if(clienteInfo.nombre.Length==0 || clienteInfo.correo.Length==0 || clienteInfo.telefono.Length==0 || clienteInfo.direccion.Length == 0)
            {
                mensajeError = "Todos los campos son requeridos";
                return;
            }

            try
            {
                String cadenaConexion = "Data Source=JDPC;Initial Catalog=prueba;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    connection.Open();
                    String sql = "INSERT INTO CLIENTES " +
                        "(NOMBRE,CORREO,TELEFONO,DIRECCION) VALUES " +
                        "(@NOMBRE,@CORREO,@TELEFONO,@DIRECCION);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@NOMBRE", clienteInfo.nombre);
                        command.Parameters.AddWithValue("@CORREO", clienteInfo.correo);
						command.Parameters.AddWithValue("@TELEFONO", clienteInfo.telefono);
						command.Parameters.AddWithValue("@DIRECCION", clienteInfo.direccion);

                        command.ExecuteNonQuery();
					}
				}
            }
            catch(Exception ex)
            {
                mensajeError= ex.Message;
                return;
            }

            clienteInfo.nombre = "";clienteInfo.telefono = "";clienteInfo.correo = "";clienteInfo.direccion = "";
            mensajeExito = "Nuevo Cliente Agregado Correctamente";

            Response.Redirect("/Clientes/Index");
		}
    }
}
