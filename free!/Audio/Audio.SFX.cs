﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Threading;

namespace Free
{
    partial class Level
    {
        public void PlaySFX(string toPlay) // i love wonderfully simple code.
        {
            try
            {
                new Thread(() =>
                {
                    MediaPlayer MediaPlayer = new MediaPlayer();
                    MediaPlayer.Open(new Uri(toPlay, UriKind.RelativeOrAbsolute));
                    MediaPlayer.Play();
                }).Start();
            }
            catch (ArgumentException)
            {
                Error.Throw(new Exception("DEBUG: Level.PlaySFX(): Attempted to load an invalid URI while loading a sound effect."), ErrorSeverity.FatalError, "Attempted to load an invalid URI while loading a sound effect.", "avant-gardé engine ver 2.11", 26);
            }
            catch (UriFormatException)
            {
                Error.Throw(new Exception("DEBUG: Level.PlaySFX(): Attempted to load an invalid URI while loading a sound effect."), ErrorSeverity.FatalError, "Attempted to load an invalid URI while loading a sound effect.", "avant-gardé engine ver 2.11", 27);
            }
        }
    }
}
