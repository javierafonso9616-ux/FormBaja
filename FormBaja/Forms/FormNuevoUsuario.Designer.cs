namespace FormBaja.Forms
{
    partial class FormNuevoUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.materialCardNuevoUsuario = new MaterialSkin.Controls.MaterialCard();
            this.TxtDNI = new MaterialSkin.Controls.MaterialTextBox2();
            this.TxtNombre = new MaterialSkin.Controls.MaterialTextBox2();
            this.TxtApellidos = new MaterialSkin.Controls.MaterialTextBox2();
            this.BtnGuardarUsuario = new MaterialSkin.Controls.MaterialButton();
            this.materialCardNuevoUsuario.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialCardNuevoUsuario
            // 
            this.materialCardNuevoUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCardNuevoUsuario.Controls.Add(this.BtnGuardarUsuario);
            this.materialCardNuevoUsuario.Controls.Add(this.TxtApellidos);
            this.materialCardNuevoUsuario.Controls.Add(this.TxtNombre);
            this.materialCardNuevoUsuario.Controls.Add(this.TxtDNI);
            this.materialCardNuevoUsuario.Depth = 0;
            this.materialCardNuevoUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialCardNuevoUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCardNuevoUsuario.Location = new System.Drawing.Point(3, 64);
            this.materialCardNuevoUsuario.Margin = new System.Windows.Forms.Padding(14);
            this.materialCardNuevoUsuario.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCardNuevoUsuario.Name = "materialCardNuevoUsuario";
            this.materialCardNuevoUsuario.Padding = new System.Windows.Forms.Padding(14);
            this.materialCardNuevoUsuario.Size = new System.Drawing.Size(794, 383);
            this.materialCardNuevoUsuario.TabIndex = 0;
            // 
            // TxtDNI
            // 
            this.TxtDNI.AnimateReadOnly = false;
            this.TxtDNI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TxtDNI.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtDNI.Depth = 0;
            this.TxtDNI.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TxtDNI.HideSelection = true;
            this.TxtDNI.Hint = "DNI";
            this.TxtDNI.LeadingIcon = null;
            this.TxtDNI.Location = new System.Drawing.Point(272, 79);
            this.TxtDNI.MaxLength = 32767;
            this.TxtDNI.MouseState = MaterialSkin.MouseState.OUT;
            this.TxtDNI.Name = "TxtDNI";
            this.TxtDNI.PasswordChar = '\0';
            this.TxtDNI.PrefixSuffixText = null;
            this.TxtDNI.ReadOnly = false;
            this.TxtDNI.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtDNI.SelectedText = "";
            this.TxtDNI.SelectionLength = 0;
            this.TxtDNI.SelectionStart = 0;
            this.TxtDNI.ShortcutsEnabled = true;
            this.TxtDNI.Size = new System.Drawing.Size(250, 48);
            this.TxtDNI.TabIndex = 0;
            this.TxtDNI.TabStop = false;
            this.TxtDNI.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtDNI.TrailingIcon = null;
            this.TxtDNI.UseSystemPasswordChar = false;
            // 
            // TxtNombre
            // 
            this.TxtNombre.AnimateReadOnly = false;
            this.TxtNombre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TxtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtNombre.Depth = 0;
            this.TxtNombre.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TxtNombre.HideSelection = true;
            this.TxtNombre.Hint = "Nombre";
            this.TxtNombre.LeadingIcon = null;
            this.TxtNombre.Location = new System.Drawing.Point(272, 169);
            this.TxtNombre.MaxLength = 32767;
            this.TxtNombre.MouseState = MaterialSkin.MouseState.OUT;
            this.TxtNombre.Name = "TxtNombre";
            this.TxtNombre.PasswordChar = '\0';
            this.TxtNombre.PrefixSuffixText = null;
            this.TxtNombre.ReadOnly = false;
            this.TxtNombre.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtNombre.SelectedText = "";
            this.TxtNombre.SelectionLength = 0;
            this.TxtNombre.SelectionStart = 0;
            this.TxtNombre.ShortcutsEnabled = true;
            this.TxtNombre.Size = new System.Drawing.Size(250, 48);
            this.TxtNombre.TabIndex = 1;
            this.TxtNombre.TabStop = false;
            this.TxtNombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtNombre.TrailingIcon = null;
            this.TxtNombre.UseSystemPasswordChar = false;
            // 
            // TxtApellidos
            // 
            this.TxtApellidos.AnimateReadOnly = false;
            this.TxtApellidos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TxtApellidos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtApellidos.Depth = 0;
            this.TxtApellidos.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TxtApellidos.HideSelection = true;
            this.TxtApellidos.Hint = "Apellidos";
            this.TxtApellidos.LeadingIcon = null;
            this.TxtApellidos.Location = new System.Drawing.Point(272, 255);
            this.TxtApellidos.MaxLength = 32767;
            this.TxtApellidos.MouseState = MaterialSkin.MouseState.OUT;
            this.TxtApellidos.Name = "TxtApellidos";
            this.TxtApellidos.PasswordChar = '\0';
            this.TxtApellidos.PrefixSuffixText = null;
            this.TxtApellidos.ReadOnly = false;
            this.TxtApellidos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtApellidos.SelectedText = "";
            this.TxtApellidos.SelectionLength = 0;
            this.TxtApellidos.SelectionStart = 0;
            this.TxtApellidos.ShortcutsEnabled = true;
            this.TxtApellidos.Size = new System.Drawing.Size(250, 48);
            this.TxtApellidos.TabIndex = 2;
            this.TxtApellidos.TabStop = false;
            this.TxtApellidos.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtApellidos.TrailingIcon = null;
            this.TxtApellidos.UseSystemPasswordChar = false;
            // 
            // BtnGuardarUsuario
            // 
            this.BtnGuardarUsuario.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnGuardarUsuario.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.BtnGuardarUsuario.Depth = 0;
            this.BtnGuardarUsuario.HighEmphasis = true;
            this.BtnGuardarUsuario.Icon = null;
            this.BtnGuardarUsuario.Location = new System.Drawing.Point(618, 327);
            this.BtnGuardarUsuario.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.BtnGuardarUsuario.MouseState = MaterialSkin.MouseState.HOVER;
            this.BtnGuardarUsuario.Name = "BtnGuardarUsuario";
            this.BtnGuardarUsuario.NoAccentTextColor = System.Drawing.Color.Empty;
            this.BtnGuardarUsuario.Size = new System.Drawing.Size(153, 36);
            this.BtnGuardarUsuario.TabIndex = 3;
            this.BtnGuardarUsuario.Text = "Guardar Usuario";
            this.BtnGuardarUsuario.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.BtnGuardarUsuario.UseAccentColor = false;
            this.BtnGuardarUsuario.UseVisualStyleBackColor = true;
            this.BtnGuardarUsuario.Click += new System.EventHandler(this.BtnGuardarUsuario_Click);
            // 
            // FormNuevoUsuario
            // 
            this.AcceptButton = this.BtnGuardarUsuario;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.materialCardNuevoUsuario);
            this.Name = "FormNuevoUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nuevo Usuario";
            this.materialCardNuevoUsuario.ResumeLayout(false);
            this.materialCardNuevoUsuario.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialCard materialCardNuevoUsuario;
        private MaterialSkin.Controls.MaterialTextBox2 TxtDNI;
        private MaterialSkin.Controls.MaterialTextBox2 TxtApellidos;
        private MaterialSkin.Controls.MaterialTextBox2 TxtNombre;
        private MaterialSkin.Controls.MaterialButton BtnGuardarUsuario;
    }
}