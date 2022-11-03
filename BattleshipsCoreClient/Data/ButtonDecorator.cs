using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipsCoreClient.Data
{

    public abstract class ButtonDecorator : Button
    {
        Button newBase;
        protected ButtonDecorator(Button basee)

        {
            newBase = basee;
        }
        public abstract void OnClick(EventArgs e);
       
       
    }
}
