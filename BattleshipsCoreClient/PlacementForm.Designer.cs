﻿namespace BattleshipsCoreClient
{
    partial class PlacementForm
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
            this.LeaveButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.PlaceableObjectPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.RotateButton = new System.Windows.Forms.Button();
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
            this.TileGrid.Location = new System.Drawing.Point(12, 12);
            this.TileGrid.Name = "TileGrid";
            this.TileGrid.RowCount = 2;
            this.TileGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TileGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TileGrid.Size = new System.Drawing.Size(323, 449);
            this.TileGrid.TabIndex = 0;
            // 
            // LeaveButton
            // 
            this.LeaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LeaveButton.Location = new System.Drawing.Point(341, 432);
            this.LeaveButton.Name = "LeaveButton";
            this.LeaveButton.Size = new System.Drawing.Size(86, 29);
            this.LeaveButton.TabIndex = 1;
            this.LeaveButton.Text = "Leave Game";
            this.LeaveButton.UseVisualStyleBackColor = true;
            this.LeaveButton.Click += new System.EventHandler(this.LeaveButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayButton.Location = new System.Drawing.Point(433, 432);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(92, 29);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Start";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // PlaceableObjectPanel
            // 
            this.PlaceableObjectPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaceableObjectPanel.AutoScroll = true;
            this.PlaceableObjectPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.PlaceableObjectPanel.Location = new System.Drawing.Point(341, 12);
            this.PlaceableObjectPanel.Name = "PlaceableObjectPanel";
            this.PlaceableObjectPanel.Size = new System.Drawing.Size(184, 381);
            this.PlaceableObjectPanel.TabIndex = 3;
            // 
            // RotateButton
            // 
            this.RotateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RotateButton.Location = new System.Drawing.Point(341, 399);
            this.RotateButton.Name = "RotateButton";
            this.RotateButton.Size = new System.Drawing.Size(86, 27);
            this.RotateButton.TabIndex = 4;
            this.RotateButton.Text = "Rotate";
            this.RotateButton.UseVisualStyleBackColor = true;
            this.RotateButton.Click += new System.EventHandler(this.RotateButton_Click);
            // 
            // PlacementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 473);
            this.Controls.Add(this.RotateButton);
            this.Controls.Add(this.PlaceableObjectPanel);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.LeaveButton);
            this.Controls.Add(this.TileGrid);
            this.Name = "PlacementForm";
            this.Text = "PlacementForm";
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel TileGrid;
        private Button LeaveButton;
        private Button PlayButton;
        private FlowLayoutPanel PlaceableObjectPanel;
        private Button RotateButton;
    }
}