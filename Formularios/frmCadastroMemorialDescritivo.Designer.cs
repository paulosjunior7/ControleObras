namespace ControleObras.Formularios
{
    partial class frmCadastroMemorialDescritivo
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
            this.components = new System.ComponentModel.Container();
            this.edtDescricaoGrupo = new System.Windows.Forms.TextBox();
            this.edtcodgrupo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lista = new System.Windows.Forms.TreeView();
            this.btnincluir = new System.Windows.Forms.Button();
            this.btnremover = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.gruposBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gruposBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gruposBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gruposBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // edtDescricaoGrupo
            // 
            this.edtDescricaoGrupo.Location = new System.Drawing.Point(117, 85);
            this.edtDescricaoGrupo.Name = "edtDescricaoGrupo";
            this.edtDescricaoGrupo.Size = new System.Drawing.Size(330, 20);
            this.edtDescricaoGrupo.TabIndex = 3;
            // 
            // edtcodgrupo
            // 
            this.edtcodgrupo.Location = new System.Drawing.Point(13, 85);
            this.edtcodgrupo.Name = "edtcodgrupo";
            this.edtcodgrupo.Size = new System.Drawing.Size(98, 20);
            this.edtcodgrupo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Grupos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Código";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(117, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Descrição";
            // 
            // lista
            // 
            this.lista.Location = new System.Drawing.Point(12, 140);
            this.lista.Name = "lista";
            this.lista.Size = new System.Drawing.Size(435, 361);
            this.lista.TabIndex = 55;
            // 
            // btnincluir
            // 
            this.btnincluir.Location = new System.Drawing.Point(291, 111);
            this.btnincluir.Name = "btnincluir";
            this.btnincluir.Size = new System.Drawing.Size(75, 23);
            this.btnincluir.TabIndex = 56;
            this.btnincluir.Text = "Incluir";
            this.btnincluir.UseVisualStyleBackColor = true;
            this.btnincluir.Click += new System.EventHandler(this.btnincluir_Click);
            // 
            // btnremover
            // 
            this.btnremover.Location = new System.Drawing.Point(372, 111);
            this.btnremover.Name = "btnremover";
            this.btnremover.Size = new System.Drawing.Size(75, 23);
            this.btnremover.TabIndex = 57;
            this.btnremover.Text = "Remover";
            this.btnremover.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.gruposBindingSource, "Codigo", true));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(434, 21);
            this.comboBox1.TabIndex = 58;
            // 
            // gruposBindingSource
            // 
            this.gruposBindingSource.DataSource = typeof(ControleObras.Entidades.Grupos);
            // 
            // gruposBindingSource1
            // 
            this.gruposBindingSource1.DataSource = typeof(ControleObras.Entidades.Grupos);
            // 
            // frmCadastroMemorialDescritivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 521);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnremover);
            this.Controls.Add(this.btnincluir);
            this.Controls.Add(this.lista);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtDescricaoGrupo);
            this.Controls.Add(this.edtcodgrupo);
            this.Name = "frmCadastroMemorialDescritivo";
            this.Text = "CadastroMemorialDescritivo";
            this.Load += new System.EventHandler(this.frmCadastroMemorialDescritivo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gruposBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gruposBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox edtDescricaoGrupo;
        private System.Windows.Forms.TextBox edtcodgrupo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TreeView lista;
        private System.Windows.Forms.Button btnincluir;
        private System.Windows.Forms.Button btnremover;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource gruposBindingSource;
        private System.Windows.Forms.BindingSource gruposBindingSource1;
    }
}