using Clases;
using FormBaja.Datos;
using FormBaja.Entidades;
using MaterialSkin.Controls;
using System;
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



                // METODO MEGA MAGICO QUE ENCONTRE PARA PODER PONERL EL %@!# FOCO EN EL TXT DNI PORQUE 
                // NO HABIA %@!# MANERA DE PONERLO(NI CON EL FOCUS, SELECT NI ACTIVECONTROL) POR EL %@!# MATERIALSKIN2
                // QUE MU BONITO PERO DA PROBLEMAS
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
