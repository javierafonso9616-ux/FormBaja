namespace FormBaja
{
    partial class FormInicial
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
            this.materialCardFormInicial = new MaterialSkin.Controls.MaterialCard();
            this.BtnCrearUsuario = new MaterialSkin.Controls.MaterialButton();
            this.TxtDni = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialCardFormInicial.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialCardFormInicial
            // 
            this.materialCardFormInicial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCardFormInicial.Controls.Add(this.BtnCrearUsuario);
            this.materialCardFormInicial.Controls.Add(this.TxtDni);
            this.materialCardFormInicial.Depth = 0;
            this.materialCardFormInicial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialCardFormInicial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCardFormInicial.Location = new System.Drawing.Point(3, 64);
            this.materialCardFormInicial.Margin = new System.Windows.Forms.Padding(14);
            this.materialCardFormInicial.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCardFormInicial.Name = "materialCardFormInicial";
            this.materialCardFormInicial.Padding = new System.Windows.Forms.Padding(14);
            this.materialCardFormInicial.Size = new System.Drawing.Size(794, 270);
            this.materialCardFormInicial.TabIndex = 0;
            // 
            // BtnCrearUsuario
            // 
            this.BtnCrearUsuario.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnCrearUsuario.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.BtnCrearUsuario.Depth = 0;
            this.BtnCrearUsuario.HighEmphasis = true;
            this.BtnCrearUsuario.Icon = null;
            this.BtnCrearUsuario.Location = new System.Drawing.Point(93, 184);
            this.BtnCrearUsuario.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.BtnCrearUsuario.MouseState = MaterialSkin.MouseState.HOVER;
            this.BtnCrearUsuario.Name = "BtnCrearUsuario";
            this.BtnCrearUsuario.NoAccentTextColor = System.Drawing.Color.Empty;
            this.BtnCrearUsuario.Size = new System.Drawing.Size(132, 36);
            this.BtnCrearUsuario.TabIndex = 1;
            this.BtnCrearUsuario.Text = "Crear usuario";
            this.BtnCrearUsuario.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.BtnCrearUsuario.UseAccentColor = false;
            this.BtnCrearUsuario.UseVisualStyleBackColor = true;
            // 
            // TxtDni
            // 
            this.TxtDni.AnimateReadOnly = false;
            this.TxtDni.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TxtDni.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtDni.Depth = 0;
            this.TxtDni.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TxtDni.HideSelection = true;
            this.TxtDni.Hint = "DNI";
            this.TxtDni.LeadingIcon = null;
            this.TxtDni.Location = new System.Drawing.Point(93, 74);
            this.TxtDni.MaxLength = 32767;
            this.TxtDni.MouseState = MaterialSkin.MouseState.OUT;
            this.TxtDni.Name = "TxtDni";
            this.TxtDni.PasswordChar = '\0';
            this.TxtDni.PrefixSuffixText = null;
            this.TxtDni.ReadOnly = false;
            this.TxtDni.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtDni.SelectedText = "";
            this.TxtDni.SelectionLength = 0;
            this.TxtDni.SelectionStart = 0;
            this.TxtDni.ShortcutsEnabled = true;
            this.TxtDni.Size = new System.Drawing.Size(592, 48);
            this.TxtDni.TabIndex = 0;
            this.TxtDni.TabStop = false;
            this.TxtDni.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtDni.TrailingIcon = null;
            this.TxtDni.UseSystemPasswordChar = false;
            // 
            // FormInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 337);
            this.Controls.Add(this.materialCardFormInicial);
            this.Name = "FormInicial";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Introduzca usuario";
            this.materialCardFormInicial.ResumeLayout(false);
            this.materialCardFormInicial.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialCard materialCardFormInicial;
        private MaterialSkin.Controls.MaterialTextBox2 TxtDni;
        private MaterialSkin.Controls.MaterialButton BtnCrearUsuario;
    }
}