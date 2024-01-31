using System;
using System.Drawing;
using System.Windows.Forms;

namespace Views;
using Game;

public class Field : Form
{
    private Graphics g = null;
    private Bitmap bmp = null;
    private Image img = null;
    Timer tm = new Timer();
    private PictureBox pb = new PictureBox {
        Dock = DockStyle.Fill,
    };

    public Image field = Bitmap.FromFile("./img/fieldtest.png");

    public Field()
    {
        tm.Interval = 10;
        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;
        
        if(Game.Current.CrrConfrontation[0] == Game.Current.CrrTeam)
            new Simulator(Game.Current.CrrTeam, Game.Current.CrrConfrontation[1]);
        else
            new Simulator(Game.Current.CrrConfrontation[0], Game.Current.CrrTeam);

        Controls.Add(pb);

        this.Load += delegate
        {
            bmp = new Bitmap(
                pb.Width, 
                pb.Height
            );
            g = Graphics.FromImage(bmp);
            Draws.Graphics = g;
            pb.Image = bmp;
            tm.Start();
        };

        KeyDown += (o, e) =>
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;

            }
        };

        tm.Tick += delegate
        {
            g.Clear(Color.DarkGreen);

            g.DrawImage(field,0,0,field.Width * 2, field.Height * 2);
            
            pb.Refresh();
        };
    }
}

