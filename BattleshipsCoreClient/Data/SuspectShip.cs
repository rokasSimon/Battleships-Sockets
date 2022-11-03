using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Data
{
    public class SuspectShip : ButtonDecorator
    {
        Button newBase;
        public SuspectShip(Button basee) : base(basee)
        {
            newBase = basee;
            Bitmap bmp = new Bitmap(20, 20);

            Graphics g = Graphics.FromImage(bmp);

            Brush blackPen = new SolidBrush(Color.Green);

            int x = 20 / 4;

            int y = 20 / 4;

            int width = 20 / 2;

            int height = 20 / 2;

            int diameter = Math.Min(width, height);

            g.FillEllipse(blackPen, x, y, diameter, diameter);

            newBase.Image = bmp;
        }

        public override void OnClick(EventArgs e)
        {
            

            AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
            AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);

            OnClick(e);
        }
    }
}
