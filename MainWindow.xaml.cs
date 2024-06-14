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

namespace recipeapp
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes;

        public MainWindow()
        {
            InitializeComponent();
            recipes = new List<Recipe>();
            recipeListBox.ItemsSource = recipes;
        }

        private void AddRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int numofRecipe = Convert.ToInt32(recipeCountTextBox.Text);

                for (int t = 0; t < numofRecipe; t++)
                {
                    Recipe recipe = new Recipe();

                    recipe.Name = Microsoft.VisualBasic.Interaction.InputBox("Enter the recipe name " + (t + 1));

                    int numofIngredients = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("Enter the number of ingredients for this recipe:"));

                    for (int i = 0; i < numofIngredients; i++)
                    {
                        Ingredient ingredient = new Ingredient();

                        ingredient.Name = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of ingredient" + (i + 1));
                        ingredient.Quantity = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox("Enter the quantity of ingredient " + (i + 1)));
                        ingredient.UnitMeasurement = Microsoft.VisualBasic.Interaction.InputBox("Enter the unit measurement of ingredient " + (i + 1));

                        string foodGroupInput = Microsoft.VisualBasic.Interaction.InputBox("Choose the food group for ingredient " + (i + 1) +
                            "\n1. Fruit and Vegetables" +
                            "\n2. Starchy food" +
                            "\n3. Dairy" +
                            "\n4. Protein" +
                            "\n5. Fat");
                        ingredient.FoodGroup = (FoodGroup)Convert.ToInt32(foodGroupInput);

                        ingredient.Calories = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox("Enter the number of calories for ingredient" + (i + 1)));

                        recipe.Ingredients.Add(ingredient);
                    }

                    int numofSteps = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("Enter the number of steps for this recipe:"));

                    for (int k = 0; k < numofSteps; k++)
                    {
                        string description = Microsoft.VisualBasic.Interaction.InputBox("Enter the description of step " + (k + 1));
                        recipe.Steps.Add(description);
                    }

                    recipes.Add(recipe);
                }

                recipeListBox.Items.Refresh();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input format. Please enter numeric values where required.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (recipeListBox.SelectedItem is Recipe selectedRecipe)
            {
                recipes.Remove(selectedRecipe);
                recipeListBox.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please select a recipe to delete.");
            }
        }

        private void RecipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (recipeListBox.SelectedItem is Recipe selectedRecipe)
            {
                recipeNameTextBlock.Text = selectedRecipe.Name;
                foodGroupTextBlock.Text = selectedRecipe.GetFoodGroupString();
                caloriesTextBlock.Text = selectedRecipe.CalculateTotalCalories().ToString();
                ingredientsListBox.ItemsSource = selectedRecipe.Ingredients;
                stepsListBox.ItemsSource = selectedRecipe.Steps;
            }
        }

        private void RateRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (recipeListBox.SelectedItem is Recipe selectedRecipe)
            {
                if (int.TryParse(ratingTextBox.Text, out int rating) && rating >= 1 && rating <= 5)
                {
                    selectedRecipe.Ratings.Add(rating);
                    MessageBox.Show("Rating added. Average Rating: " + selectedRecipe.GetAverageRating().ToString("0.00"));
                }
                else
                {
                    MessageBox.Show("Please enter a rating between 1 and 5.");
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe to rate.");
            }
        }


        private void OpenFilterWindow(object sender, RoutedEventArgs e)
        {
            RecipeFilter filterWindow = new RecipeFilter(recipes, recipeListBox);
            filterWindow.Show();
        }
    }
}
