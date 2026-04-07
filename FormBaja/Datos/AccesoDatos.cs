using ClosedXML.Excel;
using FormBaja.Entidades;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
                    string consulta = "SELECT * FROM Usuarios ORDER BY NOMBRE ASC";

                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // CARGAMOS LOS DATOS EN LA TABLA

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

        // INSERTAR DATOS
        public void ActualizarDatosProgramas(string dni, string nombreColumna, string valor)
        {
            // USAMOS [] PARA QUE EL NOMBRE DE LA COLUMNA SEA DINÁMICO Y
            // ACEPTE CUALQUIER NOMBRE CON ESPACIOS O CARÁCTERES ESPECIALES
            string consulta = $"UPDATE Usuarios SET [{nombreColumna}] = @valor WHERE DNI = @dni";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        // SI ES VACIO LO GUARDAMOS COMO NULL
                        cmd.Parameters.AddWithValue("@valor", string.IsNullOrEmpty(valor) ? (object)DBNull.Value : valor);
                        cmd.Parameters.AddWithValue("@dni", dni);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar: " + ex.Message);
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
                        // CONCATENAMOS CON % PARA QUE BUSQUE EN CUALQUIER PARTE
                        
                        cmd.Parameters.AddWithValue("@busqueda", "%" + txtBusqueda + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            tabla.Load(reader);
                        }
                    }
                    // ASIGNAMOS LA TABLA AL GRID
                    dgv.DataSource = tabla;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar el usuario: " + ex.Message);
                }
            }
        }

        // EXPORTAR A EXCEL
        public void ExportarExcel(DataGridView dgv, string txtBusqueda)
        {
            // VERIFICA QUE HAY DATOS EN EL DATAGRIDVIEW
            if (dgv.DataSource == null || !(dgv.DataSource is DataTable dt))
            {
                MessageBox.Show("No hay datos disponibles para exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // CUADRO DE WINDOW PARA GUARDAR EL ARCHIVO
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Exportar datos a Excel",
                FileName = "Listado_Bajas_" + txtBusqueda + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        // AÑADE UNA HOJA CON LOS DATOS DEL DATAGRIDVIEW
                        
                        var ws = wb.Worksheets.Add(dt, "Usuarios");

                        // AJUSTAMOS EL ANCHO DE LAS COLUMNAS
                        ws.Columns().AdjustToContents();

                        // GUARDADON DEL ARCHIVO
                        wb.SaveAs(sfd.FileName);
                        MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al generar el Excel: " + ex.Message);
                }
            }
        }

    }
}
