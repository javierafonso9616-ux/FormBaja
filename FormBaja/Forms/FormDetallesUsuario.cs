using FormBaja.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FormBaja.Forms
{
    public partial class FormDetallesUsuario : Form
    {
        private string _dni;
        private AccesoDatos _accesoDatos = new AccesoDatos();
        private FlowLayoutPanel panelContenedor;
        private DateTimePicker dtpGlobal;
        private List<ControlReferenciaPrograma> controlesProgramas = new List<ControlReferenciaPrograma>();

        private struct ControlReferenciaPrograma
        {
            public string NombrePrograma;
            public ComboBox ComboEstado;
        }

        public FormDetallesUsuario(string dni, string nombre, string apellidos)
        {
            _dni = dni;
            InitializeComponent();
            this.Text = "SEGUIMIENTO - DNI: " + _dni + " Nombre: " + nombre + " " + apellidos;
            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterParent;

            ConfigurarInterfaz();
            CargarDatos();
        }

        private void ConfigurarInterfaz()
        {
            // --- LIMPIEZA TOTAL: Borra cualquier control que venga del Diseñador ---
            this.Controls.Clear();

            // 1. Panel Inferior (Footer) para el Botón
            Panel panelFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(230, 230, 230),
                Padding = new Padding(0, 0, 20, 0)
            };

            Button btnGuardar = new Button
            {
                Text = "GUARDAR CAMBIOS",
                Size = new Size(160, 45),
                Dock = DockStyle.Right,
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Margin = new Padding(0, 15, 0, 15)
            };
            btnGuardar.Click += (s, e) => GuardarTodo();
            panelFooter.Controls.Add(btnGuardar);

            // 2. Panel Superior (Header) para la Fecha
            Panel panelFecha = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            Label lbl = new Label { Text = "FECHA DE CAMBIO:", Top = 30, Left = 20, AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            dtpGlobal = new DateTimePicker { Top = 27, Left = 180, Width = 150, Format = DateTimePickerFormat.Short };
            panelFecha.Controls.Add(lbl); panelFecha.Controls.Add(dtpGlobal);

            // 3. Panel Central con Scroll (Contenedor)
            panelContenedor = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(20),
                BackColor = Color.White
            };

            // --- ORDEN DE ADICIÓN CRÍTICO PARA EL DOCKING ---
            // En WinForms, el control con Dock.Fill se debe añadir AL FINAL para que respete a los demás
            this.Controls.Add(panelContenedor);
            this.Controls.Add(panelFecha);
            this.Controls.Add(panelFooter);

            // Forzamos visibilidad
            panelFooter.BringToFront();
            panelFecha.BringToFront();
        }

        private void CargarDatos()
        {
            panelContenedor.Controls.Clear();
            controlesProgramas.Clear();

            DataTable dt = _accesoDatos.ObtenerDetallesCompletos(_dni);
            if (dt.Rows.Count == 0) return;
            DataRow fila = dt.Rows[0];

            if (dt.Columns.Contains("Fecha") && fila["Fecha"] != DBNull.Value)
                dtpGlobal.Value = Convert.ToDateTime(fila["Fecha"]);

            AñadirEtiqueta("USUARIO: " + fila["NOMBRE"] + " " + fila["APELLIDOS"]);
            panelContenedor.Controls.Add(new Label { Text = "____________________________________", AutoSize = true, ForeColor = Color.Gray, Margin = new Padding(0, 0, 0, 20) });

            foreach (DataColumn col in dt.Columns)
            {
                string nom = col.ColumnName;
                if (nom == "DNI" || nom == "NOMBRE" || nom == "APELLIDOS" || nom == "Fecha" || nom.EndsWith("_Fecha")) continue;
                CrearFila(nom, fila[nom]?.ToString());
            }

            // Espaciador final para que el scroll suba el último elemento por encima del footer
            panelContenedor.Controls.Add(new Label { Height = 100, Width = 10 });
        }

        private void CrearFila(string prog, string estado)
        {
            Panel p = new Panel { Width = 520, Height = 60, Margin = new Padding(0, 0, 0, 10) };
            Label l = new Label { Text = prog.ToUpper(), Top = 15, Width = 280, Font = new Font("Segoe UI", 10) };
            ComboBox c = new ComboBox { Top = 12, Left = 300, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Standard };
            c.Items.AddRange(new object[] { "", "ACTIVO", "INACTIVO" });
            c.Text = estado;

            /*
            c.SelectedIndexChanged += (s, e) => PintarCombo((ComboBox)s);
            PintarCombo(c);
            */
            p.Controls.Add(l); p.Controls.Add(c);
            panelContenedor.Controls.Add(p);
            controlesProgramas.Add(new ControlReferenciaPrograma { NombrePrograma = prog, ComboEstado = c });
        }

        /*private void PintarCombo(ComboBox cb)
        {
            if (cb.Text == "ACTIVO") cb.ForeColor = Color.Green;
            else if (cb.Text == "INACTIVO") cb.ForeColor = Color.Red;
            else cb.ForeColor = Color.Black;
        }
        */
        private void AñadirEtiqueta(string t) => panelContenedor.Controls.Add(new Label { Text = t, AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold) });

        private void GuardarTodo()
        {
            try
            {
                foreach (var cp in controlesProgramas)
                    _accesoDatos.ActualizarEstadoYFechaGlobal(_dni, cp.NombrePrograma, cp.ComboEstado.Text, dtpGlobal.Value);
                MessageBox.Show("Guardado con éxito.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}