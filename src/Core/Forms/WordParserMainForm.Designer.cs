namespace WordParser;

internal partial class WordParserMainForm
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.inputParsingUrlTextBox = new System.Windows.Forms.TextBox();
            this.startParsingButton = new System.Windows.Forms.Button();
            this.appExitButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.parsingGroupBox = new System.Windows.Forms.GroupBox();
            this.settingsParsingButton = new System.Windows.Forms.Button();
            this.resultGroupBox = new System.Windows.Forms.GroupBox();
            this.resultRichText = new System.Windows.Forms.RichTextBox();
            this.saveResultToTxtButton = new System.Windows.Forms.Button();
            this.saveResultToCsvButton = new System.Windows.Forms.Button();
            this.saveBox = new System.Windows.Forms.GroupBox();
            this.parsingGroupBox.SuspendLayout();
            this.resultGroupBox.SuspendLayout();
            this.saveBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputParsingUrlTextBox
            // 
            this.inputParsingUrlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputParsingUrlTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.inputParsingUrlTextBox.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.inputParsingUrlTextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.inputParsingUrlTextBox.Location = new System.Drawing.Point(3, 19);
            this.inputParsingUrlTextBox.Name = "inputParsingUrlTextBox";
            this.inputParsingUrlTextBox.Size = new System.Drawing.Size(530, 31);
            this.inputParsingUrlTextBox.TabIndex = 0;
            this.inputParsingUrlTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // startParsingButton
            // 
            this.startParsingButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.startParsingButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.startParsingButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.startParsingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startParsingButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.startParsingButton.Location = new System.Drawing.Point(3, 53);
            this.startParsingButton.Name = "startParsingButton";
            this.startParsingButton.Size = new System.Drawing.Size(451, 31);
            this.startParsingButton.TabIndex = 1;
            this.startParsingButton.Text = "Начать парсинг";
            this.startParsingButton.UseVisualStyleBackColor = true;
            // 
            // appExitButton
            // 
            this.appExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.appExitButton.FlatAppearance.BorderSize = 0;
            this.appExitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.appExitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Crimson;
            this.appExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appExitButton.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.appExitButton.ForeColor = System.Drawing.Color.DarkGray;
            this.appExitButton.Location = new System.Drawing.Point(522, 6);
            this.appExitButton.Name = "appExitButton";
            this.appExitButton.Size = new System.Drawing.Size(31, 29);
            this.appExitButton.TabIndex = 2;
            this.appExitButton.Text = "❌";
            this.appExitButton.UseVisualStyleBackColor = true;
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.titleLabel.Location = new System.Drawing.Point(221, 3);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(111, 25);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "WordParser";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // parsingGroupBox
            // 
            this.parsingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.parsingGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.parsingGroupBox.Controls.Add(this.settingsParsingButton);
            this.parsingGroupBox.Controls.Add(this.startParsingButton);
            this.parsingGroupBox.Controls.Add(this.inputParsingUrlTextBox);
            this.parsingGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.parsingGroupBox.Location = new System.Drawing.Point(12, 36);
            this.parsingGroupBox.Name = "parsingGroupBox";
            this.parsingGroupBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.parsingGroupBox.Size = new System.Drawing.Size(536, 90);
            this.parsingGroupBox.TabIndex = 4;
            this.parsingGroupBox.TabStop = false;
            this.parsingGroupBox.Text = "Парсинг";
            // 
            // settingsParsingButton
            // 
            this.settingsParsingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsParsingButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.settingsParsingButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.settingsParsingButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.settingsParsingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsParsingButton.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.settingsParsingButton.Location = new System.Drawing.Point(460, 53);
            this.settingsParsingButton.Name = "settingsParsingButton";
            this.settingsParsingButton.Size = new System.Drawing.Size(73, 31);
            this.settingsParsingButton.TabIndex = 7;
            this.settingsParsingButton.Text = "🛠️";
            this.settingsParsingButton.UseVisualStyleBackColor = true;
            // 
            // resultGroupBox
            // 
            this.resultGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultGroupBox.Controls.Add(this.resultRichText);
            this.resultGroupBox.Location = new System.Drawing.Point(12, 132);
            this.resultGroupBox.Name = "resultGroupBox";
            this.resultGroupBox.Size = new System.Drawing.Size(536, 261);
            this.resultGroupBox.TabIndex = 5;
            this.resultGroupBox.TabStop = false;
            this.resultGroupBox.Text = "Результат";
            // 
            // resultRichText
            // 
            this.resultRichText.Dock = System.Windows.Forms.DockStyle.Top;
            this.resultRichText.Location = new System.Drawing.Point(3, 19);
            this.resultRichText.Name = "resultRichText";
            this.resultRichText.Size = new System.Drawing.Size(530, 237);
            this.resultRichText.TabIndex = 0;
            this.resultRichText.Text = "";
            // 
            // saveResultToTxtButton
            // 
            this.saveResultToTxtButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.saveResultToTxtButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.saveResultToTxtButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.saveResultToTxtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveResultToTxtButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.saveResultToTxtButton.Location = new System.Drawing.Point(6, 22);
            this.saveResultToTxtButton.Name = "saveResultToTxtButton";
            this.saveResultToTxtButton.Size = new System.Drawing.Size(255, 31);
            this.saveResultToTxtButton.TabIndex = 1;
            this.saveResultToTxtButton.Text = "Сохранить в .txt";
            this.saveResultToTxtButton.UseVisualStyleBackColor = true;
            // 
            // saveResultToCsvButton
            // 
            this.saveResultToCsvButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.saveResultToCsvButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.saveResultToCsvButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.saveResultToCsvButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveResultToCsvButton.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.saveResultToCsvButton.Location = new System.Drawing.Point(275, 22);
            this.saveResultToCsvButton.Name = "saveResultToCsvButton";
            this.saveResultToCsvButton.Size = new System.Drawing.Size(255, 31);
            this.saveResultToCsvButton.TabIndex = 1;
            this.saveResultToCsvButton.Text = "Сохранить в .csv";
            this.saveResultToCsvButton.UseVisualStyleBackColor = true;
            // 
            // saveBox
            // 
            this.saveBox.Controls.Add(this.saveResultToTxtButton);
            this.saveBox.Controls.Add(this.saveResultToCsvButton);
            this.saveBox.Enabled = false;
            this.saveBox.Location = new System.Drawing.Point(12, 394);
            this.saveBox.Name = "saveBox";
            this.saveBox.Size = new System.Drawing.Size(536, 59);
            this.saveBox.TabIndex = 6;
            this.saveBox.TabStop = false;
            this.saveBox.Text = "Результат";
            // 
            // WordParserMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(560, 462);
            this.Controls.Add(this.saveBox);
            this.Controls.Add(this.resultGroupBox);
            this.Controls.Add(this.parsingGroupBox);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.appExitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(560, 462);
            this.MinimumSize = new System.Drawing.Size(560, 462);
            this.Name = "WordParserMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WordParser";
            this.parsingGroupBox.ResumeLayout(false);
            this.parsingGroupBox.PerformLayout();
            this.resultGroupBox.ResumeLayout(false);
            this.saveBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox inputParsingUrlTextBox;
    private System.Windows.Forms.Button startParsingButton;
    private System.Windows.Forms.Button appExitButton;
    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.GroupBox parsingGroupBox;
    private System.Windows.Forms.GroupBox resultGroupBox;
    private System.Windows.Forms.RichTextBox resultRichText;
    private System.Windows.Forms.Button settingsParsingButton;
    private System.Windows.Forms.Button saveResultToTxtButton;
    private System.Windows.Forms.Button saveResultToCsvButton;
    private System.Windows.Forms.GroupBox saveBox;
}