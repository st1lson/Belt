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
            this.fieldCards = new System.Windows.Forms.PictureBox();
            this.deck = new System.Windows.Forms.PictureBox();
            this.pile = new System.Windows.Forms.PictureBox();
            this.playerCards = new System.Windows.Forms.PictureBox();
            this.botCards = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.fieldCards)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerCards)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.botCards)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StartButton.Location = new System.Drawing.Point(420, 540);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(200, 75);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // fieldCards
            // 
            this.fieldCards.BackColor = System.Drawing.Color.Transparent;
            this.fieldCards.Location = new System.Drawing.Point(333, 250);
            this.fieldCards.Name = "fieldCards";
            this.fieldCards.Size = new System.Drawing.Size(105, 155);
            this.fieldCards.TabIndex = 2;
            this.fieldCards.TabStop = false;
            this.fieldCards.Visible = false;
            // 
            // deck
            // 
            this.deck.BackColor = System.Drawing.Color.Transparent;
            this.deck.BackgroundImage = global::BeltGUI.Properties.Resources.back;
            this.deck.Location = new System.Drawing.Point(20, 250);
            this.deck.Name = "deck";
            this.deck.Size = new System.Drawing.Size(105, 155);
            this.deck.TabIndex = 3;
            this.deck.TabStop = false;
            this.deck.Click += new System.EventHandler(this.DeckClick);
            // 
            // pile
            // 
            this.pile.BackColor = System.Drawing.Color.Transparent;
            this.pile.BackgroundImage = global::BeltGUI.Properties.Resources.back;
            this.pile.Location = new System.Drawing.Point(920, 250);
            this.pile.Name = "pile";
            this.pile.Size = new System.Drawing.Size(105, 155);
            this.pile.TabIndex = 4;
            this.pile.TabStop = false;
            this.pile.Visible = false;
            // 
            // playerCards
            // 
            this.playerCards.BackColor = System.Drawing.Color.Transparent;
            this.playerCards.Location = new System.Drawing.Point(140, 500);
            this.playerCards.Name = "playerCards";
            this.playerCards.Size = new System.Drawing.Size(105, 155);
            this.playerCards.TabIndex = 5;
            this.playerCards.TabStop = false;
            this.playerCards.Visible = false;
            // 
            // botCards
            // 
            this.botCards.BackColor = System.Drawing.Color.Transparent;
            this.botCards.Location = new System.Drawing.Point(140, 20);
            this.botCards.Name = "botCards";
            this.botCards.Size = new System.Drawing.Size(105, 155);
            this.botCards.TabIndex = 6;
            this.botCards.TabStop = false;
            this.botCards.Visible = false;
            // 
            // GameMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.botCards);
            this.Controls.Add(this.playerCards);
            this.Controls.Add(this.pile);
            this.Controls.Add(this.deck);
            this.Controls.Add(this.fieldCards);
            this.Controls.Add(this.StartButton);
            this.Name = "GameMenu";
            this.Text = "GameMenu";
            ((System.ComponentModel.ISupportInitialize)(this.fieldCards)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerCards)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.botCards)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.PictureBox fieldCards;
        private System.Windows.Forms.PictureBox deck;
        private System.Windows.Forms.PictureBox pile;
        private System.Windows.Forms.PictureBox playerCards;
        private System.Windows.Forms.PictureBox botCards;
    }
}