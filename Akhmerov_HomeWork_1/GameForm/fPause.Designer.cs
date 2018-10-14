namespace Akhmerov_HomeWork_1.GameForm
{
    partial class fPause
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
            this.lblLabel = new System.Windows.Forms.Label();
            this.btnReturnToGame = new System.Windows.Forms.Button();
            this.btnExitToMainMenu = new System.Windows.Forms.Button();
            this.lblPointsLabel = new System.Windows.Forms.Label();
            this.lblGamePoints = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Font = new System.Drawing.Font("Comic Sans MS", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLabel.Location = new System.Drawing.Point(61, 104);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(203, 67);
            this.lblLabel.TabIndex = 0;
            this.lblLabel.Text = "ПАУЗА";
            // 
            // btnReturnToGame
            // 
            this.btnReturnToGame.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReturnToGame.Location = new System.Drawing.Point(62, 174);
            this.btnReturnToGame.Name = "btnReturnToGame";
            this.btnReturnToGame.Size = new System.Drawing.Size(254, 106);
            this.btnReturnToGame.TabIndex = 1;
            this.btnReturnToGame.Text = "Вернуться\r\nв игру";
            this.btnReturnToGame.UseVisualStyleBackColor = true;
            this.btnReturnToGame.Click += new System.EventHandler(this.btnReturnToGame_Click);
            // 
            // btnExitToMainMenu
            // 
            this.btnExitToMainMenu.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExitToMainMenu.Location = new System.Drawing.Point(62, 308);
            this.btnExitToMainMenu.Name = "btnExitToMainMenu";
            this.btnExitToMainMenu.Size = new System.Drawing.Size(254, 106);
            this.btnExitToMainMenu.TabIndex = 2;
            this.btnExitToMainMenu.Text = "Вернуться в\r\nГлавное Меню";
            this.btnExitToMainMenu.UseVisualStyleBackColor = true;
            this.btnExitToMainMenu.Click += new System.EventHandler(this.btnExitToMainMenu_Click);
            // 
            // lblPointsLabel
            // 
            this.lblPointsLabel.AutoSize = true;
            this.lblPointsLabel.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPointsLabel.Location = new System.Drawing.Point(93, 9);
            this.lblPointsLabel.Name = "lblPointsLabel";
            this.lblPointsLabel.Size = new System.Drawing.Size(171, 38);
            this.lblPointsLabel.TabIndex = 3;
            this.lblPointsLabel.Text = "Ваши очки:";
            // 
            // lblGamePoints
            // 
            this.lblGamePoints.AutoSize = true;
            this.lblGamePoints.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblGamePoints.Location = new System.Drawing.Point(12, 61);
            this.lblGamePoints.Name = "lblGamePoints";
            this.lblGamePoints.Size = new System.Drawing.Size(26, 30);
            this.lblGamePoints.TabIndex = 4;
            this.lblGamePoints.Text = "0";
            // 
            // fPause
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 450);
            this.ControlBox = false;
            this.Controls.Add(this.lblGamePoints);
            this.Controls.Add(this.lblPointsLabel);
            this.Controls.Add(this.btnExitToMainMenu);
            this.Controls.Add(this.btnReturnToGame);
            this.Controls.Add(this.lblLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fPause";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fPause";
            this.Load += new System.EventHandler(this.fPause_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnExitToMainMenu;
        public System.Windows.Forms.Label lblLabel;
        public System.Windows.Forms.Button btnReturnToGame;
        private System.Windows.Forms.Label lblPointsLabel;
        public System.Windows.Forms.Label lblGamePoints;
    }
}