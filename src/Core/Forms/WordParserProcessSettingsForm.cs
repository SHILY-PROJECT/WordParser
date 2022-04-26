namespace WordParser.Core.Forms;

internal partial class WordParserProcessSettingsForm : Form
{
    private readonly IServiceProvider _serviceProvider;
    private readonly WordParserProcessSettingsModel _wordParserProcessSettings;

    private static readonly Dictionary<WordSortType, string> _sortingSettingsMap = new()
    {
        [WordSortType.NoSorting] = "Без сортировки",
        [WordSortType.SortByAlphabet] = "Сортировать по алфавиту",
        [WordSortType.SortByUniquenessFromMin] = "Сортировать от более уникальных",
        [WordSortType.SortByUniquenessFromMax] = "Сортировать от менее уникальных"
    };

    private static readonly Dictionary<LetterСase, string> _registerSettingsMap = new()
    {
        [LetterСase.Default] = "иСхоДный виД",
        [LetterСase.FirstLetterInUpper] = "Первая буква в верхнем регистре",
        [LetterСase.AllLetterInUpper] = "ВСЕ БУКВЫ В ВЕРХНЕМ РЕГИСТРЕ",
        [LetterСase.AllLetterInLower] = "все буквы в нижнем регистре"
    };

    public WordParserProcessSettingsForm(IServiceProvider serviceProvider, WordParserProcessSettingsModel wordParserProcessSettings)
    {
        _serviceProvider = serviceProvider;
        _wordParserProcessSettings = wordParserProcessSettings;

        InitializeComponent();
        HandleEventsForm();

        Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
    }

    private void HandleEventsForm()
    {
        var mainSettings = ((WordParserMainForm)ActiveForm)._wordParserProcessSettings;
        var acceptButtonDefColor = this.acceptButton.ForeColor;
        var cancelButtonDefColor = this.cancelButton.ForeColor;

        this.acceptButton.MouseMove += (s, e) => this.acceptButton.ForeColor = Color.White;
        this.acceptButton.MouseLeave += (s, e) => this.acceptButton.ForeColor = acceptButtonDefColor;
        this.cancelButton.MouseMove += (s, e) => this.cancelButton.ForeColor = Color.White;
        this.cancelButton.MouseLeave += (s, e) => this.cancelButton.ForeColor = cancelButtonDefColor;
        this.Load += (s, e) =>
        {
            this.Location = new Point(Owner.Location.X + Owner.Width / 2 - this.Width / 2, Owner.Location.Y + Owner.Height / 2 - this.Height / 2);
            this.sortingBox.Items.AddRange(_sortingSettingsMap.Values.ToArray());
            this.registerBox.Items.AddRange(_registerSettingsMap.Values.ToArray());
            SetSettingsToForm(mainSettings);
        };
        this.acceptButton.Click += (s, e) =>
        {
            _wordParserProcessSettings.ChangeSettingsIfNeeded(new WordParserProcessSettingsModel
            {
                SortType = _sortingSettingsMap.FirstOrDefault(x => x.Value == sortingBox.Text).Key,
                LetterСase = _registerSettingsMap.FirstOrDefault(x => x.Value == registerBox.Text).Key,
                CheckIsLetter = isLetterCheckBox.Checked
            });
            this.Close();
        };
        this.cancelButton.Click += (s, e) => this.Close();
    }

    private void SetSettingsToForm(WordParserProcessSettingsModel mainSettings)
    {
        sortingBox.Text = _sortingSettingsMap[mainSettings.SortType];
        registerBox.Text = _registerSettingsMap[mainSettings.LetterСase];
        isLetterCheckBox.Checked = mainSettings.CheckIsLetter;
    }

    #region Shadow of form & Border for form.
    private const int WM_NCHITTEST = 0x84;
    private const int HTCLIENT = 0x1;
    private const int HTCAPTION = 0x2;

    private bool m_aeroEnabled;

    private const int CS_DROPSHADOW = 0x00020000;
    private const int WM_NCPAINT = 0x0085;
    private const int WM_ACTIVATEAPP = 0x001C;

    [DllImport("dwmapi.dll")]
    public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

    [DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    [DllImport("dwmapi.dll")]
    public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn
    (
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

    protected override void OnPaint(PaintEventArgs e)
    {
        ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
    }
    #endregion
}