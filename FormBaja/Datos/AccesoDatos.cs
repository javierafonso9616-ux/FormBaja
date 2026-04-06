using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FormBaja.Entidades;

namespace FormBaja.Datos
{
    internal class AccesoDatos
    {
        private readonly string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaUsuario"].ConnectionString;


        public List<Usuarios> LeerUsuarios()
        {
            List<Usuarios> lista = new List<Usuarios>();


            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {

                try
                {
                    conexion.Open();


                    string consulta = "SELECT * FROM Usuarios";


                    SqlCommand cmd = new SqlCommand(consulta, conexion);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuarios usuario = new Usuarios {
                            
                                // USUARIOS
                                Dni = reader["DNI"].ToString(),
                                Nombre = reader["NOMBRE"].ToString(),
                                Apellidos = reader["APELLIDOS"].ToString(),

                                // PROGRAMAS
                                Milena = reader["MILENA"].ToString(),

                                 
                            };



                        }

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }



            return lista;
        }
    }
}
