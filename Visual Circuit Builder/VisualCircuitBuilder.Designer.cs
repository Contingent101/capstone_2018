namespace Visual_Circuit_Builder
{
    partial class VCB_Form
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.operatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.truthConnectorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.aNDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nOTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nANDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nORToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nOTToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.xORToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tRUEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fALSEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operatorsToolStripMenuItem,
            this.truthConnectorsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuStrip.Size = new System.Drawing.Size(855, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // operatorsToolStripMenuItem
            // 
            this.operatorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aNDToolStripMenuItem,
            this.nOTToolStripMenuItem,
            this.nANDToolStripMenuItem,
            this.nORToolStripMenuItem,
            this.nOTToolStripMenuItem1,
            this.xORToolStripMenuItem});
            this.operatorsToolStripMenuItem.Name = "operatorsToolStripMenuItem";
            this.operatorsToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.operatorsToolStripMenuItem.Text = "Operators";
            // 
            // truthConnectorsToolStripMenuItem
            // 
            this.truthConnectorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tRUEToolStripMenuItem,
            this.fALSEToolStripMenuItem});
            this.truthConnectorsToolStripMenuItem.Name = "truthConnectorsToolStripMenuItem";
            this.truthConnectorsToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.truthConnectorsToolStripMenuItem.Text = "Truth Connectors ";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainer.Size = new System.Drawing.Size(855, 515);
            this.splitContainer.SplitterDistance = 144;
            this.splitContainer.TabIndex = 1;
            // 
            // aNDToolStripMenuItem
            // 
            this.aNDToolStripMenuItem.Name = "aNDToolStripMenuItem";
            this.aNDToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aNDToolStripMenuItem.Text = "AND";
            // 
            // nOTToolStripMenuItem
            // 
            this.nOTToolStripMenuItem.Name = "nOTToolStripMenuItem";
            this.nOTToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nOTToolStripMenuItem.Text = "NAND";
            // 
            // nANDToolStripMenuItem
            // 
            this.nANDToolStripMenuItem.Name = "nANDToolStripMenuItem";
            this.nANDToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nANDToolStripMenuItem.Text = "OR";
            // 
            // nORToolStripMenuItem
            // 
            this.nORToolStripMenuItem.Name = "nORToolStripMenuItem";
            this.nORToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nORToolStripMenuItem.Text = "NOR";
            // 
            // nOTToolStripMenuItem1
            // 
            this.nOTToolStripMenuItem1.Name = "nOTToolStripMenuItem1";
            this.nOTToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.nOTToolStripMenuItem1.Text = "NOT";
            // 
            // xORToolStripMenuItem
            // 
            this.xORToolStripMenuItem.Name = "xORToolStripMenuItem";
            this.xORToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.xORToolStripMenuItem.Text = "XOR";
            // 
            // tRUEToolStripMenuItem
            // 
            this.tRUEToolStripMenuItem.Name = "tRUEToolStripMenuItem";
            this.tRUEToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tRUEToolStripMenuItem.Text = "TRUE";
            // 
            // fALSEToolStripMenuItem
            // 
            this.fALSEToolStripMenuItem.Name = "fALSEToolStripMenuItem";
            this.fALSEToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fALSEToolStripMenuItem.Text = "FALSE";
            // 
            // VCB_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(855, 539);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "VCB_Form";
            this.Text = "Visual Circuit Builder";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem operatorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem truthConnectorsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripMenuItem aNDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nOTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nANDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nORToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nOTToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem xORToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tRUEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fALSEToolStripMenuItem;
    }
}

