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
        private readonly Timer timerBusqueda;


        //--------------------------------------------------------------
        // CREACION DEL FOMULARIO INICIAL
        //--------------------------------------------------------------


        public FormPrincipal()
        {
            InitializeComponent();
            GestorTema.ConfigurarMaterialSkin(this); // APLICR TEMA
            accesoDatos.CargarDatos(DgvBajas); // CARGA DE DATOS INICIAL

            // CONFIGURACION DEL TIMER
            timerBusqueda = new Timer();
            timerBusqueda.Interval = 500; // medio segundo
            timerBusqueda.Tick += BusquedaDelay;

            this.WindowState = FormWindowState.Maximized;
        }

        //--------------------------------------------------------------
        // CARGA DEL FORMULARIO DESPUES DE LA CREACION
        //--------------------------------------------------------------
        private void FormBaja_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();



        }

        //--------------------------------------------------------------
        // METODOS
        //--------------------------------------------------------------


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



        //--------------------------------------------------------------
        // CONFIGURAR EL GRID
        //--------------------------------------------------------------
        private void ConfigurarGrid() { 
        
        }

        //--------------------------------------------------------------
        // TEXTBOX
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

            accesoDatos.CargarDatos(DgvBajas);
        }

       
    }
}
