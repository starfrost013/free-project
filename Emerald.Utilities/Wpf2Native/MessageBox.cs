﻿using Emerald.Core;
using Emerald.Core.NativeInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Utilities.Wpf2Native
{
    /// <summary>
    /// Emerald Native Interoperability Services
    /// 
    /// MessageBox.cs
    /// 
    /// 2020-02-27
    /// 
    /// Reimplementation based on reference source
    /// and MSDN documentation of the Windows Presentation Foundation MessageBox API,
    /// with some features borrowed from the Windows Forms equivalent
    /// 
    /// Likely to be moved to EmeraldUI when that becomes a thing
    /// </summary>
    public class MessageBox 
    {
#if DEBUG
        public MessageBox()
        {
            ATest();
        }

        /// <summary>
        /// Automated testing
        /// </summary>

        public static void ATest()
        {
            Debug.Assert(Show("a") == MessageBoxResult.OK);
            Debug.Assert(Show("a", "b") == MessageBoxResult.OK);
            Debug.Assert(Show("a", "b", MessageBoxButton.OK) == MessageBoxResult.OK);
            MessageBoxResult R1 = Show("a", "b", MessageBoxButton.OKCancel);
            Debug.Assert(R1 == MessageBoxResult.OK || R1 == MessageBoxResult.Cancel);
            MessageBoxResult R2 = Show("a", "b", MessageBoxButton.RetryCancel);
            Debug.Assert(R2 == MessageBoxResult.Retry || R2 == MessageBoxResult.Cancel);
            MessageBoxResult R3 = Show("a", "b", MessageBoxButton.YesNo);
            Debug.Assert(R3 == MessageBoxResult.Yes || R3 == MessageBoxResult.No);
            MessageBoxResult R4 = Show("a", "b", MessageBoxButton.YesNoCancel);
            Debug.Assert(R4 == MessageBoxResult.Yes || R4 == MessageBoxResult.No || R4 == MessageBoxResult.Cancel);
            MessageBoxResult R5 = Show("a", "b", MessageBoxButton.AbortRetryIgnore);
            Debug.Assert(R5 == MessageBoxResult.Abort || R5 == MessageBoxResult.Retry || R5 == MessageBoxResult.Ignore);
            MessageBoxResult R6 = Show("a", "b", MessageBoxButton.CancelTryContinue);
            Debug.Assert(R6 == MessageBoxResult.Cancel || R6 == MessageBoxResult.TryAgain || R6 == MessageBoxResult.Continue);
            Show("a", "b", MessageBoxButton.OK, MessageBoxImage.None);
            Show("a", "b", MessageBoxButton.OK, MessageBoxImage.Question);
            Show("a", "b", MessageBoxButton.OK, MessageBoxImage.Warning);
            Show("a", "b", MessageBoxButton.OK, MessageBoxImage.Hand);
            Show("a", "b", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Show("a", "b", MessageBoxButton.OK, MessageBoxImage.Error);
            Show("a", "b", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Show("a", "b", MessageBoxButton.OK, MessageBoxImage.Information);
            // don't test for service notif or defaultdesktop for now
            Show("a", "b", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxOptions.RightAlign);
            Show("a", "b", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxOptions.RtlReading);
            //TODO: test hwnd

        }
#endif
        public static MessageBoxResult Show(string Text) => DoShow(Text, "", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxOptions.None); // null for caption?
        public static MessageBoxResult Show(IntPtr WindowHWND, string Text) => DoShow(Text, "", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxOptions.None, WindowHWND); // null for caption?
        public static MessageBoxResult Show(string Text, string Caption) => DoShow(Text, Caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxOptions.None); // null for caption?
        public static MessageBoxResult Show(IntPtr WindowHWND, string Text, string Caption) => DoShow(Text, Caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxOptions.None, WindowHWND); // null for caption?
        public static MessageBoxResult Show(string Text, string Caption, MessageBoxButton Button) => DoShow(Text, Caption, Button, MessageBoxImage.None, MessageBoxOptions.None); // null for caption?
        public static MessageBoxResult Show(IntPtr WindowHWND, string Text, string Caption, MessageBoxButton Button) => DoShow(Text, Caption, Button, MessageBoxImage.None, MessageBoxOptions.None, WindowHWND); // null for caption?
        public static MessageBoxResult Show(string Text, string Caption, MessageBoxButton Button, MessageBoxImage Image) => DoShow(Text, Caption, Button, Image, MessageBoxOptions.None); // null for caption?
        public static MessageBoxResult Show(IntPtr WindowHWND, string Text, string Caption, MessageBoxButton Button, MessageBoxImage Image) => DoShow(Text, Caption, Button, Image, MessageBoxOptions.None, WindowHWND); // null for caption?
        public static MessageBoxResult Show(string Text, string Caption, MessageBoxButton Button, MessageBoxImage Image, MessageBoxOptions Options) => DoShow(Text, Caption, Button, Image, Options); // null for caption?
        public static MessageBoxResult Show(IntPtr WindowHWND, string Text, string Caption, MessageBoxButton Button, MessageBoxImage Image, MessageBoxOptions Options) => DoShow(Text, Caption, Button, Image, Options, WindowHWND); // null for caption?


        private static MessageBoxResult DoShow(string Text, string Caption, MessageBoxButton ButtonSet, MessageBoxImage Image, MessageBoxOptions Options, IntPtr? HWND = null)
        {
            uint ButtonType = (uint)ButtonSet;
            uint ImageType = (uint)Image;
            uint MBOptions = (uint)Options;

            uint FinalOptions = ButtonType + ImageType + MBOptions;

            // IntPtr? to get around bullshit

            if (HWND == null)
            {
                return (MessageBoxResult)Win32Api.MessageBoxA(IntPtr.Zero, Text, Caption, (MessageBoxType)FinalOptions);
            }
            else
            {
                return (MessageBoxResult)Win32Api.MessageBoxA((IntPtr)HWND, Text, Caption, (MessageBoxType)FinalOptions);
            }

            
        }
    }
}
