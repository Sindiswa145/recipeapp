using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipeapp
{
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string UnitMeasurement { get; set; }
        public FoodGroup FoodGroup { get; set; }
        public double Calories { get; set; }
    }

    public enum FoodGroup
    {
        FruitAndVegetables = 1,
        StarchyFood,
        Dairy,
        Protein,
        Fat
    }
}