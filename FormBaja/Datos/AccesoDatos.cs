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
using System.Windows.Forms;
using FormBaja.Forms;

namespace FormBaja.Datos
{
    public class AccesoDatos
    {
        //--------------------------------------------------------------
        // CADENA DE CONEXION
        //--------------------------------------------------------------
        private readonly string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaUsuario"].ConnectionString;

        //--------------------------------------------------------------
        // METODOS
        //--------------------------------------------------------------

        // CARGA DE DATOS
        public void CargarDatos(DataGridView DgvBajas)
        {
            DgvBajas.DataSource = LeerUsuarios();
            
        }

        // LEER USUARIOS
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
            if (string.IsNullOrEmpty(nuevoUsuario.Dni) || string.IsNullOrEmpty(nuevoUsuario.Nombre)) { 
            
               MessageBox.Show("El DNI y el nombre son obligatorios.");
                return;
            
            }
            
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

                        // SI EL APELLIDO ES NULO LO GUARDAMOS COMO NULL
                        cmd.Parameters.AddWithValue("@apellidos", (object)nuevoUsuario.Apellidos ?? DBNull.Value);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Usuario añadido correctamente.");
                        
                    }
                }
                catch (SqlException ex) // EXCEPCION DE SQL
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
        
        // INSERTAR PROGRAMA EN LA BASE DE DATOS
        public void AñadirColumnaPrograma(string nombrePrograma)
        {
            if (string.IsNullOrEmpty(nombrePrograma)) {
            
                MessageBox.Show("El nombre del programa no puede estar vacio.");
                return;
            } 

            string consulta = $"ALTER TABLE Usuarios ADD [{nombrePrograma}] NVARCHAR(50) NULL";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Programa agregado correctamente.");
                    }
                }
                catch (SqlException ex)
                {
                    // Error 1706: La columna ya existe
                    if (ex.Number == 1706 || ex.Message.Contains("already exists"))
                    {
                        throw new Exception("El programa ya existe en la base de datos.");
                    }
                    throw new Exception("Error de SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al añadir el programa: " + ex.Message);
                }
            }
        }

        // BUSCAR USUARIO POR DNI O NOMBRE

        public void BuscarUsuario(DataGridView dgv, string txtBusqueda)
        {
            Console.WriteLine("buscando: " +txtBusqueda);
            DataTable tabla = new DataTable();
            
            string consulta = "SELECT * FROM Usuarios WHERE DNI LIKE @busqueda OR NOMBRE LIKE @busqueda OR APELLIDOS LIKE @busqueda";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        // creamos el parametro de la busqueda y le pasamos el txtBusqueda,
                        // el % es para que busque en cualquier parte de la cadena de texto en lugar de solo al principio
                        // o exactamente lo que hayas escrito.
                        cmd.Parameters.AddWithValue("@busqueda", "%" + txtBusqueda + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            tabla.Load(reader);
                        }
                    }
                    // asignamos la tabla al dataGridView baja
                    dgv.DataSource = tabla;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar el usuario: " + ex.Message);
                }
            }
        }

    }
}
