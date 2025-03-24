"use client";
import React, { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import RecipeImage from "@/components/recipe/recipeImage";
import RecipeInfo from "@/components/recipe/RecipeInfo";
import RecipeCategoryTags from "@/components/recipe/RecipeCategoryTags";
import RecipeIngredients from "@/components/recipe/RecipeIngredients";
import RecipeRatings from "@/components/recipe/RecipeRatings";

const Recipe = () => {
  const [recipe, setRecipe] = useState(null);
  const { recipeId } = useParams();

  useEffect(() => {
    const fetchRecipe = async () => {
      const recipeData = {
        RecipeId: 1,
        Name: "Spaghetti Carbonara",
        Instructions: "Cook pasta, make sauce, combine...",
        PrepTime: 10,
        CookTime: 15,
        ServingSize: 4,
        Categories: [
          { CategoryId: 1, Name: "Italian" },
          { CategoryId: 2, Name: "Pasta" }
        ],
        Tags: [
          { TagId: 1, Name: "Easy" },
          { TagId: 2, Name: "Quick" }
        ],
        Ingredients: [
          { IngredientId: 1, Name: "Spaghetti", Unit: "g" },
          { IngredientId: 2, Name: "Eggs", Unit: "pcs" }
        ],
        Ratings: [
          {
            RatingId: 1,
            Review: "Delicious!",
            Rating: 5,
            DateCreated: new Date().toISOString(),
            User: { UserId: "1", UserName: "UserOne" }
          }
        ]
      };

      setTimeout(() => {
        setRecipe(recipeData);
      }, 1000);
    };

    fetchRecipe();
  }, [recipeId]);

  if (!recipe) {
    return <div>Loading...</div>;
  }

  return (
    <div className="recipe wrapper">
      <RecipeImage />
      <div className="recipe-header">
        <h1 className="recipe-title">{recipe.Name}</h1>
        <RecipeInfo
          prepTime={recipe.PrepTime}
          cookTime={recipe.CookTime}
          servingSize={recipe.ServingSize}
        />
      </div>

      <RecipeCategoryTags categories={recipe.Categories} tags={recipe.Tags} />
      <RecipeIngredients ingredients={recipe.Ingredients} />
      <RecipeRatings ratings={recipe.Ratings} />
    </div>
  );
};

export default Recipe;
