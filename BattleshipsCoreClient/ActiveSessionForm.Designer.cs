namespace BattleshipsCoreClient
{
    partial class ActiveSessionForm
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
            this.LeaveButton = new System.Windows.Forms.Button();
            this.RefreshSessionButton = new System.Windows.Forms.Button();
            this.StartGameButton = new System.Windows.Forms.Button();
            this.PlayerListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // LeaveButton
            // 
            this.LeaveButton.Location = new System.Drawing.Point(218, 12);
            this.LeaveButton.Name = "LeaveButton";
            this.LeaveButton.Size = new System.Drawing.Size(175, 27);
            this.LeaveButton.TabIndex = 1;
            this.LeaveButton.Text = "Leave Session";
            this.LeaveButton.UseVisualStyleBackColor = true;
            this.LeaveButton.Click += new System.EventHandler(this.LeaveButton_Click);
            // 
            // RefreshSessionButton
            // 
            this.RefreshSessionButton.Location = new System.Drawing.Point(218, 45);
            this.RefreshSessionButton.Name = "RefreshSessionButton";
            this.RefreshSessionButton.Size = new System.Drawing.Size(175, 27);
            this.RefreshSessionButton.TabIndex = 2;
            this.RefreshSessionButton.Text = "Refresh";
            this.RefreshSessionButton.UseVisualStyleBackColor = true;
            this.RefreshSessionButton.Click += new System.EventHandler(this.RefreshSessionButton_Click);
            // 
            // StartGameButton
            // 
            this.StartGameButton.Location = new System.Drawing.Point(218, 78);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(175, 27);
            this.StartGameButton.TabIndex = 3;
            this.StartGameButton.Text = "Start Game";
            this.StartGameButton.UseVisualStyleBackColor = true;
            this.StartGameButton.Click += new System.EventHandler(this.StartGameButton_Click);
            // 
            // PlayerListBox
            // 
            this.PlayerListBox.FormattingEnabled = true;
            this.PlayerListBox.ItemHeight = 15;
            this.PlayerListBox.Location = new System.Drawing.Point(12, 12);
            this.PlayerListBox.Name = "PlayerListBox";
            this.PlayerListBox.Size = new System.Drawing.Size(200, 409);
            this.PlayerListBox.TabIndex = 4;
            // 
            // ActiveSessionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 431);
            this.Controls.Add(this.PlayerListBox);
            this.Controls.Add(this.StartGameButton);
            this.Controls.Add(this.RefreshSessionButton);
            this.Controls.Add(this.LeaveButton);
            this.Name = "ActiveSessionForm";
            this.Text = "ActiveSessionForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Button LeaveButton;
        private Button RefreshSessionButton;
        private Button StartGameButton;
        private ListBox PlayerListBox;
    }
}