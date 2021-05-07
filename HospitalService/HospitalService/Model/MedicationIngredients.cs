using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MedicationIngredients
    {
        public string IngredientName { get; set; }

        public MedicationIngredients(string ingredientName)
        {
            IngredientName = ingredientName;
        }

        public MedicationIngredients()
        {
        }
    }
}
