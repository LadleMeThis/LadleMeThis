"use client"
import { useEffect, useState } from "react";
import RecipeCard from "@/src/components/recipeCard/RecipeCard";
import IngredientSearch from "@/components/ingredientSearch/IngredientSearch";
import { fetchRecipes, login } from "@/scripts/scripts";




export default function Home() {
  const [recipes, setRecipes] = useState([]);




  useEffect(() => {
    const getRecipes = async () => {
      setRecipes(await fetchRecipes());
    };

    getRecipes()



    const loginUser = async () => {
      const loginResponse = await login({ EmailOrUsername: "admin@example.com", Password: "Admin@123" })
      console.log("loginResp", loginResponse)
    }

    loginUser();
  }, [])

  return (
    <>
      <IngredientSearch />
      <div className="main-title wrapper">
        <h1>VERY GOOD VERY NICE TOP 5</h1>
      </div>
      <div className="recipe-card-wrapper wrapper">
        {recipes.map(recipe => <RecipeCard key={recipe.recipeId} recipe={recipe} />)}
      </div>
    </>
  );
}

