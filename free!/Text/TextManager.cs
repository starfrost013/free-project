using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

/// <summary>
/// 
/// TextManager.cs
/// 
/// Created: 2019-12-04
/// 
/// Modified: 2019-12-07
/// 
/// Version: 2.35
/// 
/// Purpose: Handles everything related to text.
/// 
/// </summary>

namespace Free
{
    public partial class FreeSDL
    {
        public AGTextBlock LoadText(string Text, double x, double y)
        {
            AGTextBlock textBlock = new AGTextBlock();
            textBlock.DataContext = this;
            textBlock.Foreground = new SolidColorBrush(new Color { R = 0, G = 0, B = 0, A = 255});
            textBlock.GamePos = new Point(x, y);
            textBlock.Text = Text;
            textBlock.IsDisplayed = false; // for per-level layouts

            if (x != -1 & y != -1)
            {
                TextList.Add(textBlock);
            }

            Debug.WriteLine("The Text Manager is deprecated."); 

            return textBlock;
        }

        public void DeleteText(AGTextBlock textblock)
        {
            foreach (AGTextBlock Text in TextList)
            {
                if (Text == textblock)
                {
                    TextList.Remove(Text);
                    UpdateLayout();
                    return;
                }
            }
        }
        public void DeleteText(string Text)
        {
            foreach (AGTextBlock text in TextList)
            {
                if (text.Text == Text)
                {
                    TextList.Remove(text);
                    UpdateLayout();
                    return;
                }
            }
        }

        public AGTextBlock GetText(string TextInternalName)
        {
            foreach (AGTextBlock text in TextList)
            {
                if (text.TextName == TextInternalName)
                {
                    return text;
                }
            }
            return null; //throw warning?
        }

        public void MoveText(AGTextBlock TextBlock, double x, double y) // move text using agtextblock
        {
            TextBlock.GamePos = new Point(x, y);
            return;
        }

        public void MoveText(string Text, double x, double y) // move text
        {
            foreach (AGTextBlock TextBlock in TextList)
            {
                if (TextBlock.Text == Text)
                {
                    TextBlock.GamePos = new Point(x, y);
                    return;
                }
            }
        }

        public void SetText(AGTextBlock Text, string text) // set text
        {
            Text.Text = text;
        }

        public void SetText(string TextBlock, string text) // set text
        {
            foreach (AGTextBlock TxtBlock in TextList)
            {
                if (TxtBlock.Text == TextBlock)
                {
                    TxtBlock.Text = text;
                    return;
                }
            }
        }

        public void SetTextColour(AGTextBlock Text, Color Colour) // set text colour
        {
            Text.Foreground = new SolidColorBrush(Colour);
        }

        public void SetTextColour(string Text, Color Colour) // set text colour
        {
            foreach (AGTextBlock TextBlock in TextList)
            {
                if (TextBlock.Text == Text)
                {
                    TextBlock.Foreground = new SolidColorBrush(Colour);
                    return;
                }
            }
        }

        public void SetTextColourBg(AGTextBlock Text, Color BGColour) // set text bg colour
        {
            Text.Background = new SolidColorBrush(BGColour);
        }
        public void SetTextColourBg(string Text, Color BGColour) // set text bg colour
        {
            foreach (AGTextBlock TextBlock in TextList)
            {
                if (TextBlock.Text == Text)
                {
                    TextBlock.Background = new SolidColorBrush(BGColour);
                    return;
                }
            }
        }

        public void SetTextFont(AGTextBlock Text, FontFamily FontFamily) // set font
        {
            Text.FontFamily = FontFamily;
        }

        public void SetTextFont(string Text, FontFamily FontFamily) // set font
        {
            foreach (AGTextBlock TextBlock in TextList)
            {
                if (TextBlock.Text == Text)
                {
                    TextBlock.FontFamily = FontFamily;
                    return;
                }
            }
        }

        public void SetTextFontSize(AGTextBlock Text, int Size) // set text font size
        {
            Text.FontSize = Size;
        }

        public void SetTextFontSize(string Text, int Size)
        {
            foreach (AGTextBlock TextBlock in TextList)
            {
                if (TextBlock.Text == Text)
                {
                    TextBlock.FontSize = Size;
                    return;
                }
            }
        }

        public void SetTextInternalName(AGTextBlock Text, string InternalName) // set text internal name
        {
            Text.TextName = InternalName;
            return; 
        }

        public void SetTextFontSize(string Text, string InternalName) // set text internal name
        {
            foreach (AGTextBlock TextBlock in TextList)
            {
                if (TextBlock.Text == Text)
                {
                    TextBlock.TextName = InternalName;
                    return;
                }
            }
        }

        public void SetTextStyle(AGTextBlock Text, FontStyle FontStyle) // set text style
        {
            Text.FontStyle = FontStyle;
        }
        
        public void SetTextStyle(string Text, FontStyle FontStyle)
        {
            foreach (AGTextBlock TextBlock in TextList)
            {
                if (TextBlock.Text == Text)
                {
                    TextBlock.FontStyle = FontStyle;
                    return;
                }
            }
        }

        public void SetTextVisibility(AGTextBlock Text, bool Visibility) // yeah
        {
            Text.IsDisplayed = Visibility;
        }

        public void SetTextVisibility(string Text, bool Visibility) // set visibility
        {
            foreach (AGTextBlock TextBlock in TextList)
            {
                if (TextBlock.Text == Text)
                {
                    TextBlock.IsDisplayed = Visibility;
                    return;
                }
            }
        }
    }
}
