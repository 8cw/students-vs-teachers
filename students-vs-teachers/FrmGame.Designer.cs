
namespace Students_vs_teachers
{
    partial class FrmGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGame));
            this.pnlGame = new System.Windows.Forms.Panel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnToggleState = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.pnlTowers = new System.Windows.Forms.Panel();
            this.lblMoney = new System.Windows.Forms.Label();
            this.lblLives = new System.Windows.Forms.Label();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGame
            // 
            resources.ApplyResources(this.pnlGame, "pnlGame");
            this.pnlGame.Name = "pnlGame";
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.lblLives);
            this.pnlMenu.Controls.Add(this.lblMoney);
            this.pnlMenu.Controls.Add(this.pnlTowers);
            this.pnlMenu.Controls.Add(this.btnHelp);
            this.pnlMenu.Controls.Add(this.btnSettings);
            this.pnlMenu.Controls.Add(this.btnToggleState);
            resources.ApplyResources(this.pnlMenu, "pnlMenu");
            this.pnlMenu.Name = "pnlMenu";
            // 
            // btnToggleState
            // 
            resources.ApplyResources(this.btnToggleState, "btnToggleState");
            this.btnToggleState.Name = "btnToggleState";
            this.btnToggleState.TabStop = false;
            this.btnToggleState.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            resources.ApplyResources(this.btnSettings, "btnSettings");
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.TabStop = false;
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.TabStop = false;
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // pnlTowers
            // 
            resources.ApplyResources(this.pnlTowers, "pnlTowers");
            this.pnlTowers.Name = "pnlTowers";
            // 
            // lblMoney
            // 
            resources.ApplyResources(this.lblMoney, "lblMoney");
            this.lblMoney.Name = "lblMoney";
            // 
            // lblLives
            // 
            resources.ApplyResources(this.lblLives, "lblLives");
            this.lblLives.Name = "lblLives";
            // 
            // FrmGame
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.pnlGame);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmGame";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGame;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnToggleState;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Panel pnlTowers;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Label lblLives;
    }
}
