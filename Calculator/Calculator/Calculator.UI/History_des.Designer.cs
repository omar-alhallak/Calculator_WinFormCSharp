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
            lbHistory = new ListBox();
            btnMC = new Button();
            btnDelete = new Button();
            SuspendLayout();
            // 
            // lbHistory
            // 
            lbHistory.BackColor = Color.FromArgb(32, 32, 32);
            lbHistory.Font = new Font("Segoe UI", 15F);
            lbHistory.ForeColor = Color.White;
            lbHistory.FormattingEnabled = true;
            lbHistory.HorizontalScrollbar = true;
            lbHistory.IntegralHeight = false;
            lbHistory.ItemHeight = 28;
            lbHistory.Location = new Point(4, 2);
            lbHistory.Name = "lbHistory";
            lbHistory.Size = new Size(297, 164);
            lbHistory.TabIndex = 1;
            lbHistory.DoubleClick += lbHistory_DoubleClick;
            // 
            // btnMC
            // 
            btnMC.BackColor = Color.FromArgb(32, 32, 32);
            btnMC.FlatAppearance.BorderColor = Color.White;
            btnMC.FlatAppearance.BorderSize = 0;
            btnMC.Font = new Font("Gadugi", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMC.ForeColor = Color.FromArgb(255, 128, 0);
            btnMC.Location = new Point(12, 172);
            btnMC.Name = "btnMC";
            btnMC.Size = new Size(70, 41);
            btnMC.TabIndex = 2;
            btnMC.Text = "MC";
            btnMC.UseVisualStyleBackColor = false;
            btnMC.Click += btnAC_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(32, 32, 32);
            btnDelete.FlatAppearance.BorderColor = Color.White;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Font = new Font("Gadugi", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDelete.ForeColor = Color.FromArgb(255, 128, 0);
            btnDelete.Location = new Point(182, 172);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(110, 41);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // History_des
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(304, 221);
            Controls.Add(btnDelete);
            Controls.Add(btnMC);
            Controls.Add(lbHistory);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximumSize = new Size(320, 260);
            MinimizeBox = false;
            MinimumSize = new Size(320, 260);
            Name = "History_des";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "History";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        private ListBox lbHistory;
        private Button btnMC;
        private Button btnDelete;
    }
}