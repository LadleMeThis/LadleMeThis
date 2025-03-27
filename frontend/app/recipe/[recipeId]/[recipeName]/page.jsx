"use client";
import React, { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import RecipeImage from "@/components/recipe/recipeImage";
import RecipeInfo from "@/components/recipe/RecipeInfo";
import RecipeCategoryTags from "@/components/recipe/RecipeCategoryTags";
import RecipeIngredients from "@/components/recipe/RecipeIngredients";
import RecipeRatings from "@/components/recipe/RecipeRatings";
import { fetchRecipeById } from "@/scripts/scripts";
import Loader from "@/components/loader/Loader";

const Recipe = () => {
  const [recipe, setRecipe] = useState(null);
  const [loading, setLoading] = useState(true);
  const { recipeId } = useParams();

  useEffect(() => {
    const handleRecipe = async () => {
      const data = await fetchRecipeById(recipeId)
      setRecipe(data)

      setTimeout(() => {
        setLoading(false);
      }, 1500);

    };

    handleRecipe()

    return () => {
      setRecipe(null);
      setLoading(true); 
    };

  }, [recipeId])

  if (loading)
    return <Loader />
  

  return (
    <div className="recipe wrapper">
      <RecipeImage />
      <div className="recipe-header">
        <h1 className="recipe-title">{recipe.name}</h1>
        <RecipeInfo
          prepTime={recipe.prepTime}
          cookTime={recipe.cookTime}
          servingSize={recipe.servingSize}
        />
      </div>

      <RecipeCategoryTags categories={recipe.categories} tags={recipe.tags} />
      <p className="recipe-instructions">{recipe.instructions}</p>
      <RecipeIngredients ingredients={recipe.ingredients} />
      <RecipeRatings ratings={recipe.ratings} />
    </div>
  );
};

export default Recipe;
