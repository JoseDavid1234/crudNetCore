using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace pruebaCRUD.Pages.Clientes
{
    public class EditarModel : PageModel
    {
        public ClienteInfo clienteInfo = new ClienteInfo();
        public String mensajeError = "";
        public String mensajeExito = "";

        public void OnGet()
        {
            String id = Request.Query["ID"];

            try
            {
				String connectionString = "Data Source=JDPC;Initial Catalog=prueba;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM CLIENTES WHERE ID = @ID";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@ID", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								clienteInfo.id = "" + reader.GetInt32(0);
								clienteInfo.nombre = reader.GetString(1);
								clienteInfo.correo = reader.GetString(2);
								clienteInfo.telefono = reader.GetString(3);
								clienteInfo.direccion = reader.GetString(4);
							}
						}
					}
				}


			}
			catch(Exception ex)
            {
                mensajeError= ex.Message;
            }
        }

        public void OnPost() {
			clienteInfo.id = Request.Form["id"];
			clienteInfo.nombre = Request.Form["nombre"];
			clienteInfo.correo = Request.Form["correo"];
			clienteInfo.telefono = Request.Form["telefono"];
			clienteInfo.direccion = Request.Form["direccion"];

			if (clienteInfo.nombre.Length == 0 || clienteInfo.correo.Length == 0 || clienteInfo.telefono.Length == 0 || clienteInfo.direccion.Length == 0)
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
					String sql = "UPDATE CLIENTES " +
						"SET NOMBRE=@NOMBRE, CORREO=@CORREO, TELEFONO=@TELEFONO, DIRECCION=@DIRECCION " +
						"WHERE ID=@ID;";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@NOMBRE", clienteInfo.nombre);
						command.Parameters.AddWithValue("@CORREO", clienteInfo.correo);
						command.Parameters.AddWithValue("@TELEFONO", clienteInfo.telefono);
						command.Parameters.AddWithValue("@DIRECCION", clienteInfo.direccion);
						command.Parameters.AddWithValue("@ID", clienteInfo.id);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				mensajeError = ex.Message;
				return;
			}

			clienteInfo.nombre = ""; clienteInfo.telefono = ""; clienteInfo.correo = ""; clienteInfo.direccion = "";
			mensajeExito = "Cliente actualizado correctamente";

			Response.Redirect("/Clientes/Index");

		}
    }
}
