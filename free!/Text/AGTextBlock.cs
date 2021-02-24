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
/// Modified: 2019-12-07 [Deprecated - 2021-02-23] 
/// 
/// Version: 1.22
/// 
/// Purpose: Defines the AGTextBlock class, allowing text to be displayed in free!.
/// 
/// </summary>

namespace Free
{
    /// <summary>
    /// DEPRECATED
    /// </summary>
    public class AGTextBlock : TextBlock
    {
        public Point GamePos { get; set; }
        public bool IsDisplayed { get; set; }
        public string TextName { get; set; } // internal text name.
    }
}
