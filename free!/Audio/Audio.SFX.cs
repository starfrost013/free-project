using Emerald.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Media;
using System.Threading;

namespace Free
{
    partial class Level
    {
        /// <summary>
        /// Deprecated (2020-02-19); was never used in the first place. We are using NAudio now. 
        /// </summary>
        /// <param name="toPlay"></param>
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
