using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace CG_Lab1V2
{
    public partial class Form1 : Form
    {

        private Rectangle _rect;
        private Mover _mover;
        private ColorManager _colorManager;
        private Drawer _drawer;

        private LoopingThread _colorManagerThread;
        private LoopingThread _moverThread;

        private Mutex _mutex = new Mutex();

        private bool _run = false;
        public Form1()
        {
            InitializeComponent();

            _rect = new Rectangle(0, 0, 60, 240, 10, Color.AliceBlue, Color.Brown);

            _mover = new Mover(_rect, 1, 45, pictureBox1.Width, pictureBox1.Height);

            _colorManager = new ColorManager(_rect,
                new Color[] { Color.Red, Color.Black, Color.Green, Color.Yellow });

            _drawer = new Drawer(pictureBox1.Width, pictureBox1.Height, Color.White);

            _colorManagerThread = new LoopingThread(_colorManager.changeColor);
            _colorManagerThread._pauseVal = 1000;

            _moverThread = new LoopingThread(() =>
            {
                _mutex.WaitOne();

                _drawer.clear();

                _mover.move();

                _drawer.fillRectangle(_rect);
                _drawer.drawStroke(_rect);

                _mutex.ReleaseMutex();
            });

            _moverThread._pauseVal = 100 / speedBar.Value;

            timer1.Interval = 1000 / (FPSBar.Value * 15);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (!_run)
            {
                timer1.Start();
                button.Text = "Стоп";

                _run = true;

                _colorManagerThread.Start();
                _moverThread.Start();
            }
            else
            {
                timer1.Stop();
                button.Text = "Старт";

                _run = false;

                _colorManagerThread.Pause();
                _moverThread.Pause();
            }
        }

        private void FPSBar_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = 200 / (FPSBar.Value * 3);
        }

        private void stepBar_Scroll(object sender, EventArgs e)
        {
            _mover.setStep(stepBar.Value);
        }

        private void speedBar_Scroll(object sender, EventArgs e)
        {
            _moverThread._pauseVal = 100 / speedBar.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _mutex.WaitOne();
            pictureBox1.Image = _drawer._bitmap;
            _mutex.ReleaseMutex();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            pictureBox1.Height = control.Height;
            pictureBox1.Width = control.Width - panel1.Width;

            if (pictureBox1.Height > 0 && pictureBox1.Width > 0)
            {

                _drawer._bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                _drawer.SetDefault();

                _drawer.fillRectangle(_rect);
                _drawer.drawStroke(_rect);

                pictureBox1.Image = _drawer._bitmap;

                _mover._boxHeight = pictureBox1.Height;
                _mover._boxWidth = pictureBox1.Width;
            }
        }
    }
}
