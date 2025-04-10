"use client"
import { useEffect, useState } from "react";
import RecipeCard from "@/components/recipeCard/RecipeCard";
import IngredientSearch from "@/components/ingredientSearch/IngredientSearch";
import { fetchRecipes, fetchRecipesByIngredients } from "@/scripts/recipe";
import { fetchIngredients } from "@/scripts/ingredients";
import Loader from "@/components/loader/Loader";

export default function Home() {
  const [recipes, setRecipes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [ingredients, setIngredients] = useState([])
  const [selectedIngredients, setSelectedIngredients] = useState([]);
  const [recipeTitle, setRecipeTitle] = useState(`Top 15 flavors, kissed by the recipe fairies!`)

  const toggleIngredient = (ingredient) => {
    setSelectedIngredients((prev) =>
      prev.includes(ingredient)
        ? prev.filter((item) => item.ingredientId !== ingredient.ingredientId)
        : [...prev, ingredient]
    );
  };

  const handleIngredients = async () => {
    const data = await fetchIngredients()
    setIngredients(data)
  };

  const searchRecipes = async () => {
    const ids = selectedIngredients.map(i => i.ingredientId)
    const data = await fetchRecipesByIngredients(ids)
    setRecipes(data)
    setRecipeTitle("Magic on a plate, made from your fridge’s finest treasures!")

  };

  useEffect(() => {
    const getRecipes = async () => {
      const data = await fetchRecipes();
      const sortedData = data.sort((a, b) => b.rating - a.rating);
      const finalData = sortedData.slice(0, 15)
      setRecipes(finalData);

      setTimeout(() => {
        setLoading(false);
      }, 1500);
    };

    getRecipes()
    handleIngredients()

    return () => {
      setRecipes(null);
      setLoading(true);
    };
  }, [])


  if (loading)
    return <Loader />

  return (
    <div className="main-container wrapper">
      <IngredientSearch ingredients={ingredients}
        selectedIngredients={selectedIngredients}
        toggleIngredient={toggleIngredient}
        searchRecipes={searchRecipes} />
      <div className="main-container-cards">
        <div className="main-title">
          <h1>{recipeTitle}</h1>
        </div>
        <div className="recipe-card-wrapper home">
          {recipes.map(recipe => <RecipeCard key={recipe.recipeId} recipe={recipe} />)}
        </div>
      </div>
    </div>
  );
}

