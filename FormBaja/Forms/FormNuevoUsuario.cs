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
        //--------------------------------------------------------------
        // ACCESO A DATOS
        //--------------------------------------------------------------
        public readonly AccesoDatos accesoDatos = new AccesoDatos();

        //--------------------------------------------------------------
        // CREACION DEL FOMULARIO INICIAL
        //--------------------------------------------------------------
        public FormNuevoUsuario()
        {
            InitializeComponent();
            GestorTema.ConfigurarMaterialSkin(this);


            
        }

        //--------------------------------------------------------------
        // BOTONES
        //--------------------------------------------------------------

        private void BtnGuardarUsuario_Click(object sender, EventArgs e)
        {
            // RECOGEMOS LOS DATOS DEL USUARIO
            Usuarios usuarioAGuardar = new Usuarios
            {
                Dni = TxtDNI.Text,
                Nombre = TxtNombre.Text,
                Apellidos = TxtApellidos.Text
            };

         
            
            try
                // LLAMAMOS A LA FUNCION QUE INSERTA EL USUARIO
                
            {
                accesoDatos.InsertarUsuario(usuarioAGuardar);

                TxtDNI.Clear();
                TxtNombre.Clear();
                TxtApellidos.Clear();

             

                // metodo mega magico que encontre para poder ponerl el %@!# foco en el txt dni porque 
                // no habia %@!# manera de ponerlo(ni con el focus, select ni activecontrol) por el %@!# materialskin2
                // que mu bonito pero da problemas
                this.SelectNextControl(null, true, true, true, false);

            }
            // SI FALLA SE MUESTRA EL ERROR Y SE LIMPIAN LOS CAMPOS
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                TxtDNI.Clear();
                TxtNombre.Clear();
                TxtApellidos.Clear();

                this.SelectNextControl(null, true, true, true, false);


            }

            
            


        }

    }
}
