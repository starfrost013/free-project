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

/// <summary>
/// 
/// File name: Audio.cs
/// 
/// Created: 2019-11-21
/// 
/// Modified: 2019-11-21
/// 
/// Version: 1.00
/// 
/// Free Version: 0.08+ (Engine ver 2.8.0/01+)
/// 
/// Purpose: Plays and/or loops level music and sound effects.
/// 
/// </summary>

namespace Free
{
    public partial class Level
    {
        public void LoadLevelMusic()
        {
            MUSICPLAYER = new MediaPlayer();

            if (MUSICPATH != null)
            {
                MUSICPLAYER.Open(new Uri(MUSICPATH, UriKind.RelativeOrAbsolute));
                MUSICPLAYER.MediaEnded += LoopMusic;
                MUSICPLAYER.Play();
                
            }

        }

        public void LoopMusic(IGameObject sender, EventArgs e) // loops the music
        {
            MUSICPLAYER.Position = TimeSpan.Zero;
            MUSICPLAYER.Play();
        }
    }
}
