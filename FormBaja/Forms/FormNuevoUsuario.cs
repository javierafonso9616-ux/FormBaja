using Clases;
using DocumentFormat.OpenXml.Bibliography;
using FormBaja.Datos;
using FormBaja.Entidades;
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

namespace FormBaja.Forms
{
    


    public partial class FormNuevoUsuario : MaterialForm
    {
        public readonly AccesoDatos accesoDatos = new AccesoDatos();

        public FormNuevoUsuario()
        {
            InitializeComponent();
            GestorTema.ConfigurarMaterialSkin(this);

          
        }


        private void BtnGuardarUsuario_Click(object sender, EventArgs e)
        {
            Usuarios usuarioAGuardar = new Usuarios
            {
                Dni = TxtDNI.Text,
                Nombre = TxtNombre.Text,
                Apellidos = TxtApellidos.Text
            };

         
            
            try
            {
                accesoDatos.InsertarUsuario(usuarioAGuardar);
                MessageBox.Show("Usuario añadido correctamente.");
                Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                TxtDNI.Clear();
                TxtNombre.Clear();
                TxtApellidos.Clear();
            }

            
        }
    }
}
