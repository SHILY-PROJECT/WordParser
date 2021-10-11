using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordParser.Core.Models;
using WordParser.Core.Forms;
using WordParser.Core.Enums;
using System.IO;
using Newtonsoft.Json;
using WordParser.Core.Enums.Logger;
using WordParser.Core.Toolkit;
using WordParser.Core.Services;
using System.Diagnostics;
using WordParser.Core.Enums.Services;

namespace WordParser
{
    internal partial class WordParserMainForm : Form
    {
        private static readonly FileInfo _settingsProcessingWordsFile = new("settings_processing_words.ini");
        private readonly WordParserMain _wordParser;
        private readonly string _inputParsingUrlTextBoxDefText;
        private List<WordItemModel> _resultWordList;

        /// <summary>
        /// Настройки обработки слов.
        /// </summary>
        internal SettingsProcessingWordsModel SettingsProcessingWords { get; set; } = new();

        /// <summary>
        /// Конструктор главной формы.
        /// </summary>
        internal WordParserMainForm()
        {
            _resultWordList = new();
            _wordParser = new();
            _inputParsingUrlTextBoxDefText = "Введите URL для парсинга...";

            InitializeComponent();
            HandleEventsForm();
        }

        /// <summary>
        /// Получение имени файла для сохранения результата.
        /// </summary>
        /// <param name="rsultFileType"></param>
        /// <returns></returns>
        private static FileInfo GetNameResult(ResultFileTypeEnum rsultFileType)
        {
            var extension = rsultFileType switch
            {
                ResultFileTypeEnum.Csv  => ".csv",
                _                       => ".txt",
            };
            var file = new FileInfo(Path.Combine("result", $"result   {DateTime.Now:yyyy-MM-dd   HH-mm-ss---fffffff}{extension}"));

            if (file.Directory != null && !file.Directory.Exists) file.Directory.Create();

            return file;
        }

        /// <summary>
        /// Сохранение настроек.
        /// </summary>
        private void SaveSettings()
            => File.WriteAllText(_settingsProcessingWordsFile.FullName, JsonConvert.SerializeObject(SettingsProcessingWords, Formatting.Indented), Encoding.UTF8);

        /// <summary>
        /// Загрузка настроек.
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                if (_settingsProcessingWordsFile.Exists)
                    SettingsProcessingWords = JsonConvert.DeserializeObject<SettingsProcessingWordsModel>(File.ReadAllText(_settingsProcessingWordsFile.FullName, Encoding.UTF8)) ?? new();
            }
            catch (Exception ex)
            {
                Logger.Write($"Не удалось загрузить настройки парсера  |  MESSAGE ERROR: {ex.Message}", LogTypeEnum.Error);
            }
        }

        /// <summary>
        /// Обработка событий формы.
        /// </summary>
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

                LoadSettings();
            };
            this.FormClosing += (s, e) => SaveSettings();

            #region Юлозилка формы
            this.MouseDown += (s, e) => MovePositionForm(this);
            this.MouseMove += (s, e) => MovePositionForm(this);
            this.MouseUp += (s, e) => MovePositionForm(this);
            this.titleLabel.MouseDown += (s, e) => MovePositionForm(this.titleLabel);
            this.titleLabel.MouseMove += (s, e) => MovePositionForm(this.titleLabel);
            this.titleLabel.MouseUp += (s, e) => MovePositionForm(this.titleLabel);
            #endregion

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

            #region Парсинг
            this.startParsingButton.Click += (s, e) => Parsing(ParsingTypeEnum.StartParsing);
            this.settingsParsingButton.Click += (s, e) =>
            {
                using var settingsResultForm = new SettingsProcessingWordsForm { Owner = this };
                settingsResultForm.ShowDialog();
                if (SettingsProcessingWords.SettingsIsUpdated) Parsing(ParsingTypeEnum.ReParsing);
            };
            #endregion

            #region Сохранение результата
            this.saveResultToTxtButton.Click += async (s, e) =>
            {
                var file = GetNameResult(ResultFileTypeEnum.Txt);
                await File.WriteAllLinesAsync(file.FullName, _resultWordList.Select(x => $"{x.Word} - {x.WordUniqueness}"), Encoding.UTF8);
                OpenFileInExplorer(file);
            };
            this.saveResultToCsvButton.Click += async (s, e) =>
            {
                var file = GetNameResult(ResultFileTypeEnum.Csv);
                await File.WriteAllLinesAsync(file.FullName, _resultWordList.Select(x => $"{x.Word},{x.WordUniqueness}"), Encoding.UTF8);
                OpenFileInExplorer(file);
            };
            #endregion
        }

        /// <summary>
        /// Старт парсинга.
        /// </summary>
        /// <param name="parsingType"></param>
        private async void Parsing(ParsingTypeEnum parsingType)
        {
            using var waitForm = new WaitForm { Owner = this };
            waitForm.Show();
            this.Enabled = false;
            this.saveBox.Enabled = false;

            if (this.resultRichText.Text.Length != 0 || _resultWordList.Count != 0)
            {
                this.resultRichText.Text = string.Empty;
                _resultWordList.Clear();
            }

            await Task.Run(() =>
            {
                switch (parsingType)
                {
                    case ParsingTypeEnum.StartParsing:
                        var url = this.inputParsingUrlTextBox.Text != _inputParsingUrlTextBoxDefText ? this.inputParsingUrlTextBox.Text : string.Empty;
                        _resultWordList = _wordParser.StartParsing(url, SettingsProcessingWords);
                        break;
                    case ParsingTypeEnum.ReParsing:
                        _resultWordList = _wordParser.ReParsing(SettingsProcessingWords);
                        SettingsProcessingWords.SettingsIsUpdated = false;
                        break;
                }

                if (_resultWordList.Count != 0)
                {
                    this.resultRichText.Invoke((MethodInvoker)delegate { resultRichText.Text = string.Join(Environment.NewLine, _resultWordList.Select(x => $"{x.Word} - {x.WordUniqueness}")); });
                    this.saveBox.Invoke((MethodInvoker)delegate { saveBox.Enabled = true; });
                }
                else if (!string.IsNullOrWhiteSpace(_wordParser.DetectorMessageError))
                {
                    this.resultRichText.Invoke((MethodInvoker)delegate { resultRichText.Text = $"{_wordParser.DetectorMessageError}"; });
                }
            });

            this.Enabled = true;
        }

        /// <summary>
        /// Открывает папку и выделяет файл с результатом.
        /// </summary>
        /// <param name="file"></param>
        private static void OpenFileInExplorer(FileInfo file)
            => Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n, /select, {file.FullName}" });

        /// <summary>
        /// Юлозилка формы.
        /// </summary>
        /// <param name="control"></param>
        private void MovePositionForm(Control control)
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

        /// <summary>
        /// Обводка формы.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
        }

        #region Тень формы.
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
        #endregion

    }
}
