using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.TemplateMethod
{
    internal class AdvancedLabel : LabelTemplate
    {
        public override Label LabelAnchor(Label label, AnchorStyles style)
        {           
            label.Anchor = style;
            return label;
        }

        public override Label LabelLocation(Label label, Point point)
        {
            label.Location = point;
            return label;
        }

        public override Label LabelName(Label label, string name)
        {
            label.Name = name;
            return label;
        }

        public override Label LabelSize(Label label, Size size)
        {
            label.Size = size;
            return label;
        }

        public override Label LabelTabIndex(Label label, int tabIndex)
        {
            label.TabIndex = tabIndex;
            return label;
        }

        public override Label LabelText(Label label, string text)
        {
            label.Text = text;
            return label;
        }
    }
}
