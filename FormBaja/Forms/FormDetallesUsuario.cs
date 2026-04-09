using Clases;
using FormBaja.Datos;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FormBaja.Forms
{
    public partial class FormDetallesUsuario : MaterialForm
    {
        private string _dni;
        private AccesoDatos _accesoDatos = new AccesoDatos();
        private FlowLayoutPanel panelContenedor;
        private List<ControlReferenciaPrograma> controlesProgramas = new List<ControlReferenciaPrograma>();

        private struct ControlReferenciaPrograma
        {
            public string NombrePrograma;
            public ComboBox ComboEstado;
            public DateTimePicker PickerFecha;
        }

        public FormDetallesUsuario(string dni)
        {
            _dni = dni;
            InitializeComponent();
            GestorTema.ConfigurarMaterialSkin(this);
            this.Text = "SEGUIMIENTO - DNI: " + _dni;

            ConfigurarEstructuraBase();
            CargarDatosUsuario();
        }

        private void ConfigurarEstructuraBase()
        {
            // 1. Creamos un panel para el "Footer" (área de botones)
            Panel panelFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.White
            };

            // 2. Botón Guardar (más pequeño y a la derecha)
            MaterialButton btnGuardar = new MaterialButton
            {
                Text = "GUARDAR CAMBIOS",
                Type = MaterialButton.MaterialButtonType.Contained,
                UseAccentColor = true,
                // Lo posicionamos a la derecha
                Location = new Point(this.Width - 180, 10),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Size = new Size(150, 40)
            };
            btnGuardar.Click += (s, e) => GuardarCambios();

            panelFooter.Controls.Add(btnGuardar);

            // 3. Panel con Scroll para el contenido
            panelContenedor = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(20, 80, 20, 20), // 80px arriba para no chocar con el título azul
                BackColor = Color.White
            };

            // IMPORTANTE: Si limpiaste el diseñador, ya no necesitas el Controls.Clear()
            // Añadimos los paneles al formulario
            this.Controls.Add(panelContenedor);
            this.Controls.Add(panelFooter);

            // Esto asegura que el panel de scroll respete el espacio del footer
            panelContenedor.BringToFront();
        }

        private void CargarDatosUsuario()
        {
            panelContenedor.Controls.Clear();
            controlesProgramas.Clear();

            DataTable datos = _accesoDatos.ObtenerDetallesCompletos(_dni);
            if (datos.Rows.Count == 0) return;

            DataRow fila = datos.Rows[0];

            // Encabezado con datos personales
            AñadirEtiquetaInformativa("NOMBRE: " + fila["NOMBRE"].ToString());
            AñadirEtiquetaInformativa("APELLIDOS: " + fila["APELLIDOS"].ToString());
            panelContenedor.Controls.Add(new Label { Text = "________________________________", AutoSize = true, ForeColor = Color.Gray, Margin = new Padding(0, 0, 0, 20) });

            foreach (DataColumn col in datos.Columns)
            {
                string nombreCol = col.ColumnName;

                if (nombreCol == "DNI" || nombreCol == "NOMBRE" || nombreCol == "APELLIDOS" || nombreCol.EndsWith("_Fecha"))
                    continue;

                object valorFecha = datos.Columns.Contains(nombreCol + "_Fecha") ? fila[nombreCol + "_Fecha"] : DBNull.Value;

                CrearFilaPrograma(nombreCol, fila[nombreCol]?.ToString(), valorFecha);
            }

            // ESPACIADOR FINAL: Esto soluciona que el último programa se vea cortado
            panelContenedor.Controls.Add(new Label { Text = "", Height = 50, Width = 10 });
        }

        private void CrearFilaPrograma(string programa, string estadoActual, object fechaActual)
        {
            // Panel de la fila (ajustamos el ancho a 700 para dar aire)
            Panel fila = new Panel { Width = 700, Height = 100, Margin = new Padding(0, 10, 0, 0) };

            Label lblProg = new Label
            {
                Text = programa.ToUpper(),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Top = 5,
                Width = 600
            };

            ComboBox cbEstado = new ComboBox
            {
                Top = 35,
                Width = 150,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbEstado.Items.AddRange(new object[] { "", "ACTIVO", "INACTIVO" });
            cbEstado.Text = estadoActual;

            Label lblFecha = new Label { Text = "Fecha de cambio:", Top = 70, Left = 0, Width = 120 };
            DateTimePicker dtp = new DateTimePicker
            {
                Top = 68,
                Left = 130,
                Width = 200,
                Format = DateTimePickerFormat.Short
            };

            if (fechaActual != DBNull.Value) dtp.Value = Convert.ToDateTime(fechaActual);

            fila.Controls.Add(lblProg);
            fila.Controls.Add(cbEstado);
            fila.Controls.Add(lblFecha);
            fila.Controls.Add(dtp);

            panelContenedor.Controls.Add(fila);

            controlesProgramas.Add(new ControlReferenciaPrograma
            {
                NombrePrograma = programa,
                ComboEstado = cbEstado,
                PickerFecha = dtp
            });
        }

        private void AñadirEtiquetaInformativa(string texto)
        {
            panelContenedor.Controls.Add(new Label
            {
                Text = texto,
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Margin = new Padding(0, 5, 0, 5)
            });
        }

        private void GuardarCambios()
        {
            try
            {
                foreach (var prog in controlesProgramas)
                {
                    _accesoDatos.ActualizarProgramaConFecha(_dni, prog.NombrePrograma, prog.ComboEstado.Text, prog.PickerFecha.Value);
                }
                MessageBox.Show("Datos guardados correctamente.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }
    }
}