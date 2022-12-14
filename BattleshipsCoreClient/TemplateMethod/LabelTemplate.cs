using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.TemplateMethod
{
    public abstract class LabelTemplate
    {
        public abstract Label LabelAnchor(Label label, AnchorStyles style);
        public abstract Label LabelLocation(Label label, Point point);
        public abstract Label LabelName(Label label, string name);
        public abstract Label LabelSize(Label label, Size size);
        public abstract Label LabelTabIndex(Label label, int tabIndex);
        public abstract Label LabelText(Label label, string text);       

        public Label TemplateMethod(Label label, Point point, Size size, string name, string text, int tabIndex, AnchorStyles style)
        {
            label = LabelAnchor(label, style);
            label = LabelAutoSize(label);
            label = LabelLocation(label, point);
            label = LabelName(label, name);
            label = LabelSize(label, size);
            label = LabelTabIndex(label, tabIndex);
            label = LabelText(label, text);
            return label;
        }

        protected Label LabelAutoSize(Label label)
        {
            label.AutoSize = true;
            return label;
        }
    }
}
