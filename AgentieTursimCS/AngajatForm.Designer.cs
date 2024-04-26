using System.ComponentModel;

namespace AgentieTurismCS
{
    partial class AngajatForm
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
            this.dataGridViewExcursiiCautate = new System.Windows.Forms.DataGridView();
            this.obiectivTuristicTextBox = new System.Windows.Forms.TextBox();
            this.deLaOraTextBox = new System.Windows.Forms.TextBox();
            this.panaLaOraTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cautaButton = new System.Windows.Forms.Button();
            this.rezervaButton = new System.Windows.Forms.Button();
            this.logOutButton = new System.Windows.Forms.Button();
            this.dataGridViewToateExcursiile = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExcursiiCautate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToateExcursiile)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewExcursiiCautate
            // 
            this.dataGridViewExcursiiCautate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExcursiiCautate.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.dataGridViewExcursiiCautate.Location = new System.Drawing.Point(13, 240);
            this.dataGridViewExcursiiCautate.Name = "dataGridViewExcursiiCautate";
            this.dataGridViewExcursiiCautate.RowTemplate.Height = 24;
            this.dataGridViewExcursiiCautate.Size = new System.Drawing.Size(876, 143);
            this.dataGridViewExcursiiCautate.TabIndex = 1;
            this.dataGridViewExcursiiCautate.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewExcursiiCautate_CellPainting);
            // 
            // obiectivTuristicTextBox
            // 
            this.obiectivTuristicTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.obiectivTuristicTextBox.ForeColor = System.Drawing.Color.Silver;
            this.obiectivTuristicTextBox.Location = new System.Drawing.Point(180, 424);
            this.obiectivTuristicTextBox.Name = "obiectivTuristicTextBox";
            this.obiectivTuristicTextBox.Size = new System.Drawing.Size(191, 27);
            this.obiectivTuristicTextBox.TabIndex = 2;
            this.obiectivTuristicTextBox.Text = " nume obiectiv";
            this.obiectivTuristicTextBox.Enter += new System.EventHandler(this.obiectivTuristic_Enter);
            this.obiectivTuristicTextBox.Leave += new System.EventHandler(this.obiectivTuristic_Leave);
            // 
            // deLaOraTextBox
            // 
            this.deLaOraTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLaOraTextBox.ForeColor = System.Drawing.Color.Silver;
            this.deLaOraTextBox.Location = new System.Drawing.Point(459, 424);
            this.deLaOraTextBox.Name = "deLaOraTextBox";
            this.deLaOraTextBox.Size = new System.Drawing.Size(90, 27);
            this.deLaOraTextBox.TabIndex = 3;
            this.deLaOraTextBox.Text = " ora";
            this.deLaOraTextBox.Enter += new System.EventHandler(this.deLaOra_Enter);
            this.deLaOraTextBox.Leave += new System.EventHandler(this.deLaOra_Leave);
            // 
            // panaLaOraTextBox
            // 
            this.panaLaOraTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panaLaOraTextBox.ForeColor = System.Drawing.Color.Silver;
            this.panaLaOraTextBox.Location = new System.Drawing.Point(630, 423);
            this.panaLaOraTextBox.Name = "panaLaOraTextBox";
            this.panaLaOraTextBox.Size = new System.Drawing.Size(90, 27);
            this.panaLaOraTextBox.TabIndex = 4;
            this.panaLaOraTextBox.Text = " ora";
            this.panaLaOraTextBox.Enter += new System.EventHandler(this.panaLaOra_Enter);
            this.panaLaOraTextBox.Leave += new System.EventHandler(this.panaLaOra_Leave);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 430);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Excursie către";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(393, 426);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "între ora";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(577, 427);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "și";
            // 
            // cautaButton
            // 
            this.cautaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cautaButton.Location = new System.Drawing.Point(752, 423);
            this.cautaButton.Name = "cautaButton";
            this.cautaButton.Size = new System.Drawing.Size(137, 28);
            this.cautaButton.TabIndex = 8;
            this.cautaButton.Text = "Cauta";
            this.cautaButton.UseVisualStyleBackColor = true;
            this.cautaButton.Click += new System.EventHandler(this.cautaButton_click);
            // 
            // rezervaButton
            // 
            this.rezervaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rezervaButton.Location = new System.Drawing.Point(752, 483);
            this.rezervaButton.Name = "rezervaButton";
            this.rezervaButton.Size = new System.Drawing.Size(137, 28);
            this.rezervaButton.TabIndex = 9;
            this.rezervaButton.Text = "Rezervă";
            this.rezervaButton.UseVisualStyleBackColor = true;
            this.rezervaButton.Click += new System.EventHandler(this.rezervaButton_Click);
            // 
            // logOutButton
            // 
            this.logOutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logOutButton.Location = new System.Drawing.Point(13, 484);
            this.logOutButton.Name = "logOutButton";
            this.logOutButton.Size = new System.Drawing.Size(138, 32);
            this.logOutButton.TabIndex = 10;
            this.logOutButton.Text = "Log out\r\n";
            this.logOutButton.UseVisualStyleBackColor = true;
            this.logOutButton.Click += new System.EventHandler(this.logOutButton_click);
            // 
            // dataGridViewToateExcursiile
            // 
            this.dataGridViewToateExcursiile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewToateExcursiile.Location = new System.Drawing.Point(13, 28);
            this.dataGridViewToateExcursiile.Name = "dataGridViewToateExcursiile";
            this.dataGridViewToateExcursiile.RowTemplate.Height = 24;
            this.dataGridViewToateExcursiile.Size = new System.Drawing.Size(876, 185);
            this.dataGridViewToateExcursiile.TabIndex = 0;
            this.dataGridViewToateExcursiile.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewToateExcursiile_CellPainting);
            // 
            // AngajatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 523);
            this.Controls.Add(this.logOutButton);
            this.Controls.Add(this.rezervaButton);
            this.Controls.Add(this.cautaButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panaLaOraTextBox);
            this.Controls.Add(this.deLaOraTextBox);
            this.Controls.Add(this.obiectivTuristicTextBox);
            this.Controls.Add(this.dataGridViewExcursiiCautate);
            this.Controls.Add(this.dataGridViewToateExcursiile);
            this.Name = "AngajatForm";
            this.Text = "AngajatForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AngajatForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExcursiiCautate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToateExcursiile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button logOutButton;

        private System.Windows.Forms.Button rezervaButton;

        private System.Windows.Forms.Button cautaButton;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.TextBox obiectivTuristicTextBox;
        private System.Windows.Forms.TextBox deLaOraTextBox;
        private System.Windows.Forms.TextBox panaLaOraTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.DataGridView dataGridViewExcursiiCautate;

        private System.Windows.Forms.DataGridView dataGridViewToateExcursiile;

        #endregion
    }
}