using ClosedXML.Excel;
using FormBaja.Entidades;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DateTime = System.DateTime;

namespace FormBaja.Datos
{
    public class AccesoDatos
    {
        //--------------------------------------------------------------
        // CADENA DE CONEXION
        //--------------------------------------------------------------
        private readonly string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaUsuario"].ConnectionString;

        //--------------------------------------------------------------
        // METODOS DE ACCESO Y EDICION
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

        // ACTUALIZAR DNI USUARIO
        public void ActualizarDniUsuario(string dniOriginal, string dniNuevo)
        {
            string consulta = "UPDATE Usuarios SET DNI = @nuevo WHERE DNI = @original";
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@nuevo", dniNuevo);
                    cmd.Parameters.AddWithValue("@original", dniOriginal);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ACTUALIZAR DATOS USUARIOS
        public void ActualizarDatosUsuarios(string dni, string nombre, string apellidos)
        {
            string consulta = "UPDATE Usuarios SET NOMBRE = @nombre, APELLIDOS = @apellidos WHERE DNI = @dni";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellidos", (object)apellidos ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar: " + ex.Message);
                }
            }
        }

        // INSERTAR PROGRAMA EN LA BASE DE DATOS
        public void AñadirColumnaPrograma(string nombrePrograma)
        {
            if (string.IsNullOrEmpty(nombrePrograma)) return;

            // Creamos la columna de estado y la columna de fecha vinculada
            string consulta = $@"ALTER TABLE Usuarios ADD [{nombrePrograma}] NVARCHAR(50) NULL;
                         ALTER TABLE Usuarios ADD [{nombrePrograma}_Fecha] DATETIME NULL;";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Programa y columna de fecha agregados correctamente.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al añadir el programa: " + ex.Message);
                }
            }
        }

        // ACTUALIZAR DATOS PROGRAMA
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

        // BORRAR REGISTRO
        public void BorrarRegistro(string dni)
        {
            string consulta = "DELETE FROM Usuarios WHERE DNI = @dni";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try {

                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Registro borrado correctamente.");

                    
                    }
                
                }



                catch (Exception ex)
                {
                    throw new Exception("Error al borrar el registro: " + ex.Message);
                }


            }


        }

        // METODOS PARA EL FORM DETALLES FECHA

        // Obtiene toda la fila del usuario incluyendo las columnas de fecha
        public DataTable ObtenerDetallesCompletos(string dni)
        {
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM Usuarios WHERE DNI = @dni";
            using (SqlConnection cx = new SqlConnection(cadenaConexion))
            {
                cx.Open();
                using (SqlCommand cmd = new SqlCommand(consulta, cx))
                {
                    cmd.Parameters.AddWithValue("@dni", dni);
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        // Actualiza el estado y la fecha al mismo tiempo
        public void ActualizarProgramaConFecha(string dni, string programa, string estado, DateTime fecha)
        {
            string colFecha = programa + "_Fecha";
            string consulta = $"UPDATE Usuarios SET [{programa}] = @estado, [{colFecha}] = @fecha WHERE DNI = @dni";

            using (SqlConnection cx = new SqlConnection(cadenaConexion))
            {
                cx.Open();
                using (SqlCommand cmd = new SqlCommand(consulta, cx))
                {
                    cmd.Parameters.AddWithValue("@estado", string.IsNullOrEmpty(estado) ? (object)DBNull.Value : estado);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@dni", dni);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        //------------------------------------------------------------------------
        // OTROS METODOS
        //------------------------------------------------------------------------


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
                        
                        
                        cmd.Parameters.AddWithValue("@busqueda",  txtBusqueda + "%");

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
