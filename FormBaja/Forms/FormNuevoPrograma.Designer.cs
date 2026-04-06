namespace FormBaja.Forms
{
    partial class FormNuevoPrograma
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
            this.materialCardAnadirPrograma = new MaterialSkin.Controls.MaterialCard();
            this.BtnGuardarPrograma = new MaterialSkin.Controls.MaterialButton();
            this.TxtPrograma = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialCardAnadirPrograma.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialCardAnadirPrograma
            // 
            this.materialCardAnadirPrograma.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCardAnadirPrograma.Controls.Add(this.BtnGuardarPrograma);
            this.materialCardAnadirPrograma.Controls.Add(this.TxtPrograma);
            this.materialCardAnadirPrograma.Depth = 0;
            this.materialCardAnadirPrograma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialCardAnadirPrograma.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCardAnadirPrograma.Location = new System.Drawing.Point(3, 64);
            this.materialCardAnadirPrograma.Margin = new System.Windows.Forms.Padding(14);
            this.materialCardAnadirPrograma.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCardAnadirPrograma.Name = "materialCardAnadirPrograma";
            this.materialCardAnadirPrograma.Padding = new System.Windows.Forms.Padding(14);
            this.materialCardAnadirPrograma.Size = new System.Drawing.Size(794, 383);
            this.materialCardAnadirPrograma.TabIndex = 0;
            // 
            // BtnGuardarPrograma
            // 
            this.BtnGuardarPrograma.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnGuardarPrograma.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.BtnGuardarPrograma.Depth = 0;
            this.BtnGuardarPrograma.HighEmphasis = true;
            this.BtnGuardarPrograma.Icon = null;
            this.BtnGuardarPrograma.Location = new System.Drawing.Point(618, 327);
            this.BtnGuardarPrograma.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.BtnGuardarPrograma.MouseState = MaterialSkin.MouseState.HOVER;
            this.BtnGuardarPrograma.Name = "BtnGuardarPrograma";
            this.BtnGuardarPrograma.NoAccentTextColor = System.Drawing.Color.Empty;
            this.BtnGuardarPrograma.Size = new System.Drawing.Size(158, 36);
            this.BtnGuardarPrograma.TabIndex = 3;
            this.BtnGuardarPrograma.Text = "Guardar";
            this.BtnGuardarPrograma.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.BtnGuardarPrograma.UseAccentColor = false;
            this.BtnGuardarPrograma.UseVisualStyleBackColor = true;
            this.BtnGuardarPrograma.Click += new System.EventHandler(this.BtnGuardarPrograma_Click);
            // 
            // TxtPrograma
            // 
            this.TxtPrograma.AnimateReadOnly = false;
            this.TxtPrograma.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TxtPrograma.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtPrograma.Depth = 0;
            this.TxtPrograma.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TxtPrograma.HideSelection = true;
            this.TxtPrograma.Hint = "Nuevo programa";
            this.TxtPrograma.LeadingIcon = null;
            this.TxtPrograma.Location = new System.Drawing.Point(272, 167);
            this.TxtPrograma.MaxLength = 32767;
            this.TxtPrograma.MouseState = MaterialSkin.MouseState.OUT;
            this.TxtPrograma.Name = "TxtPrograma";
            this.TxtPrograma.PasswordChar = '\0';
            this.TxtPrograma.PrefixSuffixText = null;
            this.TxtPrograma.ReadOnly = false;
            this.TxtPrograma.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtPrograma.SelectedText = "";
            this.TxtPrograma.SelectionLength = 0;
            this.TxtPrograma.SelectionStart = 0;
            this.TxtPrograma.ShortcutsEnabled = true;
            this.TxtPrograma.Size = new System.Drawing.Size(250, 48);
            this.TxtPrograma.TabIndex = 2;
            this.TxtPrograma.TabStop = false;
            this.TxtPrograma.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtPrograma.TrailingIcon = null;
            this.TxtPrograma.UseSystemPasswordChar = false;
            // 
            // FormNuevoPrograma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.materialCardAnadirPrograma);
            this.Name = "FormNuevoPrograma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Añadir Programa";
            this.materialCardAnadirPrograma.ResumeLayout(false);
            this.materialCardAnadirPrograma.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialCard materialCardAnadirPrograma;
        private MaterialSkin.Controls.MaterialButton BtnGuardarPrograma;
        private MaterialSkin.Controls.MaterialTextBox2 TxtPrograma;
    }
}