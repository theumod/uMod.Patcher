namespace Oxide.Patcher
{
    partial class NewProjectForm
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
            this.settingsgroup = new System.Windows.Forms.GroupBox();
            this.tablepanel = new System.Windows.Forms.TableLayoutPanel();
            this.selectfilenamebutton = new System.Windows.Forms.Button();
            this.filenamelabel = new System.Windows.Forms.Label();
            this.filenametextbox = new System.Windows.Forms.TextBox();
            this.directorylabel = new System.Windows.Forms.Label();
            this.namelabel = new System.Windows.Forms.Label();
            this.nametextbox = new System.Windows.Forms.TextBox();
            this.selectdirectorybutton = new System.Windows.Forms.Button();
            this.directorytextbox = new System.Windows.Forms.TextBox();
            this.createbutton = new System.Windows.Forms.Button();
            this.cancelbutton = new System.Windows.Forms.Button();
            this.selectfilenamedialog = new System.Windows.Forms.SaveFileDialog();
            this.selectdirectorydialog = new System.Windows.Forms.FolderBrowserDialog();
            this.settingsgroup.SuspendLayout();
            this.tablepanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsgroup
            // 
            this.settingsgroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsgroup.Controls.Add(this.tablepanel);
            this.settingsgroup.Location = new System.Drawing.Point(12, 12);
            this.settingsgroup.Name = "settingsgroup";
            this.settingsgroup.Size = new System.Drawing.Size(459, 94);
            this.settingsgroup.TabIndex = 0;
            this.settingsgroup.TabStop = false;
            this.settingsgroup.Text = "Project Settings";
            // 
            // tablepanel
            // 
            this.tablepanel.ColumnCount = 3;
            this.tablepanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tablepanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablepanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tablepanel.Controls.Add(this.selectfilenamebutton, 2, 2);
            this.tablepanel.Controls.Add(this.filenamelabel, 0, 2);
            this.tablepanel.Controls.Add(this.filenametextbox, 0, 2);
            this.tablepanel.Controls.Add(this.directorylabel, 0, 1);
            this.tablepanel.Controls.Add(this.namelabel, 0, 0);
            this.tablepanel.Controls.Add(this.nametextbox, 1, 0);
            this.tablepanel.Controls.Add(this.selectdirectorybutton, 2, 1);
            this.tablepanel.Controls.Add(this.directorytextbox, 1, 1);
            this.tablepanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablepanel.Location = new System.Drawing.Point(3, 16);
            this.tablepanel.Name = "tablepanel";
            this.tablepanel.RowCount = 3;
            this.tablepanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tablepanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tablepanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tablepanel.Size = new System.Drawing.Size(453, 75);
            this.tablepanel.TabIndex = 0;
            // 
            // selectfilenamebutton
            // 
            this.selectfilenamebutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectfilenamebutton.Location = new System.Drawing.Point(423, 53);
            this.selectfilenamebutton.Name = "selectfilenamebutton";
            this.selectfilenamebutton.Size = new System.Drawing.Size(27, 19);
            this.selectfilenamebutton.TabIndex = 8;
            this.selectfilenamebutton.Text = "...";
            this.selectfilenamebutton.UseVisualStyleBackColor = true;
            this.selectfilenamebutton.Click += new System.EventHandler(this.selectfilenamebutton_Click);
            // 
            // filenamelabel
            // 
            this.filenamelabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filenamelabel.Location = new System.Drawing.Point(3, 50);
            this.filenamelabel.Name = "filenamelabel";
            this.filenamelabel.Size = new System.Drawing.Size(94, 25);
            this.filenamelabel.TabIndex = 6;
            this.filenamelabel.Text = "Filename:";
            this.filenamelabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // filenametextbox
            // 
            this.filenametextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filenametextbox.Location = new System.Drawing.Point(103, 53);
            this.filenametextbox.Name = "filenametextbox";
            this.filenametextbox.Size = new System.Drawing.Size(314, 20);
            this.filenametextbox.TabIndex = 7;
            this.filenametextbox.TextChanged += new System.EventHandler(this.filenametextbox_TextChanged);
            // 
            // directorylabel
            // 
            this.directorylabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directorylabel.Location = new System.Drawing.Point(3, 25);
            this.directorylabel.Name = "directorylabel";
            this.directorylabel.Size = new System.Drawing.Size(94, 25);
            this.directorylabel.TabIndex = 2;
            this.directorylabel.Text = "Target Directory:";
            this.directorylabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // namelabel
            // 
            this.namelabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.namelabel.Location = new System.Drawing.Point(3, 0);
            this.namelabel.Name = "namelabel";
            this.namelabel.Size = new System.Drawing.Size(94, 25);
            this.namelabel.TabIndex = 0;
            this.namelabel.Text = "Name:";
            this.namelabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nametextbox
            // 
            this.tablepanel.SetColumnSpan(this.nametextbox, 2);
            this.nametextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nametextbox.Location = new System.Drawing.Point(103, 3);
            this.nametextbox.MaxLength = 100;
            this.nametextbox.Name = "nametextbox";
            this.nametextbox.Size = new System.Drawing.Size(347, 20);
            this.nametextbox.TabIndex = 3;
            this.nametextbox.Text = "Untitled Oxide Project";
            this.nametextbox.TextChanged += new System.EventHandler(this.nametextbox_TextChanged);
            // 
            // selectdirectorybutton
            // 
            this.selectdirectorybutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectdirectorybutton.Location = new System.Drawing.Point(423, 28);
            this.selectdirectorybutton.Name = "selectdirectorybutton";
            this.selectdirectorybutton.Size = new System.Drawing.Size(27, 19);
            this.selectdirectorybutton.TabIndex = 4;
            this.selectdirectorybutton.Text = "...";
            this.selectdirectorybutton.UseVisualStyleBackColor = true;
            this.selectdirectorybutton.Click += new System.EventHandler(this.selectdirectorybutton_Click);
            // 
            // directorytextbox
            // 
            this.directorytextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directorytextbox.Location = new System.Drawing.Point(103, 28);
            this.directorytextbox.Name = "directorytextbox";
            this.directorytextbox.Size = new System.Drawing.Size(314, 20);
            this.directorytextbox.TabIndex = 5;
            this.directorytextbox.TextChanged += new System.EventHandler(this.directorytextbox_TextChanged);
            // 
            // createbutton
            // 
            this.createbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createbutton.Location = new System.Drawing.Point(12, 113);
            this.createbutton.Name = "createbutton";
            this.createbutton.Size = new System.Drawing.Size(219, 35);
            this.createbutton.TabIndex = 1;
            this.createbutton.Text = "Create";
            this.createbutton.UseVisualStyleBackColor = true;
            this.createbutton.Click += new System.EventHandler(this.createbutton_Click);
            // 
            // cancelbutton
            // 
            this.cancelbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelbutton.Location = new System.Drawing.Point(244, 113);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(227, 35);
            this.cancelbutton.TabIndex = 2;
            this.cancelbutton.Text = "Cancel";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
            // 
            // selectfilenamedialog
            // 
            this.selectfilenamedialog.DefaultExt = "opj";
            this.selectfilenamedialog.Filter = "Oxide project|*.opj";
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbutton;
            this.ClientSize = new System.Drawing.Size(483, 160);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.createbutton);
            this.Controls.Add(this.settingsgroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectForm";
            this.Text = "Create Project";
            this.Load += new System.EventHandler(this.NewProjectForm_Load);
            this.settingsgroup.ResumeLayout(false);
            this.tablepanel.ResumeLayout(false);
            this.tablepanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox settingsgroup;
        private System.Windows.Forms.Button createbutton;
        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.TableLayoutPanel tablepanel;
        private System.Windows.Forms.Label namelabel;
        private System.Windows.Forms.Label directorylabel;
        private System.Windows.Forms.TextBox nametextbox;
        private System.Windows.Forms.Button selectdirectorybutton;
        private System.Windows.Forms.TextBox directorytextbox;
        private System.Windows.Forms.Button selectfilenamebutton;
        private System.Windows.Forms.Label filenamelabel;
        private System.Windows.Forms.TextBox filenametextbox;
        private System.Windows.Forms.SaveFileDialog selectfilenamedialog;
        private System.Windows.Forms.FolderBrowserDialog selectdirectorydialog;

    }
}
