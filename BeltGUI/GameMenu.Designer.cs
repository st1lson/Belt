namespace BeltGUI
{
    partial class GameMenu
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
            this.StartButton = new System.Windows.Forms.Button();
            this.fieldCardsPlace = new System.Windows.Forms.PictureBox();
            this.deckPlace = new System.Windows.Forms.PictureBox();
            this.botStashPlace = new System.Windows.Forms.PictureBox();
            this.playerCardsPlace = new System.Windows.Forms.PictureBox();
            this.botCardsPlace = new System.Windows.Forms.PictureBox();
            this.playerStashPlace = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.fieldCardsPlace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deckPlace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.botStashPlace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerCardsPlace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.botCardsPlace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerStashPlace)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StartButton.Location = new System.Drawing.Point(580, 540);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(200, 75);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // fieldCardsPlace
            // 
            this.fieldCardsPlace.BackColor = System.Drawing.Color.Transparent;
            this.fieldCardsPlace.Location = new System.Drawing.Point(240, 250);
            this.fieldCardsPlace.Name = "fieldCardsPlace";
            this.fieldCardsPlace.Size = new System.Drawing.Size(105, 155);
            this.fieldCardsPlace.TabIndex = 2;
            this.fieldCardsPlace.TabStop = false;
            this.fieldCardsPlace.Visible = false;
            // 
            // deckPlace
            // 
            this.deckPlace.BackColor = System.Drawing.Color.Transparent;
            this.deckPlace.BackgroundImage = global::BeltGUI.Properties.Resources.back;
            this.deckPlace.Location = new System.Drawing.Point(20, 250);
            this.deckPlace.Name = "deckPlace";
            this.deckPlace.Size = new System.Drawing.Size(105, 155);
            this.deckPlace.TabIndex = 3;
            this.deckPlace.TabStop = false;
            this.deckPlace.Click += new System.EventHandler(this.DeckClick);
            // 
            // botStashPlace
            // 
            this.botStashPlace.BackColor = System.Drawing.Color.Transparent;
            this.botStashPlace.BackgroundImage = global::BeltGUI.Properties.Resources.back;
            this.botStashPlace.Location = new System.Drawing.Point(1120, 20);
            this.botStashPlace.Name = "botStashPlace";
            this.botStashPlace.Size = new System.Drawing.Size(105, 155);
            this.botStashPlace.TabIndex = 4;
            this.botStashPlace.TabStop = false;
            this.botStashPlace.Visible = false;
            // 
            // playerCardsPlace
            // 
            this.playerCardsPlace.BackColor = System.Drawing.Color.Transparent;
            this.playerCardsPlace.Location = new System.Drawing.Point(140, 500);
            this.playerCardsPlace.Name = "playerCardsPlace";
            this.playerCardsPlace.Size = new System.Drawing.Size(105, 155);
            this.playerCardsPlace.TabIndex = 5;
            this.playerCardsPlace.TabStop = false;
            this.playerCardsPlace.Visible = false;
            // 
            // botCardsPlace
            // 
            this.botCardsPlace.BackColor = System.Drawing.Color.Transparent;
            this.botCardsPlace.Location = new System.Drawing.Point(140, 20);
            this.botCardsPlace.Name = "botCardsPlace";
            this.botCardsPlace.Size = new System.Drawing.Size(105, 155);
            this.botCardsPlace.TabIndex = 6;
            this.botCardsPlace.TabStop = false;
            this.botCardsPlace.Visible = false;
            // 
            // playerStashPlace
            // 
            this.playerStashPlace.BackColor = System.Drawing.Color.Transparent;
            this.playerStashPlace.BackgroundImage = global::BeltGUI.Properties.Resources.back;
            this.playerStashPlace.Location = new System.Drawing.Point(1120, 500);
            this.playerStashPlace.Name = "playerStashPlace";
            this.playerStashPlace.Size = new System.Drawing.Size(105, 155);
            this.playerStashPlace.TabIndex = 7;
            this.playerStashPlace.TabStop = false;
            this.playerStashPlace.Visible = false;
            // 
            // GameMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.playerStashPlace);
            this.Controls.Add(this.botCardsPlace);
            this.Controls.Add(this.playerCardsPlace);
            this.Controls.Add(this.botStashPlace);
            this.Controls.Add(this.deckPlace);
            this.Controls.Add(this.fieldCardsPlace);
            this.Controls.Add(this.StartButton);
            this.Name = "GameMenu";
            this.Text = "GameMenu";
            ((System.ComponentModel.ISupportInitialize)(this.fieldCardsPlace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deckPlace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.botStashPlace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerCardsPlace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.botCardsPlace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerStashPlace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.PictureBox fieldCardsPlace;
        private System.Windows.Forms.PictureBox deckPlace;
        private System.Windows.Forms.PictureBox botStashPlace;
        private System.Windows.Forms.PictureBox playerCardsPlace;
        private System.Windows.Forms.PictureBox botCardsPlace;
        private System.Windows.Forms.PictureBox playerStashPlace;
    }
}