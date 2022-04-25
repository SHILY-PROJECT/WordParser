namespace WordParser;

internal partial class WordParserMainForm : Form
{
    private readonly IWordParser _parser;
    private IList<WordModel> _resultWordList;
    private readonly string _inputParsingUrlTextBoxDefText;

    public WordParserProcessSettingsModel _wordParserProcessSettings;

    internal WordParserMainForm(IWordParser parser, WordParserProcessSettingsModel settingsProcessingWords)
    {
        _parser = parser;
        _wordParserProcessSettings = settingsProcessingWords;

        _resultWordList = new List<WordModel>();
        _inputParsingUrlTextBoxDefText = "Введите URL для парсинга...";

        InitializeComponent();
        HandleEventsForm();
    }

    private static FileInfo GetFileNameToSaveResult(ResultFileTypeEnum rsultFileType)
    {
        var extension = rsultFileType switch
        {
            ResultFileTypeEnum.Csv      => ".csv",
            ResultFileTypeEnum.Txt or _ => ".txt",
        };

        var file = new FileInfo(Path.Combine("result", $"result   {DateTime.Now:yyyy-MM-dd   HH-mm-ss---fffffff}{extension}"));

        if (file.Directory != null && !file.Directory.Exists) file.Directory.Create();

        return file;
    }

    private void HandleEventsForm()
    {
        var appExitButtonDefColor = this.appExitButton.ForeColor;
        var startParsingButtonDefColor = this.startParsingButton.ForeColor;
        var settingsParsingButtonDefColor = this.settingsParsingButton.ForeColor;

        this.Load += (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(this.inputParsingUrlTextBox.Text) || this.inputParsingUrlTextBox.Text == _inputParsingUrlTextBoxDefText)
            {
                this.inputParsingUrlTextBox.Text = _inputParsingUrlTextBoxDefText;
                this.inputParsingUrlTextBox.ForeColor = Color.DarkGray;
            }
        };
        this.FormClosing += (s, e) => WordParserSettingsHandler.SaveSettings(_wordParserProcessSettings);

        #region [MOVING FORM]====================================================================
        this.MouseDown += (s, e) => MoveFormPosition(this);
        this.MouseMove += (s, e) => MoveFormPosition(this);
        this.MouseUp += (s, e) => MoveFormPosition(this);
        this.titleLabel.MouseDown += (s, e) => MoveFormPosition(this.titleLabel);
        this.titleLabel.MouseMove += (s, e) => MoveFormPosition(this.titleLabel);
        this.titleLabel.MouseUp += (s, e) => MoveFormPosition(this.titleLabel);
        #endregion ==============================================================================

        this.appExitButton.Click += (s, e) => Application.Exit();
        this.appExitButton.MouseMove += (s, e) => this.appExitButton.ForeColor = Color.White;
        this.appExitButton.MouseLeave += (s, e) => this.appExitButton.ForeColor = appExitButtonDefColor;
        this.startParsingButton.MouseMove += (s, e) => this.startParsingButton.ForeColor = Color.White;
        this.startParsingButton.MouseLeave += (s, e) => this.startParsingButton.ForeColor = startParsingButtonDefColor;
        this.settingsParsingButton.MouseMove += (s, e) => this.settingsParsingButton.ForeColor = Color.White;
        this.settingsParsingButton.MouseLeave += (s, e) => this.settingsParsingButton.ForeColor = settingsParsingButtonDefColor;
        this.resultRichText.TextChanged += (s, e) =>
        {
            var count = this.resultRichText.Lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray().Length;
            this.resultGroupBox.Text = count == 0 ? $"Результат" : $"Результат [{count}]";
        };
        this.inputParsingUrlTextBox.Click += (s, e) =>
        {
            if (this.inputParsingUrlTextBox.Text == _inputParsingUrlTextBoxDefText)
            {
                this.inputParsingUrlTextBox.Text = string.Empty;
                this.inputParsingUrlTextBox.ForeColor = Color.Black;
            }
        };
        this.inputParsingUrlTextBox.LostFocus += (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(this.inputParsingUrlTextBox.Text) || this.inputParsingUrlTextBox.Text == _inputParsingUrlTextBoxDefText)
            {
                this.inputParsingUrlTextBox.Text = _inputParsingUrlTextBoxDefText;
                this.inputParsingUrlTextBox.ForeColor = Color.DarkGray;
            }
        };

        #region [PARSING]==========================================================================
        this.startParsingButton.Click += (s, e) => RunParsing(ParsingType.StartParsing);
        this.settingsParsingButton.Click += (s, e) =>
        {
            using var settingsResultForm = new WordParserProcessSettingsForm { Owner = this };
            settingsResultForm.ShowDialog();
            if (_wordParserProcessSettings.SettingsIsUpdated) RunParsing(ParsingType.ReParsing);
        };
        #endregion ================================================================================

        #region [SAVE RESULT]======================================================================
        this.saveResultToTxtButton.Click += async (s, e) =>
        {
            var file = GetFileNameToSaveResult(ResultFileTypeEnum.Txt);
            await File.WriteAllLinesAsync(file.FullName, _resultWordList.Select(x => $"{x.Word} - {x.Quantity}"), Encoding.UTF8);
            OpenFileInExplorer(file);
        };
        this.saveResultToCsvButton.Click += async (s, e) =>
        {
            var file = GetFileNameToSaveResult(ResultFileTypeEnum.Csv);
            await File.WriteAllLinesAsync(file.FullName, _resultWordList.Select(x => $"{x.Word},{x.Quantity}"), Encoding.UTF8);
            OpenFileInExplorer(file);
        };
        #endregion ================================================================================
    }

    private async void RunParsing(ParsingType parsingType)
    {
        using var waitForm = new WaitForm { Owner = this };
        waitForm.Show();
        this.Enabled = false;
        this.saveBox.Enabled = false;

        if (this.resultRichText.Text.Any() || _resultWordList.Any())
        {
            this.resultRichText.Text = string.Empty;
            _resultWordList.Clear();
        }

        _resultWordList = parsingType switch
        {
            ParsingType.StartParsing => await _parser.Parse(this.inputParsingUrlTextBox.Text != _inputParsingUrlTextBoxDefText ? this.inputParsingUrlTextBox.Text : string.Empty),
            ParsingType.ReParsing => await _parser.ReApplyFilterAsync(),
            _ => _resultWordList
        };

        if (_resultWordList.Any())
        {
            this.resultRichText.Invoke((MethodInvoker)delegate { resultRichText.Text = string.Join(Environment.NewLine, _resultWordList.Select(x => $"{x.Word} - {x.Quantity}")); });
            this.saveBox.Invoke((MethodInvoker)delegate { saveBox.Enabled = true; });
        }
        else if (!string.IsNullOrWhiteSpace(_parser.DetectorMessageError))
        {
            this.resultRichText.Invoke((MethodInvoker)delegate { resultRichText.Text = $"{_parser.DetectorMessageError}"; });
        }

        this.Enabled = true;
    }

    private static void OpenFileInExplorer(FileInfo file)
        => Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n, /select, {file.FullName}" });

    private void MoveFormPosition(Control control)
    {
        var drag = default(bool);
        var mouseX = default(int);
        var mouseY = default(int);

        control.MouseDown += (s, e) =>
        {
            drag = true;
            mouseX = Cursor.Position.X - this.Left;
            mouseY = Cursor.Position.Y - this.Top;
        };

        control.MouseMove += (s, e) =>
        {
            if (drag)
            {
                this.Top = Cursor.Position.Y - mouseY;
                this.Left = Cursor.Position.X - mouseX;
            }
        };

        control.MouseUp += (s, e) => drag = false;
    }

    #region Shadow of form & Border for form.
    private const int WM_NCHITTEST = 0x84;
    private const int HTCLIENT = 0x1;
    private const int HTCAPTION = 0x2;

    private bool m_aeroEnabled;

    private const int CS_DROPSHADOW = 0x00020000;
    private const int WM_NCPAINT = 0x0085;
    private const int WM_ACTIVATEAPP = 0x001C;

    [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
    public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

    [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
    public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

    [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn(
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
        );

    public struct MARGINS
    {
        public int leftWidth;
        public int rightWidth;
        public int topHeight;
        public int bottomHeight;
    }

    protected override CreateParams CreateParams
    {
        get
        {
            m_aeroEnabled = CheckAeroEnabled();
            CreateParams cp = base.CreateParams;
            if (!m_aeroEnabled)
                cp.ClassStyle |= CS_DROPSHADOW; return cp;
        }
    }

    private bool CheckAeroEnabled()
    {
        if (Environment.OSVersion.Version.Major >= 6)
        {
            int enabled = 0; DwmIsCompositionEnabled(ref enabled);
            return (enabled == 1) ? true : false;
        }
        return false;
    }

    protected override void WndProc(ref Message m)
    {
        switch (m.Msg)
        {
            case WM_NCPAINT:
                if (m_aeroEnabled)
                {
                    var v = 2;
                    DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                    MARGINS margins = new MARGINS()
                    {
                        bottomHeight = 1,
                        leftWidth = 0,
                        rightWidth = 0,
                        topHeight = 0
                    }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                }
                break;
            default: break;
        }
        base.WndProc(ref m);
        if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
    }

    /// <summary>
    /// Border for form.
    /// </summary>
    protected override void OnPaint(PaintEventArgs e)
    {
        ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
    }
    #endregion
}
