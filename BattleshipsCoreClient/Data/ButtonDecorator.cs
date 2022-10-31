using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipsCoreClient.Data
{
    public abstract class ButtonDecorator : ButtonBase
    {
        ButtonBase newBase;
        protected ButtonDecorator(ButtonBase basee)
        {
            newBase = basee;
        }
        public abstract void OnClick(EventArgs e);
       
       
    }
}
