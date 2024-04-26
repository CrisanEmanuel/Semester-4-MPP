using System.ComponentModel;

namespace Client.Forms
{
    partial class RezervareForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.numeClientTextBox = new System.Windows.Forms.TextBox();
            this.numarTelefonTextBox = new System.Windows.Forms.TextBox();
            this.numarLocuriTextBox = new System.Windows.Forms.TextBox();
            this.rezervaButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numeObiectivLabel = new System.Windows.Forms.Label();
            this.nrLocuriLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // numeClientTextBox
            // 
            this.numeClientTextBox.Location = new System.Drawing.Point(145, 102);
            this.numeClientTextBox.Name = "numeClientTextBox";
            this.numeClientTextBox.Size = new System.Drawing.Size(222, 22);
            this.numeClientTextBox.TabIndex = 0;
            // 
            // numarTelefonTextBox
            // 
            this.numarTelefonTextBox.Location = new System.Drawing.Point(145, 158);
            this.numarTelefonTextBox.Name = "numarTelefonTextBox";
            this.numarTelefonTextBox.Size = new System.Drawing.Size(221, 22);
            this.numarTelefonTextBox.TabIndex = 1;
            // 
            // numarLocuriTextBox
            // 
            this.numarLocuriTextBox.Location = new System.Drawing.Point(145, 213);
            this.numarLocuriTextBox.Name = "numarLocuriTextBox";
            this.numarLocuriTextBox.Size = new System.Drawing.Size(221, 22);
            this.numarLocuriTextBox.TabIndex = 2;
            // 
            // rezervaButton
            // 
            this.rezervaButton.Location = new System.Drawing.Point(145, 251);
            this.rezervaButton.Name = "rezervaButton";
            this.rezervaButton.Size = new System.Drawing.Size(222, 25);
            this.rezervaButton.TabIndex = 3;
            this.rezervaButton.Text = "Rezerva";
            this.rezervaButton.UseVisualStyleBackColor = true;
            this.rezervaButton.Click += new System.EventHandler(this.rezervaButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "Pentru excursia catre";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(343, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 33);
            this.label3.TabIndex = 6;
            this.label3.Text = "mai sunt disponibile";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(592, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 27);
            this.label5.TabIndex = 8;
            this.label5.Text = "locuri.";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(32, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 34);
            this.label6.TabIndex = 9;
            this.label6.Text = "Nume client";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(32, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 23);
            this.label7.TabIndex = 10;
            this.label7.Text = "Numar telefon";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(32, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "Numar locuri";
            // 
            // numeObiectivLabel
            // 
            this.numeObiectivLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numeObiectivLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.numeObiectivLabel.Location = new System.Drawing.Point(205, 31);
            this.numeObiectivLabel.Name = "numeObiectivLabel";
            this.numeObiectivLabel.Size = new System.Drawing.Size(120, 28);
            this.numeObiectivLabel.TabIndex = 12;
            this.numeObiectivLabel.Text = "label2";
            this.numeObiectivLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nrLocuriLabel
            // 
            this.nrLocuriLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nrLocuriLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.nrLocuriLabel.Location = new System.Drawing.Point(478, 32);
            this.nrLocuriLabel.Name = "nrLocuriLabel";
            this.nrLocuriLabel.Size = new System.Drawing.Size(108, 29);
            this.nrLocuriLabel.TabIndex = 13;
            this.nrLocuriLabel.Text = "label2";
            this.nrLocuriLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(574, 246);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(101, 30);
            this.closeButton.TabIndex = 14;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // RezervareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 288);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.nrLocuriLabel);
            this.Controls.Add(this.numeObiectivLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rezervaButton);
            this.Controls.Add(this.numarLocuriTextBox);
            this.Controls.Add(this.numarTelefonTextBox);
            this.Controls.Add(this.numeClientTextBox);
            this.Name = "RezervareForm";
            this.Text = "RezervareForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RezervareForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button closeButton;

        private System.Windows.Forms.Label numeObiectivLabel;

        private System.Windows.Forms.Label nrLocuriLabel;

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;

        private System.Windows.Forms.Label label5;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Button rezervaButton;

        private System.Windows.Forms.TextBox numarLocuriTextBox;

        private System.Windows.Forms.TextBox numeClientTextBox;
        private System.Windows.Forms.TextBox numarTelefonTextBox;


        #endregion
    }
}