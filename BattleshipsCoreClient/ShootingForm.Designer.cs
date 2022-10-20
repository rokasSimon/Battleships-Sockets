namespace BattleshipsCoreClient
{
    partial class ShootingForm
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
            this.TileGrid = new System.Windows.Forms.TableLayoutPanel();
            this.TurnLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TileGrid
            // 
            this.TileGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TileGrid.ColumnCount = 2;
            this.TileGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TileGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TileGrid.Location = new System.Drawing.Point(12, 36);
            this.TileGrid.Name = "TileGrid";
            this.TileGrid.RowCount = 2;
            this.TileGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TileGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TileGrid.Size = new System.Drawing.Size(684, 662);
            this.TileGrid.TabIndex = 0;
            // 
            // TurnLabel
            // 
            this.TurnLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TurnLabel.AutoSize = true;
            this.TurnLabel.Location = new System.Drawing.Point(148, 9);
            this.TurnLabel.Name = "TurnLabel";
            this.TurnLabel.Size = new System.Drawing.Size(58, 15);
            this.TurnLabel.TabIndex = 1;
            this.TurnLabel.Text = "Your Turn";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(196, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "Single Tile";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SetSingleTileShootingStrategy);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 30);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 30);
            this.button2.TabIndex = 3;
            this.button2.Text = "Area";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SetAreaShootingStrategy);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 60);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(196, 30);
            this.button3.TabIndex = 4;
            this.button3.Text = "Horizontal";
            this.button3.Click += new System.EventHandler(this.SetHorizontalShootingStrategy);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(0, 90);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(196, 30);
            this.button4.TabIndex = 5;
            this.button4.Text = "Vertical";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.SetVerticalShootingStrategy);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Location = new System.Drawing.Point(720, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(198, 297);
            this.panel1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "label2";
            // 
            // ShootingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 637);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TurnLabel);
            this.Controls.Add(this.TileGrid);
            this.Name = "ShootingForm";
            this.Text = "ShootingForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel TileGrid;
        private Label TurnLabel;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Label label1;
        private Panel panel1;
        private Label label2;
    }
}