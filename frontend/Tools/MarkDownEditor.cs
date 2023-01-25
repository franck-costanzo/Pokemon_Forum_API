using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smogon_MAUIapp.Tools
{
    public class MarkDownEditor : Editor
    {
        public static readonly BindableProperty SelectionStartProperty = 
            BindableProperty.Create(nameof(SelectionStart), typeof(int), typeof(MarkDownEditor), default(int), BindingMode.TwoWay);
        public int SelectionStart
        {
            get { return (int)GetValue(SelectionStartProperty); }
            set { SetValue(SelectionStartProperty, value); }
        }

        public static readonly BindableProperty SelectionEndProperty = 
            BindableProperty.Create(nameof(SelectionEnd), typeof(int), typeof(MarkDownEditor), default(int), BindingMode.TwoWay);
        public int SelectionEnd
        {
            get { return (int)GetValue(SelectionEndProperty); }
            set { SetValue(SelectionEndProperty, value); }
        }
    }
}
