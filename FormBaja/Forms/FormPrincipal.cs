using Clases;
using FormBaja.Datos;
using FormBaja.Forms;
using MaterialSkin.Controls;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FormBaja
{
    public partial class FormPrincipal : MaterialForm
    {
        public readonly AccesoDatos accesoDatos = new AccesoDatos();
        private Timer timerBusqueda;
        private string dniOriginal;
        private ContextMenuStrip menuContextual;
        private bool esCargaInterna = false;

        public FormPrincipal()
        {
            InitializeComponent();
            this.Opacity = 0;
            GestorTema.ConfigurarMaterialSkin(this);

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                    null, DgvBajas, new object[] { true });

            ConfigurarTimerBusqueda();
            ConfigurarMenuContextual();

            this.StartPosition = FormStartPosition.Manual;
            this.Bounds = Screen.PrimaryScreen.WorkingArea;

            DgvBajas.CellDoubleClick += (s, e) => {
                if (e.RowIndex >= 0) AbrirDetallesUsuario();
            };
        }

        private void FormPrincipal_Shown(object sender, EventArgs e)
        {
            accesoDatos.CargarDatos(DgvBajas);
            ConfigurarGrid();
            this.Opacity = 1;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void ConfigurarTimerBusqueda()
        {
            timerBusqueda = new Timer { Interval = 500 };
            timerBusqueda.Tick += BusquedaDelay;
        }

        private void BusquedaDelay(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            accesoDatos.BuscarUsuario(DgvBajas, TxtBuscarDNIoNombre.Text.ToUpper().Trim());
        }

        private void ConvertirCeldasEnDesplegables()
        {
            for (int i = 3; i < DgvBajas.Columns.Count; i++)
            {
                string nom = DgvBajas.Columns[i].Name;
                // OCULTAMOS LA COLUMNA DE FECHA GLOBAL Y LAS ANTIGUAS
                if (nom == "Fecha" || nom.EndsWith("_Fecha"))
                {
                    DgvBajas.Columns[i].Visible = false;
                    continue;
                }
                DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn
                {
                    Name = nom,
                    HeaderText = nom,
                    DataPropertyName = nom,
                    FlatStyle = FlatStyle.Flat,
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                };
                comboCol.DefaultCellStyle.BackColor = Color.White;
                comboCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 222, 251);
                comboCol.Items.AddRange("", "ACTIVO", "INACTIVO");

                DgvBajas.Columns.RemoveAt(i);
                DgvBajas.Columns.Insert(i, comboCol);
            }
        }

        private void BorrarFilaSeleccionada()
        {
            if (DgvBajas.SelectedRows.Count > 0)
            {
                string dni = DgvBajas.SelectedRows[0].Cells[0].Value.ToString();
                if (MessageBox.Show("¿Borrar registro?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    accesoDatos.BorrarRegistro(dni);
                    accesoDatos.CargarDatos(DgvBajas);
                    ConfigurarGrid();
                }
            }
        }

        private void ConfigurarMenuContextual()
        {
            menuContextual = new ContextMenuStrip();
            ToolStripMenuItem itemDetalles = new ToolStripMenuItem("Ver Detalles");
            itemDetalles.Click += (s, e) => AbrirDetallesUsuario();
            ToolStripMenuItem itemBorrar = new ToolStripMenuItem("Borrar");
            itemBorrar.Click += (s, e) => BorrarFilaSeleccionada();
            menuContextual.Items.Add(itemDetalles);
            menuContextual.Items.Add(itemBorrar);
        }

        private void AbrirDetallesUsuario()
        {
            if (DgvBajas.SelectedRows.Count > 0)
            {
                string dni = DgvBajas.SelectedRows[0].Cells[0].Value.ToString().Trim();
                string nombre = DgvBajas.SelectedRows[0].Cells[1].Value.ToString().Trim();
                string apellido = DgvBajas.SelectedRows[0].Cells[2].Value.ToString().Trim();

                if (new FormDetallesUsuario(dni, nombre,apellido ).ShowDialog() == DialogResult.OK)
                {
                    accesoDatos.CargarDatos(DgvBajas);
                    ConfigurarGrid();
                }
            }
        }

        private void ConfigurarGrid()
        {
            DgvBajas.SuspendLayout();// PARTE DEL AGILIZAMIENTO DEL PROGRAMA

            // CONVERTISMOS LAS CELDAS EN DESPLEGABLES
            ConvertirCeldasEnDesplegables();

            // ACTIVAMOS EL MODO AUTOMÁTICO TEMPORALMENTE PARA QUE CÁLCULE EL TAMAÑO IDEAL
            DgvBajas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // *ANTES TENIA ALLCELLS*

            // BLOQUEO DE COLUMNAS: RECORREMOS LAS COLUMNAS PARA FIJAR SU TAMAÑO ACTUAL
            // ESTO EVITA QUE AL ORDENAR O RECARGAR SE VUELVA A MOVER

            for (int i = 0; i < DgvBajas.Columns.Count; i++)
            {
                int anchoCalculado = DgvBajas.Columns[i].Width;
                DgvBajas.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // BLOQUEO DE MODO AUTO
                DgvBajas.Columns[i].Width = anchoCalculado;
            }

            // CONFIGURACIÓN ESPECIAL PARA NOMBRE Y APELLIDOS (SALTOS DE LÍNEA)
            if (DgvBajas.Columns.Count > 2)
            {
                // NOMBRE: FIJAMOS UN ANCHO PARA EL WRAP
                DgvBajas.Columns[1].Width = 100;
                DgvBajas.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // APELLIDOS: ESTE ES EL QUE RELLENA EL HUECO (MODO FILL)

                DgvBajas.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DgvBajas.Columns[2].Width = 100;
                DgvBajas.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                
            }

            // APAGAMOS EL MODO AUTOMÁTICO GLOBAL (PARA QUE NO CAMBIE AL ORDENAR)
            DgvBajas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // PERMITIR QUE LAS FILAS CREZCAN PARA EL SALTO DE LINEA
            DgvBajas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // BLOQUEO DE REDIMENSIONAR FILAS
            DgvBajas.AllowUserToResizeRows = false;
            DgvBajas.RowHeadersVisible = false;
            DgvBajas.ReadOnly = true;
            // ESTILOS VISUALES Y COLORES
            DgvBajas.BackgroundColor = Color.White;
            DgvBajas.BorderStyle = BorderStyle.None;
            DgvBajas.GridColor = Color.FromArgb(230, 230, 230);
            DgvBajas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvBajas.MultiSelect = false;

            // CONFIGURACION DE ESTILOS DE CABECERA
            DgvBajas.EnableHeadersVisualStyles = false;
            DgvBajas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DgvBajas.ColumnHeadersHeight = 60;
            DgvBajas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            DgvBajas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DgvBajas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            DgvBajas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;




            // ALTERNADO DE COLORES DE FILAS
            DgvBajas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            DgvBajas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 222, 251);
            DgvBajas.DefaultCellStyle.SelectionForeColor = Color.Black;

            // ALINEAMIENTO DE TEXTO DE LAS CELDAS
            DgvBajas.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DgvBajas.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            // CONFIGURACIÓN ESPECIAL PARA EL DNI
            if (DgvBajas.Columns.Count > 0)
            {
                DgvBajas.Columns[0].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                DgvBajas.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }




            DgvBajas.ResumeLayout(); // PARTE DEL AGILIZAMIENTO DEL PROGRAMA
        }


        private void DgvBajas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 3) return;
            if (e.Value != null)
            {
                string valor = e.Value.ToString();
                if (valor == "ACTIVO") e.CellStyle.ForeColor = Color.Green;
                else if (valor == "INACTIVO") e.CellStyle.ForeColor = Color.Red;
                else if (valor == "") e.CellStyle.BackColor = Color.LightGray;
            }
        }

        private void TxtBuscarDNIoNombre_TextChanged(object sender, EventArgs e) { timerBusqueda.Stop(); timerBusqueda.Start(); }
        private void BtnNuevoUsuario_Click(object sender, EventArgs e) { new FormNuevoUsuario().ShowDialog(); accesoDatos.CargarDatos(DgvBajas); ConfigurarGrid(); }
        private void BtnNuevoPrograma_Click(object sender, EventArgs e) { new FormNuevoPrograma().ShowDialog(); accesoDatos.CargarDatos(DgvBajas); ConfigurarGrid(); }
        private void BtnExportar_Click(object sender, EventArgs e) => accesoDatos.ExportarExcel(accesoDatos.LeerUsuarios(), "Baja_Servicios_Usuarios");
        private void PictureBox1_Click(object sender, EventArgs e) => Process.Start("https://hospitalcrgijon.com/");

        private void DgvBajas_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                DgvBajas.ClearSelection(); DgvBajas.Rows[e.RowIndex].Selected = true;
                menuContextual.Show(Cursor.Position);
            }
        }

        private void DgvBajas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || esCargaInterna) return;
            try
            {
                esCargaInterna = true;
                string dni = DgvBajas.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (e.ColumnIndex == 0) accesoDatos.ActualizarDniUsuario(dniOriginal, dni);
                else if (e.ColumnIndex == 1 || e.ColumnIndex == 2) accesoDatos.ActualizarDatosUsuarios(dni, DgvBajas.Rows[e.RowIndex].Cells[1].Value?.ToString(), DgvBajas.Rows[e.RowIndex].Cells[2].Value?.ToString());
                else if (e.ColumnIndex >= 3) accesoDatos.ActualizarDatosProgramas(dni, DgvBajas.Columns[e.ColumnIndex].Name, DgvBajas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString());
            }
            finally { esCargaInterna = false; }
        }

        private void DgvBajas_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0 && DgvBajas.Rows[e.RowIndex].Cells[0].Value != null)
                dniOriginal = DgvBajas.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void DgvBajas_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DgvBajas.IsCurrentCellDirty && DgvBajas.CurrentCellAddress.X >= 3) DgvBajas.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        // test
        private void MaterialButton1_Click(object sender, EventArgs e) { accesoDatos.CargarDatos(DgvBajas); ConfigurarGrid(); }
    }
}