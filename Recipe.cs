using recipeapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipeapp
{
    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public List<int> Ratings { get; set; }
        public List<string> Comments { get; set; }

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
            Ratings = new List<int>();
            Comments = new List<string>();
        }

        public bool MatchesFilters(string ingredientFilter, string foodGroupFilter, double maxCaloriesFilter)
        {
            // Check if the recipe contains the specified ingredient
            if (!string.IsNullOrEmpty(ingredientFilter))
            {
                if (!Ingredients.Any(i => i.Name.Contains(ingredientFilter, StringComparison.OrdinalIgnoreCase)))
                {
                    return false;
                }
            }

            // Check if the recipe contains ingredients from the specified food group
            if (foodGroupFilter != "All")
            {
                if (!Ingredients.Any(i => i.FoodGroup.ToString() == foodGroupFilter))
                {
                    return false;
                }
            }

            // Check if the total calories of the recipe are within the specified limit
            if (CalculateTotalCalories() > maxCaloriesFilter)
            {
                return false;
            }

            return true;
        }

        public double CalculateTotalCalories()
        {
            return Ingredients.Sum(i => i.Calories);
        }

        public string GetFoodGroupString()
        {
            return string.Join(", ", Ingredients.Select(i => i.FoodGroup.ToString()));
        }

        public double GetAverageRating()
        {
            return Ratings.Any() ? Ratings.Average() : 0;
        }
    }
}