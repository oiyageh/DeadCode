using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MixingStation : MonoBehaviour
{
    public List<string> requiredIngredients = new List<string> { "Vodka", "Soda" };
    public List<string> placedIngredients = new List<string>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ingredient"))
        {
            Debug.Log("Holy shit its my dad");
            Ingredient ingredient = other.GetComponent<Ingredient>();

            if (ingredient != null)
            {
                placedIngredients.Add(other.name);
                ingredient.inCookingStation = true;
                Debug.Log($"Added {other.name} to cooking station.");
                CheckRecipe();
            }
        }
    }

    void CheckRecipe()
    {
        bool hasAllIngredients = true;
        foreach (string ingredient in requiredIngredients)
        {
            if (!placedIngredients.Contains(ingredient))
            {
                hasAllIngredients = false;
                break;
            }
        }

        if (hasAllIngredients)
        {
            Debug.Log("Recipe complete! Dish is ready.");
            // Trigger success effect (e.g., particle effect, sound, or UI feedback)
        }
    }
}