namespace WordParser.Core.Forms;

internal partial class WaitForm : Form
{
    public WaitForm()
    {
        InitializeComponent();
        RegisterFormEvents();
    }

    private void RegisterFormEvents()
    {
        ActionOnEventsToLoadAndCloseForm();
    }

    private void ActionOnEventsToLoadAndCloseForm()
    {
        this.Load += (s, e) => this.Location = new Point(
            Owner.Location.X + Owner.Width / 2 - this.Width / 2,
            Owner.Location.Y + Owner.Height / 2 - this.Height / 2);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
    }
}