using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Data
{
    public class GrassDecorator : ButtonDecorator
    {
        Button newBase;
        public GrassDecorator(Button basee) : base(basee)
        {
            newBase = basee;
            Bitmap bmp = new Bitmap(basee.Width, basee.Height);

            Graphics g = Graphics.FromImage(bmp);

            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(230, 34, 139, 34));

            int x = 8;

            int y = 8;

            int width = basee.Width - 16;

            int height = basee.Height - 16;

            g.FillRectangle(semiTransBrush, x, y, width, height);

            newBase.BackgroundImage = bmp;
        }

        public override void OnClick(EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
