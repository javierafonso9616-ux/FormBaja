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
        private readonly string cadenaConexion = ConfigurationManager.ConnectionStrings["CadenaUsuario"].ConnectionString;

        public void CargarDatos(DataGridView DgvBajas)
        {
            DgvBajas.DataSource = LeerUsuarios();
        }

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
                        using (SqlDataReader reader = cmd.ExecuteReader()) { tabla.Load(reader); }
                    }
                }
                catch (Exception) { throw; }
            }
            return tabla;
        }

        public void InsertarUsuario(Usuarios nuevoUsuario)
        {
            if (string.IsNullOrEmpty(nuevoUsuario.Dni) || string.IsNullOrEmpty(nuevoUsuario.Nombre))
            {
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
                        cmd.Parameters.AddWithValue("@apellidos", (object)nuevoUsuario.Apellidos ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Usuario añadido correctamente.");
                    }
                }
                catch (Exception ex) { throw new Exception("Error al insertar: " + ex.Message); }
            }
        }

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
                catch (Exception ex) { throw new Exception("Error al actualizar: " + ex.Message); }
            }
        }

        public void AñadirColumnaPrograma(string nombrePrograma)
        {
            if (string.IsNullOrEmpty(nombrePrograma)) return;
            // Solo creamos la columna del programa, sin columna de fecha individual
            string consulta = $"ALTER TABLE Usuarios ADD [{nombrePrograma}] NVARCHAR(50) NULL;";
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
                catch (Exception ex) { throw new Exception("Error: " + ex.Message); }
            }
        }

        public void ActualizarDatosProgramas(string dni, string nombreColumna, string valor)
        {
            // Al actualizar desde el Grid, también actualizamos la fecha global a 'Hoy'
            string consulta = $"UPDATE Usuarios SET [{nombreColumna}] = @valor, [Fecha] = @fecha WHERE DNI = @dni";
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@valor", string.IsNullOrEmpty(valor) ? (object)DBNull.Value : valor);
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex) { throw new Exception("Error: " + ex.Message); }
            }
        }

        public void BorrarRegistro(string dni)
        {
            string consulta = "DELETE FROM Usuarios WHERE DNI = @dni";
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro borrado correctamente.");
                    }
                }
                catch (Exception ex) { throw new Exception("Error al borrar: " + ex.Message); }
            }
        }

        public DataTable ObtenerDetallesCompletos(string dni)
        {
            using (SqlConnection cx = new SqlConnection(cadenaConexion))
            {
                cx.Open();
                // Verificamos si existe la columna 'Fecha' global, si no, la creamos
                string sqlCheck = "IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = 'Fecha' AND Object_ID = Object_ID('Usuarios')) " +
                                  "ALTER TABLE Usuarios ADD [Fecha] DATETIME NULL";
                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, cx)) { cmdCheck.ExecuteNonQuery(); }

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

        public void ActualizarEstadoYFechaGlobal(string dni, string programa, string estado, DateTime fecha)
        {
            string consulta = $"UPDATE Usuarios SET [{programa}] = @estado, [Fecha] = @fecha WHERE DNI = @dni";
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

        public void BuscarUsuario(DataGridView dgv, string txtBusqueda)
        {
            DataTable tabla = new DataTable();
            string consulta = "SELECT * FROM Usuarios WHERE DNI LIKE @busqueda OR NOMBRE LIKE @busqueda OR APELLIDOS LIKE @busqueda";
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@busqueda", txtBusqueda + "%");
                        using (SqlDataReader reader = cmd.ExecuteReader()) { tabla.Load(reader); }
                    }
                    dgv.DataSource = tabla;
                }
                catch (Exception ex) { throw new Exception("Error: " + ex.Message); }
            }
        }

        public void ExportarExcel(DataTable dt, string nombreArchivo)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = nombreArchivo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(dt, "Usuarios");

                        // --- MEJORA PARA LAS FECHAS ---
                        // Buscamos todas las celdas que contienen fechas para darles formato y asegurar el ancho
                        foreach (var column in ws.ColumnsUsed())
                        {
                            // Si el encabezado o los datos parecen ser fechas
                            if (column.FirstCell().Value.ToString().Contains("Fecha") ||
                                column.FirstCell().Value.ToString().Contains("FECHA"))
                            {
                                // Forzamos un formato de fecha corto para que ocupe menos espacio
                                column.Style.DateFormat.Format = "dd/mm/yyyy HH:mm:ss";
                            }
                        }

                        // Ajustamos el ancho de todas las columnas al contenido final
                        ws.Columns().AdjustToContents();

                        // Opcional: Darle un poco de "aire" extra a las columnas (un 10% más)
                        foreach (var col in ws.ColumnsUsed()) { col.Width += 2; }

                        wb.SaveAs(sfd.FileName);
                        MessageBox.Show("Exportado con éxito.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar: " + ex.Message);
                }
            }
        }
    }
}   