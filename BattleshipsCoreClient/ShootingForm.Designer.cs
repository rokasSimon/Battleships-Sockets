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
            this.TileGrid.Location = new System.Drawing.Point(17, 60);
            this.TileGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TileGrid.Name = "TileGrid";
            this.TileGrid.RowCount = 2;
            this.TileGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TileGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TileGrid.Size = new System.Drawing.Size(900, 1103);
            this.TileGrid.TabIndex = 0;
            this.TileGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.TileGrid_Paint_1);
            // 
            // TurnLabel
            // 
            this.TurnLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TurnLabel.AutoSize = true;
            this.TurnLabel.Location = new System.Drawing.Point(124, 15);
            this.TurnLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TurnLabel.Name = "TurnLabel";
            this.TurnLabel.Size = new System.Drawing.Size(87, 25);
            this.TurnLabel.TabIndex = 1;
            this.TurnLabel.Text = "Your Turn";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1000, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Single Tile";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SetSingleTileShootingStrategy);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1000, 150);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 34);
            this.button2.TabIndex = 3;
            this.button2.Text = "Area";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SetAreaShootingStrategy);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1000, 200);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 34);
            this.button3.TabIndex = 4;
            this.button3.Text = "Horizontal";
            this.button3.Click += new System.EventHandler(this.SetHorizontalShootingStrategy);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1000, 250);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 34);
            this.button4.TabIndex = 5;
            this.button4.Text = "Vertical";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.SetVerticalShootingStrategy);
            // 
            // ShootingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 1183);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TurnLabel);
            this.Controls.Add(this.TileGrid);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ShootingForm";
            this.Text = "ShootingForm";
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
    }
}