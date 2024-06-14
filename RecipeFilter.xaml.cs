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

namespace recipeapp
{
    public partial class RecipeFilter : Window
    {
        private List<Recipe> recipes;
        private ListBox recipeListBox;

        public RecipeFilter(List<Recipe> recipes, ListBox recipeListBox)
        {
            InitializeComponent();
            this.recipes = recipes;
            this.recipeListBox = recipeListBox;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (recipes.Count == 0)
            {
                MessageBox.Show("No recipes have been entered yet.", "No Recipes", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string ingredientFilter = ingredientFilterTextBox.Text;
            string foodGroupFilter = null;

            if (foodGroupFilterComboBox.SelectedItem != null)
            {
                foodGroupFilter = ((ComboBoxItem)foodGroupFilterComboBox.SelectedItem).Content.ToString();
            }

            double maxCaloriesFilter;

            if (!double.TryParse(caloriesFilterTextBox.Text, out maxCaloriesFilter))
            {
                maxCaloriesFilter = double.MaxValue;
            }

            var filteredRecipes = recipes.Where(recipe => recipe.MatchesFilters(ingredientFilter, foodGroupFilter, maxCaloriesFilter)).ToList();
            filteredRecipeListBox.ItemsSource = filteredRecipes;
            filteredRecipeListBox.Items.Refresh();
        }



        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            string ingredientFilter = ingredientFilterTextBox.Text;
            string foodGroupFilter = ((ComboBoxItem)foodGroupFilterComboBox.SelectedItem).Content.ToString();
            double maxCaloriesFilter;

            if (!double.TryParse(caloriesFilterTextBox.Text, out maxCaloriesFilter))
            {
                maxCaloriesFilter = double.MaxValue;
            }

            var filteredRecipes = recipes.Where(recipe => recipe.MatchesFilters(ingredientFilter, foodGroupFilter, maxCaloriesFilter)).ToList();
            recipeListBox.ItemsSource = filteredRecipes;
            recipeListBox.Items.Refresh();
            this.Close();
        }
    }
}
