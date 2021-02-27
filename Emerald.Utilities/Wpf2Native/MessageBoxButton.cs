﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Utilities.Wpf2Native
{
    /// <summary>
    /// The buttons on a MessageBox.
    /// </summary>
    public enum MessageBoxButton
    {
        /// <summary>
        /// OK button.
        /// </summary>
        OK = 0,
        
        /// <summary>
        /// OK/Cancel buttons.
        /// </summary>
        OKCancel = 1,

        /// <summary>
        /// Abort/Retry/Ignore buttons (NOT IN .NET IMPL)
        /// </summary>
        AbortRetryIgnore = 2,

        /// <summary>
        /// Yes/No/Cancel buttons
        /// </summary>
        YesNoCancel = 3,

        /// <summary>
        /// Yes/No buttons
        /// </summary>
        YesNo = 4,

        /// <summary>
        /// Retry/Cancel buttons (NOT IN .NET IMPL)
        /// </summary>
        RetryCancel = 5,

        /// <summary>
        /// Cancel/Try/Continue buttons (NOT IN .NET IMPL)
        /// </summary>
        CancelTryContinue = 6,

        
    }
}
