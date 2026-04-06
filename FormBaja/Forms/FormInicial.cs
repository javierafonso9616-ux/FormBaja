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
using Clases;


namespace FormBaja
{
    public partial class FormInicial : MaterialForm
    {
        public FormInicial()
        {
            InitializeComponent();
            
            GestorTema.ConfigurarMaterialSkin(this);
        }
    }
}
