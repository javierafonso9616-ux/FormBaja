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

        // Estructura para guardar las referencias a los controles creados
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
            this.Text = "Detalles y Seguimiento - DNI: " + _dni;

            ConfigurarEstructuraBase();
            CargarDatosUsuario();
        }

        private void ConfigurarEstructuraBase()
        {
            // Panel con Scroll
            panelContenedor = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(20, 80, 20, 20) // Espacio para el header de MaterialSkin
            };

            // Botón Guardar
            MaterialButton btnGuardar = new MaterialButton
            {
                Text = "GUARDAR CAMBIOS",
                Dock = DockStyle.Bottom,
                Height = 50
            };
            btnGuardar.Click += (s, e) => GuardarCambios();

            this.Controls.Add(panelContenedor);
            this.Controls.Add(btnGuardar);
        }

        private void CargarDatosUsuario()
        {
            DataTable datos = _accesoDatos.ObtenerDetallesCompletos(_dni);
            if (datos.Rows.Count == 0) return;

            DataRow fila = datos.Rows[0];

            // 1. Mostrar Datos Básicos
            AñadirEtiquetaInformativa("NOMBRE: " + fila["NOMBRE"].ToString());
            AñadirEtiquetaInformativa("APELLIDOS: " + fila["APELLIDOS"].ToString());
            panelContenedor.Controls.Add(new Label { Text = "________________________________", AutoSize = true, ForeColor = Color.Gray });

            // 2. Generar controles para cada programa de forma dinámica
            foreach (DataColumn col in datos.Columns)
            {
                string nombreCol = col.ColumnName;

                // Saltamos las columnas fijas y las de fecha
                if (nombreCol == "DNI" || nombreCol == "NOMBRE" || nombreCol == "APELLIDOS" || nombreCol.EndsWith("_Fecha"))
                    continue;

                CrearFilaPrograma(nombreCol, fila[nombreCol]?.ToString(), fila[nombreCol + "_Fecha"]);
            }
        }

        private void CrearFilaPrograma(string programa, string estadoActual, object fechaActual)
        {
            Panel fila = new Panel { Width = 500, Height = 100, Margin = new Padding(0, 10, 0, 0) };

            Label lblProg = new Label { Text = programa, Font = new Font("Segoe UI", 10, FontStyle.Bold), Top = 5, Width = 400 };

            ComboBox cbEstado = new ComboBox { Top = 30, Width = 150, FlatStyle = FlatStyle.Flat };
            cbEstado.Items.AddRange(new object[] { "", "ACTIVO", "INACTIVO" });
            cbEstado.Text = estadoActual;

            Label lblFecha = new Label { Text = "Fecha de cambio:", Top = 60, Left = 0, Width = 120 };
            DateTimePicker dtp = new DateTimePicker { Top = 58, Left = 130, Width = 200, Format = DateTimePickerFormat.Short };

            if (fechaActual != DBNull.Value) dtp.Value = Convert.ToDateTime(fechaActual);

            fila.Controls.Add(lblProg);
            fila.Controls.Add(cbEstado);
            fila.Controls.Add(lblFecha);
            fila.Controls.Add(dtp);

            panelContenedor.Controls.Add(fila);

            // Guardamos la referencia para el botón guardar
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
                MessageBox.Show("Cambios guardados correctamente.");
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