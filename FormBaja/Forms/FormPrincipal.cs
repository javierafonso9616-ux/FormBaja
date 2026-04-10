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

        private void ConfigurarGrid()
        {
            DgvBajas.SuspendLayout();

            // 1. Permitimos edición general para DNI, Nombre y Apellidos
            DgvBajas.ReadOnly = false;

            ConvertirCeldasEnDesplegables();

            DgvBajas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            for (int i = 0; i < DgvBajas.Columns.Count; i++)
            {
                int anchoCalculado = DgvBajas.Columns[i].Width;
                DgvBajas.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                DgvBajas.Columns[i].Width = anchoCalculado;

                // Bloqueamos explícitamente las columnas de programas (índice 3 en adelante)
                if (i >= 3) DgvBajas.Columns[i].ReadOnly = true;
            }

            if (DgvBajas.Columns.Count > 2)
            {
                DgvBajas.Columns[1].Width = 100;
                DgvBajas.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                DgvBajas.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DgvBajas.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            DgvBajas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            DgvBajas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DgvBajas.AllowUserToResizeRows = false;
            DgvBajas.RowHeadersVisible = false;

            DgvBajas.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            DgvBajas.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

            DgvBajas.BackgroundColor = Color.White;
            DgvBajas.GridColor = Color.FromArgb(230, 230, 230);
            DgvBajas.EnableHeadersVisualStyles = false;
            DgvBajas.ColumnHeadersHeight = 60;
            DgvBajas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            DgvBajas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DgvBajas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            DgvBajas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // fuente especial para el dni
            DgvBajas.Columns[0].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            DgvBajas.ResumeLayout();
        }

        private void ConvertirCeldasEnDesplegables()
        {
            for (int i = 3; i < DgvBajas.Columns.Count; i++)
            {
                string nom = DgvBajas.Columns[i].Name;
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
                    ReadOnly = true // BLOQUEO INDIVIDUAL
                };

                comboCol.Items.AddRange("", "ACTIVO", "INACTIVO");
                DgvBajas.Columns.RemoveAt(i);
                DgvBajas.Columns.Insert(i, comboCol);
            }
        }

        // --- EL BLOQUEO DEFINITIVO ESTÁ AQUÍ ---
        private void DgvBajas_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Si el usuario intenta editar columnas de programas (3 en adelante), cancelamos
            if (e.ColumnIndex >= 3)
            {
                e.Cancel = true;
                return;
            }

            // Si es la columna DNI (0), guardamos el valor original para la actualización
            if (e.ColumnIndex == 0 && DgvBajas.Rows[e.RowIndex].Cells[0].Value != null)
                dniOriginal = DgvBajas.Rows[e.RowIndex].Cells[0].Value.ToString().ToUpper().Trim();
        }

        private void DgvBajas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || esCargaInterna) return;
            try
            {
                esCargaInterna = true;
                string dniActual = DgvBajas.Rows[e.RowIndex].Cells[0].Value?.ToString().Trim().ToUpper();

                if (e.ColumnIndex == 0)
                    accesoDatos.ActualizarDniUsuario(dniOriginal, dniActual);
                else if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
                    accesoDatos.ActualizarDatosUsuarios(dniActual, DgvBajas.Rows[e.RowIndex].Cells[1].Value?.ToString().ToUpper().Trim(), DgvBajas.Rows[e.RowIndex].Cells[2].Value?.ToString().ToUpper().Trim());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { esCargaInterna = false;

                accesoDatos.CargarDatos(DgvBajas);
                ConfigurarGrid();
            }
        }

        private void AbrirDetallesUsuario()
        {
            if (DgvBajas.SelectedRows.Count > 0)
            {
                string dni = DgvBajas.SelectedRows[0].Cells[0].Value.ToString().Trim().ToUpper();
                string nombre = DgvBajas.SelectedRows[0].Cells[1].Value.ToString().Trim().ToUpper();
                string apellido = DgvBajas.SelectedRows[0].Cells[2].Value.ToString().Trim().ToUpper();
                if (new FormDetallesUsuario(dni, nombre, apellido).ShowDialog() == DialogResult.OK)
                {
                    accesoDatos.CargarDatos(DgvBajas);
                    ConfigurarGrid();
                }
            }
        }

        private void DgvBajas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 3) return;
            if (e.Value != null)
            {
                string v = e.Value.ToString().ToUpper().Trim();
                if (v == "ACTIVO") e.CellStyle.ForeColor = Color.Green;
                else if (v == "INACTIVO") e.CellStyle.ForeColor = Color.Red;
            }
        }

        private void ConfigurarTimerBusqueda()
        {
            timerBusqueda = new Timer { Interval = 500 };
            timerBusqueda.Tick += (s, e) => {
                timerBusqueda.Stop();
                accesoDatos.BuscarUsuario(DgvBajas, TxtBuscarDNIoNombre.Text.ToUpper().Trim());
            };
        }

        private void ConfigurarMenuContextual()
        {
            menuContextual = new ContextMenuStrip();
            ToolStripMenuItem det = new ToolStripMenuItem("Ver Detalles");
            det.Click += (s, e) => AbrirDetallesUsuario();
            ToolStripMenuItem bor = new ToolStripMenuItem("Borrar");
            bor.Click += (s, e) => {
                if (DgvBajas.SelectedRows.Count > 0)
                {
                    string dni = DgvBajas.SelectedRows[0].Cells[0].Value.ToString().ToUpper().Trim();
                    if (MessageBox.Show("¿Borrar?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        accesoDatos.BorrarRegistro(dni);
                        accesoDatos.CargarDatos(DgvBajas);
                        ConfigurarGrid();
                    }
                }
            };
            menuContextual.Items.Add(det); menuContextual.Items.Add(bor);
        }

        private void DgvBajas_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                DgvBajas.ClearSelection(); DgvBajas.Rows[e.RowIndex].Selected = true;
                menuContextual.Show(Cursor.Position);
            }
        }

        private void TxtBuscarDNIoNombre_TextChanged(object sender, EventArgs e) { timerBusqueda.Stop(); timerBusqueda.Start(); }
        private void BtnNuevoUsuario_Click(object sender, EventArgs e) { new FormNuevoUsuario().ShowDialog(); accesoDatos.CargarDatos(DgvBajas); ConfigurarGrid(); }
        private void BtnNuevoPrograma_Click(object sender, EventArgs e) { new FormNuevoPrograma().ShowDialog(); accesoDatos.CargarDatos(DgvBajas); ConfigurarGrid(); }
        private void BtnExportar_Click(object sender, EventArgs e) => accesoDatos.ExportarExcel(accesoDatos.LeerUsuarios(), "Usuarios");
        private void PictureBox1_Click(object sender, EventArgs e) => Process.Start("https://hospitalcrgijon.com/");
    }
}