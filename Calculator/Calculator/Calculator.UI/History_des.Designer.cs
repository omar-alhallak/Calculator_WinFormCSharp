namespace Calculator.Calculator.UI
{
    partial class History_des
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
            btnMC = new RoundedButton();
            btnDelete = new RoundedButton();
            lstHistory = new ListView();
            columnHeader1 = new ColumnHeader();
            SuspendLayout();
            // 
            // btnMC
            // 
            btnMC.BackColor = Color.FromArgb(60, 60, 60);
            btnMC.FlatAppearance.BorderColor = Color.White;
            btnMC.FlatAppearance.BorderSize = 0;
            btnMC.FlatStyle = FlatStyle.Flat;
            btnMC.Font = new Font("Gadugi", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMC.ForeColor = Color.DarkOrange;
            btnMC.Location = new Point(113, 1);
            btnMC.Name = "btnMC";
            btnMC.Roundness = 0.65F;
            btnMC.Size = new Size(70, 41);
            btnMC.TabIndex = 2;
            btnMC.TabStop = false;
            btnMC.Text = "MC";
            btnMC.UseVisualStyleBackColor = false;
            btnMC.Click += btnAC_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(60, 60, 60);
            btnDelete.FlatAppearance.BorderColor = Color.White;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Gadugi", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDelete.ForeColor = Color.DarkOrange;
            btnDelete.Location = new Point(189, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.Roundness = 0.65F;
            btnDelete.Size = new Size(110, 41);
            btnDelete.TabIndex = 3;
            btnDelete.TabStop = false;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // lstHistory
            // 
            lstHistory.BackColor = Color.FromArgb(20, 20, 20);
            lstHistory.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            lstHistory.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstHistory.ForeColor = Color.White;
            lstHistory.FullRowSelect = true;
            lstHistory.HeaderStyle = ColumnHeaderStyle.None;
            lstHistory.Location = new Point(2, 48);
            lstHistory.MultiSelect = false;
            lstHistory.Name = "lstHistory";
            lstHistory.Size = new Size(297, 171);
            lstHistory.TabIndex = 4;
            lstHistory.UseCompatibleStateImageBehavior = false;
            lstHistory.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "";
            columnHeader1.Width = 25;
            // 
            // History_des
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(307, 221);
            Controls.Add(lstHistory);
            Controls.Add(btnDelete);
            Controls.Add(btnMC);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "History_des";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "History";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion
        private RoundedButton btnMC;
        private RoundedButton btnDelete;
        private ListView lstHistory;
        private ColumnHeader columnHeader1;
    }
}