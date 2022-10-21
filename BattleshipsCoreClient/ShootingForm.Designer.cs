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
            this.components = new System.ComponentModel.Container();
            this.TileGrid = new System.Windows.Forms.TableLayoutPanel();
            this.TurnLabel = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.radioButtonShoot = new System.Windows.Forms.RadioButton();
            this.radioButtonDoubleShoot = new System.Windows.Forms.RadioButton();
            this.radioButtonBomb = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            this.TileGrid.Location = new System.Drawing.Point(14, 48);
            this.TileGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TileGrid.Name = "TileGrid";
            this.TileGrid.RowCount = 2;
            this.TileGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TileGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TileGrid.Size = new System.Drawing.Size(774, 883);
            this.TileGrid.TabIndex = 0;
            this.TileGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.TileGrid_Paint);
            // 
            // TurnLabel
            // 
            this.TurnLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TurnLabel.AutoSize = true;
            this.TurnLabel.Location = new System.Drawing.Point(479, 11);
            this.TurnLabel.Name = "TurnLabel";
            this.TurnLabel.Size = new System.Drawing.Size(71, 20);
            this.TurnLabel.TabIndex = 1;
            this.TurnLabel.Text = "Your Turn";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // radioButton1
            // 
            this.radioButtonShoot.AutoSize = true;
            this.radioButtonShoot.Location = new System.Drawing.Point(14, 9);
            this.radioButtonShoot.Name = "Shoot";
            this.radioButtonShoot.Size = new System.Drawing.Size(117, 24);
            this.radioButtonShoot.TabIndex = 2;
            this.radioButtonShoot.TabStop = true;
            this.radioButtonShoot.Text = "Shoot";
            this.radioButtonShoot.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButtonDoubleShoot.AutoSize = true;
            this.radioButtonDoubleShoot.Location = new System.Drawing.Point(137, 9);
            this.radioButtonDoubleShoot.Name = "Double Shoot";
            this.radioButtonDoubleShoot.Size = new System.Drawing.Size(117, 24);
            this.radioButtonDoubleShoot.TabIndex = 3;
            this.radioButtonDoubleShoot.TabStop = true;
            this.radioButtonDoubleShoot.Text = "Double Shoot";
            this.radioButtonDoubleShoot.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButtonBomb.AutoSize = true;
            this.radioButtonBomb.Location = new System.Drawing.Point(260, 11);
            this.radioButtonBomb.Name = "Bomb";
            this.radioButtonBomb.Size = new System.Drawing.Size(117, 24);
            this.radioButtonBomb.TabIndex = 4;
            this.radioButtonBomb.TabStop = true;
            this.radioButtonBomb.Text = "Bomb";
            this.radioButtonBomb.UseVisualStyleBackColor = true;
            // 
            // ShootingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 947);
            this.Controls.Add(this.radioButtonBomb);
            this.Controls.Add(this.radioButtonDoubleShoot);
            this.Controls.Add(this.radioButtonShoot);
            this.Controls.Add(this.TurnLabel);
            this.Controls.Add(this.TileGrid);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ShootingForm";
            this.Text = "ShootingForm";
            this.Load += new System.EventHandler(this.ShootingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel TileGrid;
        private Label TurnLabel;
        private Button ShotNuke;
        private ErrorProvider errorProvider1;
        private RadioButton radioButtonBomb;
        private RadioButton radioButtonDoubleShoot;
        private RadioButton radioButtonShoot;
    }
}