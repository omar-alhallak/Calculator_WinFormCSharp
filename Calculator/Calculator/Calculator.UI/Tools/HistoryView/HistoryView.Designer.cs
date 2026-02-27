namespace Calculator.Calculator.UI.Tools.HistoryView
{
    partial class HistoryView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lstHistory = new ListView();
            columnHeader1 = new ColumnHeader();
            btnDelete = new RoundedButton();
            btnMC = new RoundedButton();
            lblMessageEmpty = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // lstHistory
            // 
            lstHistory.BackColor = Color.FromArgb(20, 20, 20);
            lstHistory.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            lstHistory.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstHistory.ForeColor = Color.White;
            lstHistory.FullRowSelect = true;
            lstHistory.HeaderStyle = ColumnHeaderStyle.None;
            lstHistory.Location = new Point(6, 70);
            lstHistory.Margin = new Padding(5);
            lstHistory.MultiSelect = false;
            lstHistory.Name = "lstHistory";
            lstHistory.Size = new Size(349, 273);
            lstHistory.TabIndex = 1;
            lstHistory.UseCompatibleStateImageBehavior = false;
            lstHistory.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "";
            columnHeader1.Width = 25;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(60, 60, 60);
            btnDelete.FlatAppearance.BorderColor = Color.White;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Gadugi", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDelete.ForeColor = Color.DarkOrange;
            btnDelete.Location = new Point(238, 5);
            btnDelete.Margin = new Padding(5);
            btnDelete.Name = "btnDelete";
            btnDelete.Roundness = 0.65F;
            btnDelete.Size = new Size(117, 55);
            btnDelete.TabIndex = 999;
            btnDelete.TabStop = false;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnMC
            // 
            btnMC.BackColor = Color.FromArgb(60, 60, 60);
            btnMC.FlatAppearance.BorderColor = Color.White;
            btnMC.FlatAppearance.BorderSize = 0;
            btnMC.FlatStyle = FlatStyle.Flat;
            btnMC.Font = new Font("Gadugi", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMC.ForeColor = Color.DarkOrange;
            btnMC.Location = new Point(159, 5);
            btnMC.Margin = new Padding(5);
            btnMC.Name = "btnMC";
            btnMC.Roundness = 0.65F;
            btnMC.Size = new Size(69, 55);
            btnMC.TabIndex = 999;
            btnMC.TabStop = false;
            btnMC.Text = "MC";
            btnMC.UseVisualStyleBackColor = false;
            btnMC.Click += btnMC_Click;
            // 
            // lblMessageEmpty
            // 
            lblMessageEmpty.BackColor = Color.FromArgb(20, 20, 20);
            lblMessageEmpty.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMessageEmpty.ForeColor = Color.White;
            lblMessageEmpty.Location = new Point(28, 82);
            lblMessageEmpty.Name = "lblMessageEmpty";
            lblMessageEmpty.Size = new Size(312, 38);
            lblMessageEmpty.TabIndex = 1000;
            lblMessageEmpty.Text = "\"ابدأ بالحساب لتظهر العمليات هنا\"";
            lblMessageEmpty.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(6, 5);
            label1.Name = "label1";
            label1.Size = new Size(139, 50);
            label1.TabIndex = 1001;
            label1.Text = "History";
            // 
            // HistoryView
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            Controls.Add(label1);
            Controls.Add(lblMessageEmpty);
            Controls.Add(lstHistory);
            Controls.Add(btnDelete);
            Controls.Add(btnMC);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "HistoryView";
            Size = new Size(362, 347);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lstHistory;
        private ColumnHeader columnHeader1;
        private RoundedButton btnDelete;
        private RoundedButton btnMC;
        private Label lblMessageEmpty;
        private Label label1;
    }
}
