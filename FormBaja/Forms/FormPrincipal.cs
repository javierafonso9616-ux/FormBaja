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
        // ACCESO A DATOS
        //--------------------------------------------------------------
        public readonly AccesoDatos accesoDatos = new AccesoDatos();

        

        //--------------------------------------------------------------
        // CREACION DEL FOMULARIO INICIAL
        //--------------------------------------------------------------
        public FormPrincipal()
        {
            InitializeComponent();
            GestorTema.ConfigurarMaterialSkin(this); // APLICR TEMA
            accesoDatos.CargarDatos(DgvBajas); // CARGA DE DATOS INICIAL

        }

        //--------------------------------------------------------------
        // CARGA DEL FORMULARIO DESPUES DE LA CREACION
        //--------------------------------------------------------------
        private void FormBaja_Load(object sender, EventArgs e)
        {

            
            
        }

        //--------------------------------------------------------------
        // METODOS
        //--------------------------------------------------------------

        
        private void TimerBusqueda(int ms) {


            
        
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
            string busqueda = TxtBuscarDNIoNombre.Text.ToUpper().Trim();

            TimerBusqueda(300);

            try
            {
                // Pasamos el DataGridView y el texto a buscar
                accesoDatos.BuscarUsuario(DgvBajas, busqueda);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
