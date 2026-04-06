using MaterialSkin;
using MaterialSkin.Controls;

namespace Clases
{
    internal class GestorTema
    {
        public static void ConfigurarMaterialSkin(MaterialForm formulario)
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(formulario);

            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue900,
                Primary.Blue900,
                Primary.Blue200,
                Accent.Blue700,
                TextShade.WHITE);

        }
    }
}
