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
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.operatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AndToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.XnorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FunctionMenuStrip = new System.Windows.Forms.MenuStrip();
            this.sTOPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rUNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sLOWRUNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sAVEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lOADToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rESETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.GateToolStrip = new System.Windows.Forms.ToolStripLabel();
            this.MouseEvent = new System.Windows.Forms.ToolStripLabel();
            this.PaintHandlerTool = new System.Windows.Forms.ToolStripLabel();
            this.MouseCoordinates = new System.Windows.Forms.ToolStripLabel();
            this.DrawingObject = new System.Windows.Forms.ToolStripLabel();
            this.MousePointing = new System.Windows.Forms.ToolStripLabel();
            this.SegmentsTool = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.FunctionMenuStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picCanvas
            // 
            this.picCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picCanvas.BackColor = System.Drawing.Color.White;
            this.picCanvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picCanvas.Location = new System.Drawing.Point(12, 27);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(720, 506);
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            this.picCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.picCanvas_Paint);
            this.picCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseDown);
            this.picCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseMove_NotDown);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operatorsToolStripMenuItem,
            this.InputToolStripMenuItem,
            this.AndToolStripMenuItem1,
            this.OrToolStripMenuItem,
            this.NotToolStripMenuItem,
            this.NorToolStripMenuItem,
            this.XorToolStripMenuItem1,
            this.XnorToolStripMenuItem,
            this.NandToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuStrip.Size = new System.Drawing.Size(745, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // operatorsToolStripMenuItem
            // 
            this.operatorsToolStripMenuItem.Enabled = false;
            this.operatorsToolStripMenuItem.Name = "operatorsToolStripMenuItem";
            this.operatorsToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
            this.operatorsToolStripMenuItem.Text = "BUILDING BLOCKS:";
            // 
            // InputToolStripMenuItem
            // 
            this.InputToolStripMenuItem.BackColor = System.Drawing.Color.Silver;
            this.InputToolStripMenuItem.Name = "InputToolStripMenuItem";
            this.InputToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.InputToolStripMenuItem.Text = "INPUT";
            this.InputToolStripMenuItem.Click += new System.EventHandler(this.onClick);
            // 
            // AndToolStripMenuItem1
            // 
            this.AndToolStripMenuItem1.BackColor = System.Drawing.Color.Silver;
            this.AndToolStripMenuItem1.Name = "AndToolStripMenuItem1";
            this.AndToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.AndToolStripMenuItem1.Text = "AND";
            this.AndToolStripMenuItem1.Click += new System.EventHandler(this.onClick);
            // 
            // OrToolStripMenuItem
            // 
            this.OrToolStripMenuItem.BackColor = System.Drawing.Color.Silver;
            this.OrToolStripMenuItem.Name = "OrToolStripMenuItem";
            this.OrToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.OrToolStripMenuItem.Text = "OR";
            this.OrToolStripMenuItem.Click += new System.EventHandler(this.onClick);
            // 
            // NotToolStripMenuItem
            // 
            this.NotToolStripMenuItem.BackColor = System.Drawing.Color.Silver;
            this.NotToolStripMenuItem.Name = "NotToolStripMenuItem";
            this.NotToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.NotToolStripMenuItem.Text = "NOT";
            this.NotToolStripMenuItem.Click += new System.EventHandler(this.onClick);
            // 
            // NorToolStripMenuItem
            // 
            this.NorToolStripMenuItem.BackColor = System.Drawing.Color.Silver;
            this.NorToolStripMenuItem.Name = "NorToolStripMenuItem";
            this.NorToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.NorToolStripMenuItem.Text = "NOR";
            this.NorToolStripMenuItem.Click += new System.EventHandler(this.onClick);
            // 
            // XorToolStripMenuItem1
            // 
            this.XorToolStripMenuItem1.BackColor = System.Drawing.Color.Silver;
            this.XorToolStripMenuItem1.Name = "XorToolStripMenuItem1";
            this.XorToolStripMenuItem1.Size = new System.Drawing.Size(42, 20);
            this.XorToolStripMenuItem1.Text = "XOR";
            this.XorToolStripMenuItem1.Click += new System.EventHandler(this.onClick);
            // 
            // XnorToolStripMenuItem
            // 
            this.XnorToolStripMenuItem.BackColor = System.Drawing.Color.Silver;
            this.XnorToolStripMenuItem.Name = "XnorToolStripMenuItem";
            this.XnorToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.XnorToolStripMenuItem.Text = "XNOR";
            this.XnorToolStripMenuItem.Click += new System.EventHandler(this.onClick);
            // 
            // NandToolStripMenuItem
            // 
            this.NandToolStripMenuItem.BackColor = System.Drawing.Color.Silver;
            this.NandToolStripMenuItem.Name = "NandToolStripMenuItem";
            this.NandToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.NandToolStripMenuItem.Text = "NAND";
            this.NandToolStripMenuItem.Click += new System.EventHandler(this.onClick);
            // 
            // FunctionMenuStrip
            // 
            this.FunctionMenuStrip.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.FunctionMenuStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.FunctionMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sTOPToolStripMenuItem,
            this.rUNToolStripMenuItem,
            this.sLOWRUNToolStripMenuItem,
            this.sAVEToolStripMenuItem,
            this.lOADToolStripMenuItem,
            this.rESETToolStripMenuItem});
            this.FunctionMenuStrip.Location = new System.Drawing.Point(745, 0);
            this.FunctionMenuStrip.Name = "FunctionMenuStrip";
            this.FunctionMenuStrip.Size = new System.Drawing.Size(84, 571);
            this.FunctionMenuStrip.TabIndex = 1;
            this.FunctionMenuStrip.Text = "FunctionMenuStrip";
            // 
            // sTOPToolStripMenuItem
            // 
            this.sTOPToolStripMenuItem.Name = "sTOPToolStripMenuItem";
            this.sTOPToolStripMenuItem.Size = new System.Drawing.Size(71, 19);
            this.sTOPToolStripMenuItem.Text = "STOP";
            // 
            // rUNToolStripMenuItem
            // 
            this.rUNToolStripMenuItem.Name = "rUNToolStripMenuItem";
            this.rUNToolStripMenuItem.Size = new System.Drawing.Size(71, 19);
            this.rUNToolStripMenuItem.Text = "RUN";
            // 
            // sLOWRUNToolStripMenuItem
            // 
            this.sLOWRUNToolStripMenuItem.Name = "sLOWRUNToolStripMenuItem";
            this.sLOWRUNToolStripMenuItem.Size = new System.Drawing.Size(71, 19);
            this.sLOWRUNToolStripMenuItem.Text = "SLOW RUN";
            // 
            // sAVEToolStripMenuItem
            // 
            this.sAVEToolStripMenuItem.Name = "sAVEToolStripMenuItem";
            this.sAVEToolStripMenuItem.Size = new System.Drawing.Size(71, 19);
            this.sAVEToolStripMenuItem.Text = "SAVE";
            // 
            // lOADToolStripMenuItem
            // 
            this.lOADToolStripMenuItem.Name = "lOADToolStripMenuItem";
            this.lOADToolStripMenuItem.Size = new System.Drawing.Size(71, 19);
            this.lOADToolStripMenuItem.Text = "LOAD";
            // 
            // rESETToolStripMenuItem
            // 
            this.rESETToolStripMenuItem.Name = "rESETToolStripMenuItem";
            this.rESETToolStripMenuItem.Size = new System.Drawing.Size(71, 19);
            this.rESETToolStripMenuItem.Text = "RESET";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GateToolStrip,
            this.MouseEvent,
            this.PaintHandlerTool,
            this.MouseCoordinates,
            this.DrawingObject,
            this.MousePointing,
            this.SegmentsTool});
            this.toolStrip1.Location = new System.Drawing.Point(0, 546);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(745, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // GateToolStrip
            // 
            this.GateToolStrip.Name = "GateToolStrip";
            this.GateToolStrip.Size = new System.Drawing.Size(78, 22);
            this.GateToolStrip.Text = "GateToolStrip";
            // 
            // MouseEvent
            // 
            this.MouseEvent.Name = "MouseEvent";
            this.MouseEvent.Size = new System.Drawing.Size(78, 22);
            this.MouseEvent.Text = "MouseEvent: ";
            // 
            // PaintHandlerTool
            // 
            this.PaintHandlerTool.Name = "PaintHandlerTool";
            this.PaintHandlerTool.Size = new System.Drawing.Size(110, 22);
            this.PaintHandlerTool.Text = "PaintHandler Calls: ";
            // 
            // MouseCoordinates
            // 
            this.MouseCoordinates.Name = "MouseCoordinates";
            this.MouseCoordinates.Size = new System.Drawing.Size(110, 22);
            this.MouseCoordinates.Text = "Mouse Coord: (X,Y)";
            // 
            // DrawingObject
            // 
            this.DrawingObject.Name = "DrawingObject";
            this.DrawingObject.Size = new System.Drawing.Size(57, 22);
            this.DrawingObject.Text = "Drawing: ";
            // 
            // MousePointing
            // 
            this.MousePointing.Name = "MousePointing";
            this.MousePointing.Size = new System.Drawing.Size(77, 22);
            this.MousePointing.Text = "MousePoint: ";
            // 
            // SegmentsTool
            // 
            this.SegmentsTool.Name = "SegmentsTool";
            this.SegmentsTool.Size = new System.Drawing.Size(126, 22);
            this.SegmentsTool.Text = "Number of Segments: ";
            // 
            // VCB_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(829, 571);
            this.Controls.Add(this.picCanvas);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.FunctionMenuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "VCB_Form";
            this.Text = "Visual Circuit Builder";
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.FunctionMenuStrip.ResumeLayout(false);
            this.FunctionMenuStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem operatorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AndToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem OrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem XorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem XnorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NandToolStripMenuItem;
        private System.Windows.Forms.MenuStrip FunctionMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem sTOPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rUNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sLOWRUNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sAVEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lOADToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rESETToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel GateToolStrip;
        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.ToolStripLabel MouseEvent;
        private System.Windows.Forms.ToolStripLabel PaintHandlerTool;
        private System.Windows.Forms.ToolStripLabel MouseCoordinates;
        private System.Windows.Forms.ToolStripLabel DrawingObject;
        private System.Windows.Forms.ToolStripLabel MousePointing;
        private System.Windows.Forms.ToolStripLabel SegmentsTool;
    }
}

