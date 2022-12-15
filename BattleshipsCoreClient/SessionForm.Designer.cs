﻿using BattleshipsCoreClient.TemplateMethod;

namespace BattleshipsCoreClient
{
    partial class SessionForm
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
            this.RefreshButton = new System.Windows.Forms.Button();
            this.SessionListGrid = new System.Windows.Forms.DataGridView();
            this.SessionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlayerCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateSessionButton = new System.Windows.Forms.Button();
            this.CreateSessionTextBox = new System.Windows.Forms.TextBox();
            this.CreateSessionLabel = new System.Windows.Forms.Label();
            this.DisconnectButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SessionListGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshButton.Location = new System.Drawing.Point(444, 14);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(113, 30);
            this.RefreshButton.TabIndex = 0;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // SessionListGrid
            // 
            this.SessionListGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionListGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SessionListGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.SessionListGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.SessionListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SessionListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SessionName,
            this.PlayerCount});
            this.SessionListGrid.Location = new System.Drawing.Point(12, 50);
            this.SessionListGrid.MultiSelect = false;
            this.SessionListGrid.Name = "SessionListGrid";
            this.SessionListGrid.RowTemplate.Height = 25;
            this.SessionListGrid.Size = new System.Drawing.Size(545, 384);
            this.SessionListGrid.TabIndex = 1;
            this.SessionListGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SessionRow_Click);
            // 
            // SessionName
            // 
            this.SessionName.HeaderText = "Session Name";
            this.SessionName.Name = "SessionName";
            // 
            // PlayerCount
            // 
            this.PlayerCount.HeaderText = "Players Inside";
            this.PlayerCount.Name = "PlayerCount";
            // 
            // CreateSessionButton
            // 
            this.CreateSessionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateSessionButton.Location = new System.Drawing.Point(439, 448);
            this.CreateSessionButton.Name = "CreateSessionButton";
            this.CreateSessionButton.Size = new System.Drawing.Size(118, 23);
            this.CreateSessionButton.TabIndex = 2;
            this.CreateSessionButton.Text = "Create Session";
            this.CreateSessionButton.UseVisualStyleBackColor = true;
            this.CreateSessionButton.Click += new System.EventHandler(this.CreateSessionButton_Click);
            // 
            // CreateSessionTextBox
            // 
            this.CreateSessionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateSessionTextBox.Location = new System.Drawing.Point(333, 448);
            this.CreateSessionTextBox.Name = "CreateSessionTextBox";
            this.CreateSessionTextBox.Size = new System.Drawing.Size(100, 23);
            this.CreateSessionTextBox.TabIndex = 3;
            // 
            // CreateSessionLabel
            // 
            this.CreateSessionLabel = this.template.TemplateMethod(CreateSessionLabel, new System.Drawing.Point(243, 452), 
            new System.Drawing.Size(84, 15), "CreateSessionLabel", "Session Name:", 4,
            ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))));           
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Location = new System.Drawing.Point(12, 12);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(88, 30);
            this.DisconnectButton.TabIndex = 5;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // SessionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 485);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.CreateSessionLabel);
            this.Controls.Add(this.CreateSessionTextBox);
            this.Controls.Add(this.CreateSessionButton);
            this.Controls.Add(this.SessionListGrid);
            this.Controls.Add(this.RefreshButton);
            this.Name = "SessionForm";
            this.Text = "SessionForm";
            ((System.ComponentModel.ISupportInitialize)(this.SessionListGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button RefreshButton;
        private DataGridView SessionListGrid;
        private DataGridViewTextBoxColumn SessionName;
        private DataGridViewTextBoxColumn PlayerCount;
        private Button CreateSessionButton;
        private TextBox CreateSessionTextBox;
        private Label CreateSessionLabel;
        private Button DisconnectButton;
        private LabelTemplate template = new AdvancedLabel();
    }
}