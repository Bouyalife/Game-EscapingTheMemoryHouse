using GameV1.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace GameV1
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.playerHealth = new System.Windows.Forms.ProgressBar();
            this.playerBox = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.ConversationLabel = new System.Windows.Forms.Label();
            this.letterLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 455);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Health: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 455);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ammo:";
            // 
            // playerHealth
            // 
            this.playerHealth.Location = new System.Drawing.Point(221, 445);
            this.playerHealth.Name = "playerHealth";
            this.playerHealth.Size = new System.Drawing.Size(100, 23);
            this.playerHealth.TabIndex = 2;
            // 
            // playerBox
            // 
            this.playerBox.BackColor = System.Drawing.Color.Transparent;
            this.playerBox.Image = global::GameV1.Properties.Resources.walkRight1;
            this.playerBox.Location = new System.Drawing.Point(55, 50);
            this.playerBox.Name = "playerBox";
            this.playerBox.Size = new System.Drawing.Size(64, 64);
            this.playerBox.TabIndex = 3;
            this.playerBox.TabStop = false;
            this.playerBox.Tag = "player";
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 10;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimerEvent);
            // 
            // ConversationLabel
            // 
            this.ConversationLabel.AutoSize = true;
            this.ConversationLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConversationLabel.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConversationLabel.Location = new System.Drawing.Point(130, 391);
            this.ConversationLabel.Name = "ConversationLabel";
            this.ConversationLabel.Size = new System.Drawing.Size(0, 27);
            this.ConversationLabel.TabIndex = 4;
            // 
            // letterLabel
            // 
            this.letterLabel.AutoSize = true;
            this.letterLabel.BackColor = System.Drawing.Color.Transparent;
            this.letterLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.letterLabel.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letterLabel.Location = new System.Drawing.Point(268, 224);
            this.letterLabel.Name = "letterLabel";
            this.letterLabel.Size = new System.Drawing.Size(1373, 27);
            this.letterLabel.TabIndex = 5;
            this.letterLabel.Text = "We got your waifu pillow mr onana, bring me the virgin waifu misstress Okaniwa to" +
    " the temple unharmed and a trade will be made.";
            this.letterLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GameV1.Properties.Resources.map0;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.letterLabel);
            this.Controls.Add(this.ConversationLabel);
            this.Controls.Add(this.playerBox);
            this.Controls.Add(this.playerHealth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mouseClick);

            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar playerHealth;
        private System.Windows.Forms.PictureBox playerBox;
        private System.Windows.Forms.Timer gameTimer;
        private Label ConversationLabel;
        private Label letterLabel;
    }
}

