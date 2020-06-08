using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

/// <summary>
/// 
/// File Name: Controls.cs
/// 
/// Handles configurable controls in engine version 2.4.0/05 and later.
/// 
/// File Created: 2019-11-17
/// 
/// File Modified: 2019-12-18
/// 
/// Created by: Cosmo
/// 
/// </summary>

namespace Free
{
    public enum LastCtrl { MoveLeft, MoveRight, Jump, Pause, UseSkill, FreeRunGrabLedge, FreeRunJump, Fire, Fire_MouseButton }
    public static class Controls // Static class holding key definitions, they can be set by shit. 
    {
        public static Key Fire { get; set; }
        public static string Fire_MouseButton { get; set; }
        public static Key MoveLeft { get; set; }
        public static Key MoveRight { get; set; }
        public static Key Jump { get; set; }
        public static Key Pause { get; set; }

        public static Key ConvertKey(string Key)
        {
            try
            {
                KeyConverter KeyConverter = new KeyConverter();

                Key ConvertedKey = (Key)KeyConverter.ConvertFromString(Key);
                return ConvertedKey;
            }
            catch (NotSupportedException err)
            {
                MessageBox.Show($"An invalid key was used when Controls.xml was being parsed: \n{err}", "avant-gardé engine ver 2.7.0/04", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(6);
                return new Key();
            }
        }
    }


}

