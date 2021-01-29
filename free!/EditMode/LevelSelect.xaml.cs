using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Free
{
    /// <summary>
    /// Interaction logic for LevelSelect.xaml
    /// </summary>
    public partial class LevelSelect : Window
    {
        public FreeSDL FreeSDL { get; set; }
        public LevelSelect(FreeSDL MnWindow)
        {
            InitializeComponent();
            FreeSDL = MnWindow; // uh oh
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PleaseEnterLevelBox.Text == "") Error.Throw(new Exception(), ErrorSeverity.Warning, "No level ID was entered.", "avant-gardé engine ver 2.5.0/03", 2);

                int levelId = Convert.ToInt32(PleaseEnterLevelBox.Text);

                FreeSDL.LoadNow(levelId);
                this.Close();
                
            }
            catch (FormatException)
            {
                Error.Throw(new Exception(), ErrorSeverity.Warning, "An invalid level ID was entered.", "avant-gardé engine ver 2.5.0/03", 25);
            }
        }
    }
}
