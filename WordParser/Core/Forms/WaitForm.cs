using System.Drawing;
using System.Windows.Forms;

namespace WordParser.Core.Forms
{
    internal partial class WaitForm : Form
    {
        internal WaitForm()
        {
            InitializeComponent();
            HandleEventsForm();
        }

        /// <summary>
        /// Обработка событий формы.
        /// </summary>
        private void HandleEventsForm()
        {
            this.Load += (s, e)
                => this.Location = new Point(Owner.Location.X + Owner.Width / 2 - this.Width / 2, Owner.Location.Y + Owner.Height / 2 - this.Height / 2);
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
