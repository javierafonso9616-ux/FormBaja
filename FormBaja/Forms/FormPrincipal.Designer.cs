namespace FormBaja
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.materialCardBajaSuperior = new MaterialSkin.Controls.MaterialCard();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnNuevoPrograma = new MaterialSkin.Controls.MaterialButton();
            this.BtnNuevoUsuario = new MaterialSkin.Controls.MaterialButton();
            this.TxtBuscarDNIoNombre = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialCardBajaInferior = new MaterialSkin.Controls.MaterialCard();
            this.DgvBajas = new System.Windows.Forms.DataGridView();
            this.materialButton1 = new MaterialSkin.Controls.MaterialButton();
            this.materialCardBajaSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.materialCardBajaInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvBajas)).BeginInit();
            this.SuspendLayout();
            // 
            // materialCardBajaSuperior
            // 
            this.materialCardBajaSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCardBajaSuperior.Controls.Add(this.materialButton1);
            this.materialCardBajaSuperior.Controls.Add(this.pictureBox1);
            this.materialCardBajaSuperior.Controls.Add(this.BtnNuevoPrograma);
            this.materialCardBajaSuperior.Controls.Add(this.BtnNuevoUsuario);
            this.materialCardBajaSuperior.Controls.Add(this.TxtBuscarDNIoNombre);
            this.materialCardBajaSuperior.Depth = 0;
            this.materialCardBajaSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialCardBajaSuperior.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCardBajaSuperior.Location = new System.Drawing.Point(3, 64);
            this.materialCardBajaSuperior.Margin = new System.Windows.Forms.Padding(14);
            this.materialCardBajaSuperior.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCardBajaSuperior.Name = "materialCardBajaSuperior";
            this.materialCardBajaSuperior.Padding = new System.Windows.Forms.Padding(14);
            this.materialCardBajaSuperior.Size = new System.Drawing.Size(1914, 173);
            this.materialCardBajaSuperior.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FormBaja.Properties.Resources.Logotipo_Cruz_Roja_Horizontal_transparente_letras_negras;
            this.pictureBox1.Location = new System.Drawing.Point(17, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(451, 139);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // BtnNuevoPrograma
            // 
            this.BtnNuevoPrograma.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnNuevoPrograma.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.BtnNuevoPrograma.Depth = 0;
            this.BtnNuevoPrograma.HighEmphasis = true;
            this.BtnNuevoPrograma.Icon = null;
            this.BtnNuevoPrograma.Location = new System.Drawing.Point(1289, 92);
            this.BtnNuevoPrograma.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.BtnNuevoPrograma.MouseState = MaterialSkin.MouseState.HOVER;
            this.BtnNuevoPrograma.Name = "BtnNuevoPrograma";
            this.BtnNuevoPrograma.NoAccentTextColor = System.Drawing.Color.Empty;
            this.BtnNuevoPrograma.Size = new System.Drawing.Size(158, 36);
            this.BtnNuevoPrograma.TabIndex = 2;
            this.BtnNuevoPrograma.Text = "Añadir Programa";
            this.BtnNuevoPrograma.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.BtnNuevoPrograma.UseAccentColor = false;
            this.BtnNuevoPrograma.UseVisualStyleBackColor = true;
            this.BtnNuevoPrograma.Click += new System.EventHandler(this.BtnNuevoPrograma_Click);
            // 
            // BtnNuevoUsuario
            // 
            this.BtnNuevoUsuario.AutoSize = false;
            this.BtnNuevoUsuario.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnNuevoUsuario.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.BtnNuevoUsuario.Depth = 0;
            this.BtnNuevoUsuario.HighEmphasis = true;
            this.BtnNuevoUsuario.Icon = null;
            this.BtnNuevoUsuario.Location = new System.Drawing.Point(1289, 44);
            this.BtnNuevoUsuario.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.BtnNuevoUsuario.MouseState = MaterialSkin.MouseState.HOVER;
            this.BtnNuevoUsuario.Name = "BtnNuevoUsuario";
            this.BtnNuevoUsuario.NoAccentTextColor = System.Drawing.Color.Empty;
            this.BtnNuevoUsuario.Size = new System.Drawing.Size(158, 36);
            this.BtnNuevoUsuario.TabIndex = 1;
            this.BtnNuevoUsuario.Text = "Nuevo usuario";
            this.BtnNuevoUsuario.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.BtnNuevoUsuario.UseAccentColor = false;
            this.BtnNuevoUsuario.UseVisualStyleBackColor = true;
            this.BtnNuevoUsuario.Click += new System.EventHandler(this.BtnNuevoUsuario_Click);
            // 
            // TxtBuscarDNIoNombre
            // 
            this.TxtBuscarDNIoNombre.AnimateReadOnly = false;
            this.TxtBuscarDNIoNombre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TxtBuscarDNIoNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtBuscarDNIoNombre.Depth = 0;
            this.TxtBuscarDNIoNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TxtBuscarDNIoNombre.HideSelection = true;
            this.TxtBuscarDNIoNombre.Hint = "DNI";
            this.TxtBuscarDNIoNombre.LeadingIcon = null;
            this.TxtBuscarDNIoNombre.Location = new System.Drawing.Point(559, 62);
            this.TxtBuscarDNIoNombre.MaxLength = 32767;
            this.TxtBuscarDNIoNombre.MouseState = MaterialSkin.MouseState.OUT;
            this.TxtBuscarDNIoNombre.Name = "TxtBuscarDNIoNombre";
            this.TxtBuscarDNIoNombre.PasswordChar = '\0';
            this.TxtBuscarDNIoNombre.PrefixSuffixText = null;
            this.TxtBuscarDNIoNombre.ReadOnly = false;
            this.TxtBuscarDNIoNombre.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtBuscarDNIoNombre.SelectedText = "";
            this.TxtBuscarDNIoNombre.SelectionLength = 0;
            this.TxtBuscarDNIoNombre.SelectionStart = 0;
            this.TxtBuscarDNIoNombre.ShortcutsEnabled = true;
            this.TxtBuscarDNIoNombre.Size = new System.Drawing.Size(653, 48);
            this.TxtBuscarDNIoNombre.TabIndex = 0;
            this.TxtBuscarDNIoNombre.TabStop = false;
            this.TxtBuscarDNIoNombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtBuscarDNIoNombre.TrailingIcon = null;
            this.TxtBuscarDNIoNombre.UseSystemPasswordChar = false;
            this.TxtBuscarDNIoNombre.TextChanged += new System.EventHandler(this.TxtBuscarDNIoNombre_TextChanged);
            // 
            // materialCardBajaInferior
            // 
            this.materialCardBajaInferior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCardBajaInferior.Controls.Add(this.DgvBajas);
            this.materialCardBajaInferior.Depth = 0;
            this.materialCardBajaInferior.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialCardBajaInferior.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCardBajaInferior.Location = new System.Drawing.Point(3, 237);
            this.materialCardBajaInferior.Margin = new System.Windows.Forms.Padding(14);
            this.materialCardBajaInferior.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCardBajaInferior.Name = "materialCardBajaInferior";
            this.materialCardBajaInferior.Padding = new System.Windows.Forms.Padding(14);
            this.materialCardBajaInferior.Size = new System.Drawing.Size(1914, 840);
            this.materialCardBajaInferior.TabIndex = 1;
            // 
            // DgvBajas
            // 
            this.DgvBajas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvBajas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvBajas.Location = new System.Drawing.Point(14, 14);
            this.DgvBajas.Name = "DgvBajas";
            this.DgvBajas.Size = new System.Drawing.Size(1886, 812);
            this.DgvBajas.TabIndex = 0;
            this.DgvBajas.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvBajas_CellEnter);
            this.DgvBajas.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvBajas_CellValueChanged);
            this.DgvBajas.CurrentCellDirtyStateChanged += new System.EventHandler(this.DgvBajas_CurrentCellDirtyStateChanged);
            // 
            // materialButton1
            // 
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton1.Depth = 0;
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = null;
            this.materialButton1.Location = new System.Drawing.Point(500, 120);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton1.Size = new System.Drawing.Size(158, 36);
            this.materialButton1.TabIndex = 4;
            this.materialButton1.Text = "materialButton1";
            this.materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = false;
            this.materialButton1.UseVisualStyleBackColor = true;
            this.materialButton1.Click += new System.EventHandler(this.MaterialButton1_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.materialCardBajaInferior);
            this.Controls.Add(this.materialCardBajaSuperior);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Baja";
            this.Load += new System.EventHandler(this.FormBaja_Load);
            this.materialCardBajaSuperior.ResumeLayout(false);
            this.materialCardBajaSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.materialCardBajaInferior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvBajas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialCard materialCardBajaSuperior;
        private MaterialSkin.Controls.MaterialCard materialCardBajaInferior;
        private MaterialSkin.Controls.MaterialTextBox2 TxtBuscarDNIoNombre;
        private MaterialSkin.Controls.MaterialButton BtnNuevoPrograma;
        private MaterialSkin.Controls.MaterialButton BtnNuevoUsuario;
        private System.Windows.Forms.DataGridView DgvBajas;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialButton materialButton1;
    }
}

