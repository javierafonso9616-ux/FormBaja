using Clases;
using FormBaja.Datos;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace FormBaja.Forms
{

    public partial class FormNuevoPrograma : MaterialForm
    {
        //--------------------------------------------------------------
        // ACCESO A DATOS
        //--------------------------------------------------------------

        public readonly AccesoDatos accesoDatos = new AccesoDatos();

        //--------------------------------------------------------------
        // CREACION DEL FOMULARIO INICIAL
        //--------------------------------------------------------------
        public FormNuevoPrograma()
        {
            InitializeComponent();
            GestorTema.ConfigurarMaterialSkin(this);



            // ESTO ES PARA QUE EL ESCAPE CIERRE EL FORMULARIO
            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            };
        }


        //--------------------------------------------------------------
        // BOTONES
        //--------------------------------------------------------------
        private void BtnGuardarPrograma_Click(object sender, EventArgs e)
        {
            // RECOGEMOS EL NOMBRE DEL PROGRAMA
            string nombrePrograma = TxtPrograma.Text.Trim().ToUpper();


            
            try
                // LLAMAMOS A LA FUNCION QUE INSERTA EL PROGRAMA
                
            {
                accesoDatos.AñadirColumnaPrograma(nombrePrograma);
               
                TxtPrograma.Clear();
                

                
            }

            // SI FALLA SE MUESTRA EL ERROR Y SE LIMPIA EL CAMPO
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                TxtPrograma.Clear();
            }
        }

       
        
    }
}
