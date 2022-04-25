namespace WordParser.Core.Forms;

internal partial class SettingsProcessingWordsForm
{
    private System.ComponentModel.IContainer components = null;

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
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.isLetterCheckBox = new System.Windows.Forms.CheckBox();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.registerBox = new System.Windows.Forms.ComboBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.sortingBox = new System.Windows.Forms.ComboBox();
        this.acceptButton = new System.Windows.Forms.Button();
        this.cancelButton = new System.Windows.Forms.Button();
        this.groupBox1.SuspendLayout();
        this.groupBox3.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.SuspendLayout();
        // 
        // groupBox1
        // 
        this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox1.Controls.Add(this.isLetterCheckBox);
        this.groupBox1.Controls.Add(this.groupBox3);
        this.groupBox1.Controls.Add(this.groupBox2);
        this.groupBox1.Controls.Add(this.acceptButton);
        this.groupBox1.Controls.Add(this.cancelButton);
        this.groupBox1.Location = new System.Drawing.Point(12, 7);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(291, 185);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        // 
        // isLetterCheckBox
        // 
        this.isLetterCheckBox.AutoSize = true;
        this.isLetterCheckBox.Location = new System.Drawing.Point(5, 123);
        this.isLetterCheckBox.Name = "isLetterCheckBox";
        this.isLetterCheckBox.Size = new System.Drawing.Size(283, 19);
        this.isLetterCheckBox.TabIndex = 7;
        this.isLetterCheckBox.Text = "Строка должна содержать хотя бы одну букву.";
        this.isLetterCheckBox.UseVisualStyleBackColor = true;
        // 
        // groupBox3
        // 
        this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox3.Controls.Add(this.registerBox);
        this.groupBox3.Location = new System.Drawing.Point(6, 68);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.groupBox3.Size = new System.Drawing.Size(279, 49);
        this.groupBox3.TabIndex = 6;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "Настройки регистра";
        // 
        // registerBox
        // 
        this.registerBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.registerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.registerBox.FormattingEnabled = true;
        this.registerBox.Location = new System.Drawing.Point(3, 19);
        this.registerBox.Name = "registerBox";
        this.registerBox.Size = new System.Drawing.Size(273, 23);
        this.registerBox.TabIndex = 4;
        // 
        // groupBox2
        // 
        this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox2.Controls.Add(this.sortingBox);
        this.groupBox2.Location = new System.Drawing.Point(5, 13);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.groupBox2.Size = new System.Drawing.Size(279, 49);
        this.groupBox2.TabIndex = 6;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Настройки сортировки";
        // 
        // sortingBox
        // 
        this.sortingBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.sortingBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.sortingBox.FormattingEnabled = true;
        this.sortingBox.Location = new System.Drawing.Point(3, 19);
        this.sortingBox.Name = "sortingBox";
        this.sortingBox.Size = new System.Drawing.Size(273, 23);
        this.sortingBox.TabIndex = 4;
        // 
        // acceptButton
        // 
        this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.acceptButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
        this.acceptButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
        this.acceptButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
        this.acceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.acceptButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.acceptButton.Location = new System.Drawing.Point(5, 148);
        this.acceptButton.Name = "acceptButton";
        this.acceptButton.Size = new System.Drawing.Size(135, 30);
        this.acceptButton.TabIndex = 3;
        this.acceptButton.Text = "Принять";
        this.acceptButton.UseVisualStyleBackColor = true;
        // 
        // cancelButton
        // 
        this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
        this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
        this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
        this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.cancelButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.cancelButton.Location = new System.Drawing.Point(150, 148);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(135, 30);
        this.cancelButton.TabIndex = 3;
        this.cancelButton.Text = "Отмена";
        this.cancelButton.UseVisualStyleBackColor = true;
        // 
        // SettingsProcessingWordsForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.White;
        this.ClientSize = new System.Drawing.Size(315, 203);
        this.Controls.Add(this.groupBox1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Name = "SettingsProcessingWordsForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "SettingsResultForm";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox3.ResumeLayout(false);
        this.groupBox2.ResumeLayout(false);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.ComboBox sortingBox;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox isLetterCheckBox;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.ComboBox registerBox;
}