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
    public class AccesoDatos
    {
        private readonly string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaUsuario"].ConnectionString;

        // OBTENER LOS DATOS DE LA BASE DE DATOS
        public DataTable LeerUsuarios()
        {
            DataTable tabla = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion)) 
            {
                try
                {
                    conexion.Open();
                    string consulta = "SELECT * FROM Usuarios"; 
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Esto carga automáticamente TODAS las columnas existentes de forma dinámica
                            tabla.Load(reader);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return tabla;
        }

        // INSERTAR USUARIO EN LA BASE DE DATOS
        public void InsertarUsuario(Usuarios nuevoUsuario)
        {
            
            string consulta = "INSERT INTO Usuarios (DNI, NOMBRE, APELLIDOS) VALUES (@dni, @nombre, @apellidos)";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        
                        cmd.Parameters.AddWithValue("@dni", nuevoUsuario.Dni);
                        cmd.Parameters.AddWithValue("@nombre", nuevoUsuario.Nombre);

                        // Si el apellido es nulo, se inserta como NULL en la base de datos
                        cmd.Parameters.AddWithValue("@apellidos", (object)nuevoUsuario.Apellidos ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex) // excepcion sql 
                {
                    // 2627: Violación de Primary Key / 2601: Índice único duplicado
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        throw new Exception("El usuario con DNI: " + nuevoUsuario.Dni + "  ya existe");
                    }
                    else
                    {
                        throw new Exception("Error de base de datos: " + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error al insertar el usuario: " + ex.Message);
                }
                
            }
        }

        public void AnadirPrograma(Usuarios nuevoPrograma)
        {
        }

    }
}
