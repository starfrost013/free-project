using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// 
/// AGTextBlock.cs
/// 
/// Created: 2019-12-07
/// 
/// Modified: 2019-12-07
/// 
/// Version: 1.21
/// 
/// Purpose: Defines the AGTextBlcok class, allowing text to be displayed in free!.
/// 
/// </summary>

namespace Free
{
    public class AGTextBlock : TextBlock
    {
        public Point GamePos { get; set; }
        public bool IsDisplayed { get; set; }
        public string TextName { get; set; } // internal text name.
    }
}
