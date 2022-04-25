using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordParser.Core.Models;

namespace WordParser.Core.Forms
{
    internal partial class SettingsProcessingWordsForm : Form
    {
        /// <summary>
        /// Настройки сортировки.
        /// </summary>
        private static readonly Dictionary<SortingWordsSettingsEnum, string> _sortingSettingsMap = new()
        {
            [SortingWordsSettingsEnum.NoSorting] = "Без сортировки",
            [SortingWordsSettingsEnum.SortByAlphabet] = "Сортировать по алфавиту",
            [SortingWordsSettingsEnum.SortByUniquenessFromMin] = "Сортировать от более уникальных",
            [SortingWordsSettingsEnum.SortByUniquenessFromMax] = "Сортировать от менее уникальных"
        };

        /// <summary>
        /// Настройки регистра результата.
        /// </summary>
        private static readonly Dictionary<LetterСaseEnum, string> _registerSettingsMap = new()
        {
            [LetterСaseEnum.Default] = "иСхоДный виД",
            [LetterСaseEnum.FirstLetterInUpper] = "Первая буква в верхнем регистре",
            [LetterСaseEnum.AllLetterInUpper] = "ВСЕ БУКВЫ В ВЕРХНЕМ РЕГИСТРЕ",
            [LetterСaseEnum.AllLetterInLower] = "все буквы в нижнем регистре"
        };

        /// <summary>
        /// Настройки парсинга.
        /// </summary>
        internal SettingsProcessingWordsForm()
        {
            InitializeComponent();
            HandleEventsForm();
        }

        /// <summary>
        /// Обработка событий формы.
        /// </summary>
        private void HandleEventsForm()
        {
            var mainSettings = ((WordParserMainForm)ActiveForm).SettingsProcessingWords;
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
                var newSettings = new SettingsProcessingWordsModel
                {
                    SortingMode = _sortingSettingsMap.FirstOrDefault(x => x.Value == sortingBox.Text).Key,
                    RegisterSettings = _registerSettingsMap.FirstOrDefault(x => x.Value == registerBox.Text).Key,
                    CheckIsLetter = isLetterCheckBox.Checked
                };

                if (newSettings.SortingMode != mainSettings.SortingMode ||
                    newSettings.RegisterSettings != mainSettings.RegisterSettings ||
                    newSettings.CheckIsLetter != mainSettings.CheckIsLetter)
                    ((WordParserMainForm)Owner).SettingsProcessingWords = new SettingsProcessingWordsModel(newSettings, true);

                this.Close();
            };
            this.cancelButton.Click += (s, e) => this.Close();
        }

        /// <summary>
        /// Установка настроек формы.
        /// </summary>
        private void SetSettingsToForm(SettingsProcessingWordsModel mainSettings)
        {
            sortingBox.Text = _sortingSettingsMap[mainSettings.SortingMode];
            registerBox.Text = _registerSettingsMap[mainSettings.RegisterSettings];
            isLetterCheckBox.Checked = mainSettings.CheckIsLetter;
        }

        /// <summary>
        /// Обводка формы.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
        }
    }
}
