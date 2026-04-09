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

        // METODOS PARA EL FORM DETALLES FECHA---------------------------

        // MÉTODO PARA REPARAR LA ESTRUCTURA DE LA TABLA AUTOMÁTICAMENTE
        private void SincronizarColumnasDeFecha(SqlConnection conexion)
        {
            // 1. Obtenemos todas las columnas de la tabla Usuarios
            DataTable esquema = conexion.GetSchema("Columns", new[] { null, null, "Usuarios" });

            // 2. Metemos los nombres en una lista para buscar más rápido
            var nombresColumnas = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (DataRow fila in esquema.Rows)
            {
                nombresColumnas.Add(fila["COLUMN_NAME"].ToString());
            }

            // 3. Revisamos cada columna para ver si es un programa que necesita su _Fecha
            foreach (string col in nombresColumnas)
            {
                // Si no es una columna fija y no es ya una columna de fecha
                if (col != "DNI" && col != "NOMBRE" && col != "APELLIDOS" && !col.EndsWith("_Fecha"))
                {
                    string colFecha = col + "_Fecha";

                    // Si el programa existe pero su columna de fecha no, la creamos
                    if (!nombresColumnas.Contains(colFecha))
                    {
                        string sqlAlter = $"ALTER TABLE Usuarios ADD [{colFecha}] DATETIME NULL";
                        using (SqlCommand cmd = new SqlCommand(sqlAlter, conexion))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        // ACTUALIZACIÓN DEL MÉTODO DE LECTURA
        // MÉTODO PARA OBTENER DETALLES Y REPARAR COLUMNAS FALTANTES
        // MÉTODO ACTUALIZADO: REPARA LA TABLA Y OBTIENE LOS DATOS
        public DataTable ObtenerDetallesCompletos(string dni)
        {
            using (SqlConnection cx = new SqlConnection(cadenaConexion))
            {
                cx.Open();

                // 1. REPARACIÓN AUTOMÁTICA (Sincroniza columnas faltantes)
                DataTable esquema = cx.GetSchema("Columns", new[] { null, null, "Usuarios" });
                var columnasExistentes = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow col in esquema.Rows) columnasExistentes.Add(col["COLUMN_NAME"].ToString());

                foreach (string colName in columnasExistentes)
                {
                    if (colName != "DNI" && colName != "NOMBRE" && colName != "APELLIDOS" && !colName.EndsWith("_Fecha"))
                    {
                        string colFecha = colName + "_Fecha";
                        if (!columnasExistentes.Contains(colFecha))
                        {
                            string sqlAlter = $"ALTER TABLE Usuarios ADD [{colFecha}] DATETIME NULL";
                            using (SqlCommand cmdAlter = new SqlCommand(sqlAlter, cx)) { cmdAlter.ExecuteNonQuery(); }
                        }
                    }
                }

                // 2. CARGA DE DATOS (Usamos Trim para evitar fallos de búsqueda)
                DataTable dt = new DataTable();
                string consulta = "SELECT * FROM Usuarios WHERE DNI = @dni";
                using (SqlCommand cmd = new SqlCommand(consulta, cx))
                {
                    cmd.Parameters.AddWithValue("@dni", dni.Trim());
                    dt.Load(cmd.ExecuteReader());
                }
                return dt;
            }
        }

        // Actualiza el estado y la fecha al mismo tiempo
        public void ActualizarProgramaConFecha(string dni, string programa, string estado, DateTime fecha)
        {
            using (SqlConnection cx = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cx.Open();

                    // 1. COMPROBACIÓN DE SEGURIDAD: ¿Existe la columna de fecha en SQL Server?
                    string colFecha = programa + "_Fecha";
                    string checkSql = $@"IF EXISTS (SELECT * FROM sys.columns 
                                         WHERE Name = '{colFecha}' AND Object_ID = Object_ID('Usuarios'))
                                         SELECT 1 ELSE SELECT 0";

                    bool tieneColumnaFecha;
                    using (SqlCommand cmdCheck = new SqlCommand(checkSql, cx))
                    {
                        tieneColumnaFecha = (int)cmdCheck.ExecuteScalar() == 1;
                    }

                    // 2. CONSTRUIMOS LA CONSULTA DEPENDIENDO DE SI EXISTE LA COLUMNA
                    string consulta;
                    if (tieneColumnaFecha)
                    {
                        consulta = $"UPDATE Usuarios SET [{programa}] = @estado, [{colFecha}] = @fecha WHERE DNI = @dni";
                    }
                    else
                    {
                        // Si el programa es antiguo y no tiene columna fecha, solo actualizamos el estado
                        consulta = $"UPDATE Usuarios SET [{programa}] = @estado WHERE DNI = @dni";
                    }

                    using (SqlCommand cmd = new SqlCommand(consulta, cx))
                    {
                        cmd.Parameters.AddWithValue("@estado", string.IsNullOrEmpty(estado) ? (object)DBNull.Value : estado);
                        cmd.Parameters.AddWithValue("@dni", dni);

                        // Solo añadimos el parámetro de fecha si la columna existe
                        if (tieneColumnaFecha)
                        {
                            cmd.Parameters.AddWithValue("@fecha", fecha);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar programa y fecha: " + ex.Message);
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
        // EXPORTAR A EXCEL (ACTUALIZADO PARA RECIBIR DATATABLE)
        public void ExportarExcel(DataTable dt, string nombreArchivo)
        {
            // VERIFICA QUE HAYA DATOS
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos disponibles para exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // CUADRO DE DIALOGO PARA GUARDAR EL ARCHIVO
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Exportar datos a Excel",
                FileName = nombreArchivo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        // AÑADIMOS LA HOJA CON LOS DATOS COMPLETOS
                        var ws = wb.Worksheets.Add(dt, "Usuarios");

                        // AJUSTAMOS EL ANCHO DE LAS COLUMNAS
                        ws.Columns().AdjustToContents();

                        // GUARDAMOS EL ARCHIVO
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
