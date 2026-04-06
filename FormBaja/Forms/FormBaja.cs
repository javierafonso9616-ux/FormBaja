using Clases;
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
        public FormBaja()
        {
            InitializeComponent();
            GestorTema.ConfigurarMaterialSkin(this);
        }

        private void TxtBuscarDNIoNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnNuevoUsuario_Click(object sender, EventArgs e)
        {

        }

        private void BtnNuevoPrograma_Click(object sender, EventArgs e)
        {

        }
    }
}
