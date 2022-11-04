using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Data
{
    public class MissMarkDecorator : ButtonDecorator
    {
        Button newBase;
        public MissMarkDecorator(Button basee) : base(basee)
        {
            newBase = basee;
            
            Bitmap bmp = new Bitmap(basee.Width, basee.Height);
            Graphics g = Graphics.FromImage(bmp);

            SolidBrush water = new SolidBrush(Color.FromArgb(150, 65, 105, 225));

            int x = 8;

            int y = 8;

            int width = basee.Width - 16;

            int height = basee.Height - 16;

            int diameter = Math.Min(width, height);

            g.FillRectangle(water, x, y, width, height);


            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(255, 105, 105, 105));

            int x2 = 12;

            int y2 = 12;

            int width2 = basee.Width - 24;

            int height2 = basee.Height - 24;

            g.FillRectangle(semiTransBrush, x2, y2, width2, height2);

            newBase.Image = bmp;
        }

        public override void OnClick(EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
