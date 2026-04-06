using Clases;
using FormBaja.Datos;
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

    public partial class FormNuevoPrograma : MaterialForm
    {
        public readonly AccesoDatos accesoDatos = new AccesoDatos();

        public FormNuevoPrograma()
        {
            InitializeComponent();
            GestorTema.ConfigurarMaterialSkin(this);
        }

        private void BtnGuardarPrograma_Click(object sender, EventArgs e)
        {

        }
    }
}
