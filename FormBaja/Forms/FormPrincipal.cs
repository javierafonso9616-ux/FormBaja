using Clases;
using FormBaja.Datos;
using FormBaja.Forms;
using MaterialSkin.Controls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FormBaja
{
    public partial class FormPrincipal : MaterialForm
    {
        
       
        //--------------------------------------------------------------
        // ATRIBUTOS
        //--------------------------------------------------------------
        public readonly AccesoDatos accesoDatos = new AccesoDatos();
        private Timer timerBusqueda;
        
        // [DEBUG] cronometro
        // private Stopwatch cronometroCarga;

        //--------------------------------------------------------------
        // CREACION DEL FOMULARIO INICIAL
        //--------------------------------------------------------------


        public FormPrincipal()
        {
           // [DEBUG] cronometro
           //cronometroCarga = Stopwatch.StartNew();


            InitializeComponent();

            this.Opacity = 0;

            GestorTema.ConfigurarMaterialSkin(this); // APLICR TEMA
            

            //----------------------------------------------------------------
            //  CODIGO PARA AGILIZAR LA CARGA DEL PROGRAMA
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                    null, DgvBajas, new object[] { true });

            //----------------------------------------------------------------

          

            ConfigurarTimerBusqueda(); // CONFIGURACION DEL TIMER   

            // CENTRADO DE FORMULARIO PARAA EVITAR QUE TAPE LA BARRA DE WINDOWS Y SE PONGA EN PANTALLA MAXIMIZADA( NO COMPLETA)
            this.StartPosition = FormStartPosition.Manual;
            this.Bounds = Screen.PrimaryScreen.WorkingArea;
            

        }

        //--------------------------------------------------------------
        // CARGA DEL FORMULARIO DESPUES DE LA CREACION
        //--------------------------------------------------------------
        private void FormPrincipal_Shown(object sender, EventArgs e)
        {
            this.SuspendLayout();

            accesoDatos.CargarDatos(DgvBajas); // CARGA DE DATOS INICIAL
            ConfigurarGrid();

            this.ResumeLayout();
            this.Opacity = 1;



            //  [DEBUG] cronometro
            //  cronometroCarga.Stop();
            //  long milisegundos = cronometroCarga.ElapsedMilliseconds;
            //  Debug.WriteLine($">>> TIEMPO TOTAL DE CARGA: {milisegundos} ms");
        }

        // METODO PARA AGILIZAR LA CARGA DEL PROGRAMA
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED: Dibuja todos los hijos de abajo hacia arriba
                return cp;
            }
        }
        //--------------------------------------------------------------
        // METODOS
        //--------------------------------------------------------------

        private void ConfigurarTimerBusqueda()
        {
            
            timerBusqueda = new Timer { Interval = 500 };
            timerBusqueda.Tick += BusquedaDelay;
        }

        private void BusquedaDelay(object sender, EventArgs e)
        {
            // se para el timer para evitar que se ejecute varias veces
            timerBusqueda.Stop();

            string busqueda = TxtBuscarDNIoNombre.Text.ToUpper().Trim();

            try
            {
                // ejecutamos la búsqueda en la base de datos
                accesoDatos.BuscarUsuario(DgvBajas, busqueda);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message);
            }
        }

        private void ConvertirCeldasEnDesplegables() {
            // Recorremos todas las columnas a partir de la 3 (donde empiezan los programas)
            // DNI (0), NOMBRE (1), APELLIDOS (2) se quedan como texto
            for (int i = 3; i < DgvBajas.Columns.Count; i++)
            {
                string nombreColumna = DgvBajas.Columns[i].Name;

                // CREAMOS LA NUEVA COLUMNA CON EL NOMBRE DEL PROGRAMA Y LOS ESTILOS
                DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn
                {
                    Name = nombreColumna,
                    HeaderText = nombreColumna,
                    DataPropertyName = nombreColumna, 
                    FlatStyle = FlatStyle.Flat,                   
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                };

                // COLORES DE LAS CELDAS DESPLEGABLE
                comboCol.DefaultCellStyle.BackColor = Color.White;
                comboCol.DefaultCellStyle.ForeColor = Color.Black;
                comboCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 222, 251);


                // OPCIONES DEL DESPLEGABLE
                comboCol.Items.Add("");
                comboCol.Items.Add("ACTIVO");
                comboCol.Items.Add("INACTIVO");
                                    

                // REEMPLAZAMOS LA COLUMNA POR LA NUEVA CON DESPLEGABLE
                DgvBajas.Columns.RemoveAt(i);
                DgvBajas.Columns.Insert(i, comboCol);
            }
        }


        //--------------------------------------------------------------
        // CONFIGURAR EL GRID
        //--------------------------------------------------------------

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

        //--------------------------------------------------------------
        // EVENTOS DEL GRID
        //--------------------------------------------------------------

        // EVENTO PARA QUE LOS DESPLEGABLES FUNCIONEN CON UN CLICK EN LUGAR DE DOS POR DEFECTO
        private void DgvBajas_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvBajas.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && e.RowIndex != -1)
            {
                DgvBajas.BeginEdit(true);
                ((ComboBox)DgvBajas.EditingControl).DroppedDown = true;
            }
        }

        // EVENTO PARA FORZAR AL GRID A "CONFIRMAR" LOS CAMBIOS AL ELEGIR UNA OPCION
        private void DgvBajas_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DgvBajas.IsCurrentCellDirty)
            {
                DgvBajas.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // EVENTO QUE MANDA LOS DATOS A LA BASE DE DATOS
        private void DgvBajas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // EVITAR QUE SE EJECUTE EN EL ENCABEZADO
            if (e.RowIndex < 0) return;

            // SI LA COLUMNA ES UN DESPLEGABLE
            if (e.ColumnIndex >= 3)
            {
                try
                {
                    // PILLAMOS EL DNI (CLAVE PRIMARIA)
                    string dni = DgvBajas.Rows[e.RowIndex].Cells[0].Value.ToString();

                    // NOMBRE DEL PROGRAMA
                    string programa = DgvBajas.Columns[e.ColumnIndex].Name;

                    // OBTENEMOS EL NUEVO VALOR
                    string nuevoValor = DgvBajas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

                    // LLAMAMOS AL METODO PARA INSERTAR LOS DATOS NUEVOS
                    accesoDatos.ActualizarDatosProgramas(dni, programa, nuevoValor);
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //--------------------------------------------------------------
        // EVENTOS DEL TEXTBOX 
        //--------------------------------------------------------------

        private void TxtBuscarDNIoNombre_TextChanged(object sender, EventArgs e)
        {
            
            // CADA VEZ QUE EL TEXTO CAMBIA, PARAMOS Y INICIAMOS EL TIMER
            // PARA QUE NO SE EJECUTE VARIAS VECES SEGUIDAS
           
            timerBusqueda.Stop();
            timerBusqueda.Start();
        }

        

        //--------------------------------------------------------------
        // BOTONES
        //--------------------------------------------------------------

        private void BtnNuevoUsuario_Click(object sender, EventArgs e)
        {
            FormNuevoUsuario formNuevoUsuario = new FormNuevoUsuario();
            formNuevoUsuario.ShowDialog();

            accesoDatos.CargarDatos(DgvBajas);
            ConfigurarGrid();

        }

        private void BtnNuevoPrograma_Click(object sender, EventArgs e)
        {
            FormNuevoPrograma formNuevoPrograma = new FormNuevoPrograma();  
            formNuevoPrograma.ShowDialog();

            // ACTUALIZAMOS EL GRID LLAMANDO PRIMERO AL METODO PARA CONVERTIR LAS COLUMNAS EN DESPLEGABLES
            // Y LUEGO LLAMAMOS AL METODO QUE CARGA LOS DATOS
            
            accesoDatos.CargarDatos(DgvBajas);
            ConfigurarGrid();
            
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            string txtBusqueda = TxtBuscarDNIoNombre.Text.ToUpper().Trim();
            try
            {
                // PASAMOS EL DATAGRIDVIEW AL METODO PARA EXPORTAR
                accesoDatos.ExportarExcel(DgvBajas, txtBusqueda);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://hospitalcrgijon.com/");

        }






        // test
        private void MaterialButton1_Click(object sender, EventArgs e)
        {
            accesoDatos.CargarDatos(DgvBajas); // CARGA DE DATOS INICIAL
            ConfigurarGrid();
        }

        
    }
}
