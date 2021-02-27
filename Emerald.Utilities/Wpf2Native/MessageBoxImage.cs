using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Utilities.Wpf2Native
{
    /// <summary>
    /// Icons used in messageboxes.
    /// </summary>
    public enum MessageBoxImage
    {
        /// <summary>
        /// No icon.
        /// </summary>
        None = 0,

        /// <summary>
        /// Same as Error.
        /// </summary>
        Hand = 16,
        
        /// <summary>
        /// An error icon is shown.
        /// </summary>
        Error = 16,

        /// <summary>
        /// A question icon is shown.
        /// </summary>
        Question = 32,

        /// <summary>
        /// Same as warning.
        /// </summary>
        Exclamation = 48,

        /// <summary>
        /// A warning icon is shown.
        /// </summary>
        Warning = 48,

        /// <summary>
        /// Identical to information.
        /// </summary>
        Asterisk = 64,

        /// <summary>
        /// Display an informational icon in the message box.
        /// </summary>
        Information = 64,


    }
}
