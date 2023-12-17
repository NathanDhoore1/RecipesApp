using Microsoft.EntityFrameworkCore;
using Recipes.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Recipes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseContext dbContext;
        public MainWindow()
        {
            InitializeComponent();
            dbContext = new DatabaseContext();
        }


        public void GrantAccess()
        {
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get values from the text boxes
                string name = txtName.Text;
                string description = txtDescription.Text;

                // Create a new Recipe object
                Recipe newRecipe = new Recipe
                {
                    Name = name,
                    Description = description

                };


                dbContext.Recipes.Add(newRecipe);
                dbContext.SaveChanges();

                MessageBox.Show("Recipe created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating recipe: {ex.Message}");
            }
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                recipeListView.Items.Clear();


                using (var dbContext = new DatabaseContext())
                {

                    var recipes = dbContext.Recipes.ToList();

                    foreach (var recipe in recipes)
                    {
                        recipeListView.Items.Add(recipe);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading recipes: {ex.Message}");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (recipeListView.SelectedItem != null)
                {
                    Recipe selectedRecipe = (Recipe)recipeListView.SelectedItem;

                    selectedRecipe.Name = txtName.Text;
                    selectedRecipe.Description = txtDescription.Text; 

                    using (var dbContext = new DatabaseContext())
                    {
                        dbContext.Recipes.Update(selectedRecipe);
                        dbContext.SaveChanges();
                    }

                    MessageBox.Show("Recipe updated successfully!");
                }
                else
                {
                    MessageBox.Show("Please select a recipe to update.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating recipe: {ex.Message}");
            }

            //refresh list
            Read_Click(sender, e);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (recipeListView.SelectedItem != null)
                {
                    Recipe selectedRecipe = (Recipe)recipeListView.SelectedItem;

                    using (var dbContext = new DatabaseContext())
                    {
                        dbContext.Recipes.Remove(selectedRecipe);
                        dbContext.SaveChanges();
                    }

                    MessageBox.Show("Recipe deleted successfully!");

                    Read_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select a recipe to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting recipe: {ex.Message}");
            }
        }

    }
}