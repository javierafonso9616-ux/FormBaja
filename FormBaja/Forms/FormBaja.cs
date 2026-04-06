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
    public partial class FormBaja : MaterialForm
    {
        public readonly AccesoDatos accesoDatos = new AccesoDatos();
        public FormBaja()
        {
            InitializeComponent();
            GestorTema.ConfigurarMaterialSkin(this);
            CargarDatos();

        }

        private void FormBaja_Load(object sender, EventArgs e)
        {

            
            
        }


        private void CargarDatos() {
            DgvBajas.DataSource = accesoDatos.LeerUsuarios();
        }




        // CONFIGURAR EL GRID
        private void ConfigurarGrid() { 
        
        }

        // TEXTBOX
        private void TxtBuscarDNIoNombre_TextChanged(object sender, EventArgs e)
        {

        }


        // BOTONES
        private void BtnNuevoUsuario_Click(object sender, EventArgs e)
        {
            FormNuevoUsuario formNuevoUsuario = new FormNuevoUsuario();
            formNuevoUsuario.ShowDialog();
            CargarDatos();

        }

        private void BtnNuevoPrograma_Click(object sender, EventArgs e)
        {
            FormNuevoPrograma formNuevoPrograma = new FormNuevoPrograma();  
            formNuevoPrograma.ShowDialog();
            CargarDatos();
        }

       
    }
}
