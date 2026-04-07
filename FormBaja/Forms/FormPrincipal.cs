using Clases;
using FormBaja.Datos;
using FormBaja.Forms;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        //--------------------------------------------------------------
        // CREACION DEL FOMULARIO INICIAL
        //--------------------------------------------------------------


        public FormPrincipal()
        {
            InitializeComponent();


            GestorTema.ConfigurarMaterialSkin(this); // APLICR TEMA
            accesoDatos.CargarDatos(DgvBajas); // CARGA DE DATOS INICIAL

            
            ConfigurarTimerBusqueda(); // CONFIGURACION DEL TIMER

            this.WindowState = FormWindowState.Maximized;

        }

        //--------------------------------------------------------------
        // CARGA DEL FORMULARIO DESPUES DE LA CREACION
        //--------------------------------------------------------------
        private void FormBaja_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            accesoDatos.CargarDatos(DgvBajas); // CARGA DE DATOS INICIAL


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

                // Creamos la columna de tipo desplegable
                DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn
                {
                    Name = nombreColumna,
                    HeaderText = nombreColumna,
                    DataPropertyName = nombreColumna // Importante para que se enlace al DataTable
                };

                // Añadimos las opciones que mencionaste
                comboCol.Items.Add("");
                comboCol.Items.Add("ACTIVADO");
                comboCol.Items.Add("DESACTIVADO");

                // Intercambiamos la columna de texto por la de combo
                DgvBajas.Columns.RemoveAt(i);
                DgvBajas.Columns.Insert(i, comboCol);
            }
        }
        

        //--------------------------------------------------------------
        // CONFIGURAR EL GRID
        //--------------------------------------------------------------

        private void ConfigurarGrid()
        {
            ConvertirCeldasEnDesplegables();
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
            // Cada vez que el texto cambia, paramos y volvemos a arrancar el timer
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

        }

        private void BtnNuevoPrograma_Click(object sender, EventArgs e)
        {
            FormNuevoPrograma formNuevoPrograma = new FormNuevoPrograma();  
            formNuevoPrograma.ShowDialog();

            // ACTUALIZAMOS EL GRID LLAMANDO PRIMERO AL METODO PARA CONVERTIR LAS COLUMNAS EN DESPLEGABLES
            // Y LUEGO LLAMAMOS AL METODO QUE CARGA LOS DATOS
            
            accesoDatos.CargarDatos(DgvBajas);
            ConvertirCeldasEnDesplegables();
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("prueba");
        }



        // test
        private void MaterialButton1_Click(object sender, EventArgs e)
        {
            accesoDatos.CargarDatos(DgvBajas);
        }

        
    }
}
