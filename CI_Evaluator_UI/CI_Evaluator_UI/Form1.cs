using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CI_Evaluator_UI
{
    public partial class Form1 : Form
    {
        // Import for rounded corners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int left,
            int top,
            int right,
            int bottom,
            int width,
            int height
        );

        private KeyLoggerEngine engine = new KeyLoggerEngine();

        public Form1()
        {
            InitializeComponent();
            ApplyModernUI();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Apply rounded form after layout is ready
            this.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20)
            );
        }

        private void ApplyModernUI()
        {
            // Form settings
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.Padding = new Padding(0);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Header panel
            Panel header = new Panel();
            header.Height = 50;
            header.Dock = DockStyle.Top;
            header.BackColor = Color.FromArgb(45, 45, 45);
            this.Controls.Add(header);

            // Title label
            Label title = new Label();
            title.Text = "Customer Interaction Evaluator";
            title.ForeColor = Color.White;
            title.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(15, (header.Height - 30) / 2);
            header.Controls.Add(title);

            // Close button
            Button btnClose = new Button();
            btnClose.Text = "X";
            btnClose.Width = 40;
            btnClose.Height = 30;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.White;
            btnClose.BackColor = Color.FromArgb(60, 60, 60);
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Location = new Point(this.Width - 50, 10);
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Click += (s, e) => this.Close();
            header.Controls.Add(btnClose);

            // Main panel
            Panel content = new Panel();
            content.Dock = DockStyle.Fill;
            content.BackColor = Color.FromArgb(25, 25, 25);
            this.Controls.Add(content);

            // Start button
            Button btnStart = new Button();
            btnStart.Text = "Start";
            btnStart.Width = 120;
            btnStart.Height = 45;
            btnStart.BackColor = Color.FromArgb(0, 120, 215);
            btnStart.ForeColor = Color.White;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Location = new Point(120, 80);
            btnStart.Click += BtnStart_Click;
            content.Controls.Add(btnStart);

            // Stop button
            Button btnStop = new Button();
            btnStop.Text = "Stop";
            btnStop.Width = 120;
            btnStop.Height = 45;
            btnStop.BackColor = Color.FromArgb(200, 50, 50);
            btnStop.ForeColor = Color.White;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnStop.FlatAppearance.BorderSize = 0;
            btnStop.Location = new Point(270, 80);
            btnStop.Click += BtnStop_Click;
            content.Controls.Add(btnStop);

            // Status label
            lblStatus = new Label();
            lblStatus.Text = "Status: Idle";
            lblStatus.ForeColor = Color.White;
            lblStatus.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(210, 150);
            content.Controls.Add(lblStatus);
        }

        private Label lblStatus;

        private void BtnStart_Click(object sender, EventArgs e)
        {
            engine.Start();
            lblStatus.Text = "Status: Running";

        }

        private async void BtnStop_Click(object sender, EventArgs e)
        {
            engine.Stop();
            lblStatus.Text = "Status: Stopped. Closing in 3 seconds.";

            await Task.Delay(1000);
            this.Close();
        }
    }
}
