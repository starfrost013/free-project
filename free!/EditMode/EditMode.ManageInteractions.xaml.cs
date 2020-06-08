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
    /// Interaction logic for EditMode.xaml
    /// </summary>
    public partial class ManageInteractions : Window
    {
        public MainWindow MainWindow;
        public bool EditMode;
        public ManageInteractions(MainWindow MnWindow)
        {
            InitializeComponent();
            MainWindow = MnWindow;
            CurrentInteractionTypeBox.ItemsSource = Enum.GetNames(typeof(InteractionType));
            CurrentInteractionTypeBox.SelectedIndex = 1;
            Interactions.ItemsSource = MainWindow.InteractionList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) // adds a new interaction
        {
            try
            {
                Interaction interaction = new Interaction();
                interaction.OBJ1ID = Convert.ToInt32(CurrentInteractionObj1Box.Text);
                interaction.OBJ2ID = Convert.ToInt32(CurrentInteractionObj2Box.Text);
                interaction.OBJINTERACTIONTYPE = (InteractionType)Enum.Parse(typeof(InteractionType), CurrentInteractionTypeBox.Text);
                MainWindow.InteractionList.Add(interaction);
                Interactions.Items.Refresh();
            }
            catch (FormatException)
            {
                Error.Throw(new Exception("DEBUG: The iser attempted to add an interaction with an invalid object 1 or 2 ID. "), ErrorSeverity.Warning, "Attempted to delete a nonexistent interaction.", "avant-gardé engine ver 2.11.0/03", 18);
            }
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Interactions.SelectedIndex == -1)
            {
                Error.Throw(new Exception("DEBUG: The user attempted to delete a non-existent interaction. "), ErrorSeverity.Warning, "Attempted to delete a nonexistent interaction.", "avant-gardé engine ver 2.11.0/03", 17);
                return;
            }

            for (int i = 0; i < MainWindow.InteractionList.Count; i++)
            {
                Interaction interactioncheck = MainWindow.InteractionList[i];
                if (i == Interactions.SelectedIndex)
                {
                    MainWindow.InteractionList.Remove(interactioncheck);
                    Interactions.Items.Refresh();
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            switch (EditMode) {
                case false:
                    EditMode = true;
                    EditButton.Content = "Edit ←";

                    if (Interactions.SelectedIndex == -1)
                    {
                        Error.Throw(new Exception("DEBUG: The user attempted to edit a non-existent interaction. "), ErrorSeverity.Warning, "Attempted to edit a nonexistent interaction.", "avant-gardé engine ver 2.11.0/03", 19);
                    }

                    for (int i = 0; i < MainWindow.InteractionList.Count; i++)
                    {
                        Interaction interaction = MainWindow.InteractionList[i];

                        if (i == Interactions.SelectedIndex)
                        {
                            CurrentInteractionObj1Box.Text = interaction.OBJ1ID.ToString();
                            CurrentInteractionObj2Box.Text = interaction.OBJ2ID.ToString();
                            CurrentInteractionTypeBox.SelectedItem = interaction.OBJINTERACTIONTYPE;
                        }
                    }

                    return;
                case true:
                    EditMode = false;
                    EditButton.Content = "Edit →";
                    if (Interactions.SelectedIndex == -1)
                    {
                        Error.Throw(new Exception("DEBUG: The user attempted to edit a non-existent interaction. "), ErrorSeverity.Warning, "Attempted to edit a nonexistent interaction.", "avant-gardé engine ver 2.11.0/03", 20);
                    }

                    for (int i = 0; i < MainWindow.InteractionList.Count; i++)
                    {
                        Interaction interaction = MainWindow.InteractionList[i];

                        if (i == Interactions.SelectedIndex)
                        {
                            interaction.OBJ1ID = Convert.ToInt32(CurrentInteractionObj1Box.Text);
                            interaction.OBJ2ID = Convert.ToInt32(CurrentInteractionObj2Box.Text);
                            interaction.OBJINTERACTIONTYPE = (InteractionType)Enum.Parse(typeof(InteractionType), CurrentInteractionTypeBox.Text);
                            MainWindow.InteractionList[i] = interaction;
                        }
                    }
                    Interactions.Items.Refresh();
                    return;
            }
        }
    }
}
