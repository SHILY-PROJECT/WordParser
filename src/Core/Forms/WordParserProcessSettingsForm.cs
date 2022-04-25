namespace WordParser.Core.Forms;

internal partial class WordParserProcessSettingsForm : Form
{
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

    internal WordParserProcessSettingsForm()
    {
        InitializeComponent();
        HandleEventsForm();
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
            var newSettings = new WordParserProcessSettingsModel
            {
                SortMode = _sortingSettingsMap.FirstOrDefault(x => x.Value == sortingBox.Text).Key,
                RegisterSettings = _registerSettingsMap.FirstOrDefault(x => x.Value == registerBox.Text).Key,
                CheckIsLetter = isLetterCheckBox.Checked
            };

            if (newSettings.SortMode != mainSettings.SortMode ||
                newSettings.RegisterSettings != mainSettings.RegisterSettings ||
                newSettings.CheckIsLetter != mainSettings.CheckIsLetter)
                ((WordParserMainForm)Owner)._wordParserProcessSettings = new WordParserProcessSettingsModel(newSettings, true);

            this.Close();
        };
        this.cancelButton.Click += (s, e) => this.Close();
    }

    private void SetSettingsToForm(WordParserProcessSettingsModel mainSettings)
    {
        sortingBox.Text = _sortingSettingsMap[mainSettings.SortMode];
        registerBox.Text = _registerSettingsMap[mainSettings.RegisterSettings];
        isLetterCheckBox.Checked = mainSettings.CheckIsLetter;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
    }
}