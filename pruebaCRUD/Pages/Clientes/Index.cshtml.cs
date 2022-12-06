using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace pruebaCRUD.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public List<ClienteInfo> listaClientes=new List<ClienteInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=JDPC;Initial Catalog=prueba;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM CLIENTES";
                    using(SqlCommand command= new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                ClienteInfo clienteInfo= new ClienteInfo();
                                clienteInfo.id = "" + reader.GetInt32(0);
                                clienteInfo.nombre= reader.GetString(1);
                                clienteInfo.correo= reader.GetString(2);
                                clienteInfo.telefono= reader.GetString(3);
                                clienteInfo.direccion= reader.GetString(4);
                                clienteInfo.creacion= reader.GetDateTime(5).ToString();
                                listaClientes.Add(clienteInfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class ClienteInfo
    {
        public String id;
        public String nombre;
        public String correo;
        public String telefono;
        public String direccion;
        public String creacion;
    }
}
