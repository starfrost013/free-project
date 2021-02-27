using Emerald.Core.NativeInterop;
using System;
using System.Collections.Generic;
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
    public static class MessageBox
    {

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
            return (MessageBoxResult)Win32Api.MessageBoxW((IntPtr)HWND, Text, Caption, (MessageBoxType)FinalOptions);
        }
    }
}
