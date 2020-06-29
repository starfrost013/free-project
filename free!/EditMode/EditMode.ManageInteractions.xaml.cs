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
        public FreeSDL FreeSDL;
        public bool EditMode;
        public ManageInteractions(FreeSDL MnWindow)
        {
            InitializeComponent();
            FreeSDL = MnWindow;
            CurrentInteractionTypeBox.ItemsSource = Enum.GetNames(typeof(InteractionType));
            CurrentInteractionTypeBox.SelectedIndex = 1;
            Interactions.ItemsSource = FreeSDL.InteractionList;
        }

        private void AddButton_Click(IGameObject sender, RoutedEventArgs e) // adds a new interaction
        {
            try
            {
                Interaction interaction = new Interaction();
                interaction.GameObject1ID = Convert.ToInt32(CurrentInteractionGameObject1Box.Text);
                interaction.GameObject2ID = Convert.ToInt32(CurrentInteractionGameObject2Box.Text);
                interaction.GameObjectINTERACTIONTYPE = (InteractionType)Enum.Parse(typeof(InteractionType), CurrentInteractionTypeBox.Text);
                FreeSDL.InteractionList.Add(interaction);
                Interactions.Items.Refresh();
            }
            catch (FormatException)
            {
                Error.Throw(new Exception("DEBUG: The iser attempted to add an interaction with an invalid IGameObject 1 or 2 ID. "), ErrorSeverity.Warning, "Attempted to delete a nonexistent interaction.", "avant-gardé engine ver 2.11.0/03", 18);
            }
        }

        private void DoneButton_Click(IGameObject sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(IGameObject sender, RoutedEventArgs e)
        {
            if (Interactions.SelectedIndex == -1)
            {
                Error.Throw(new Exception("DEBUG: The user attempted to delete a non-existent interaction. "), ErrorSeverity.Warning, "Attempted to delete a nonexistent interaction.", "avant-gardé engine ver 2.11.0/03", 17);
                return;
            }

            for (int i = 0; i < FreeSDL.InteractionList.Count; i++)
            {
                Interaction interactioncheck = FreeSDL.InteractionList[i];
                if (i == Interactions.SelectedIndex)
                {
                    FreeSDL.InteractionList.Remove(interactioncheck);
                    Interactions.Items.Refresh();
                }
            }
        }

        private void EditButton_Click(IGameObject sender, RoutedEventArgs e)
        {
            switch (EditMode) {
                case false:
                    EditMode = true;
                    EditButton.Content = "Edit ←";

                    if (Interactions.SelectedIndex == -1)
                    {
                        Error.Throw(new Exception("DEBUG: The user attempted to edit a non-existent interaction. "), ErrorSeverity.Warning, "Attempted to edit a nonexistent interaction.", "avant-gardé engine ver 2.11.0/03", 19);
                    }

                    for (int i = 0; i < FreeSDL.InteractionList.Count; i++)
                    {
                        Interaction interaction = FreeSDL.InteractionList[i];

                        if (i == Interactions.SelectedIndex)
                        {
                            CurrentInteractionGameObject1Box.Text = interaction.GameObject1ID.ToString();
                            CurrentInteractionGameObject2Box.Text = interaction.GameObject2ID.ToString();
                            CurrentInteractionTypeBox.SelectedItem = interaction.GameObjectINTERACTIONTYPE;
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

                    for (int i = 0; i < FreeSDL.InteractionList.Count; i++)
                    {
                        Interaction interaction = FreeSDL.InteractionList[i];

                        if (i == Interactions.SelectedIndex)
                        {
                            interaction.GameObject1ID = Convert.ToInt32(CurrentInteractionGameObject1Box.Text);
                            interaction.GameObject2ID = Convert.ToInt32(CurrentInteractionGameObject2Box.Text);
                            interaction.GameObjectINTERACTIONTYPE = (InteractionType)Enum.Parse(typeof(InteractionType), CurrentInteractionTypeBox.Text);
                            FreeSDL.InteractionList[i] = interaction;
                        }
                    }
                    Interactions.Items.Refresh();
                    return;
            }
        }
    }
}
