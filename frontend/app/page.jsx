"use client"
import { useEffect, useState } from "react";
import RecipeCard from "@/src/components/recipeCard/RecipeCard";
import IngredientSearch from "@/components/ingredientSearch/IngredientSearch";
import { fetchRecipes, login } from "@/scripts/scripts";
import Loader from "@/components/loader/Loader";




export default function Home() {
  const [recipes, setRecipes] = useState([]);
  const [loading, setLoading] = useState(true);



  useEffect(() => {
    const getRecipes = async () => {
      setRecipes(await fetchRecipes());

      setTimeout(() => {
        setLoading(false);
      }, 1500);
    };

    getRecipes()



    const loginUser = async () => {
      const loginResponse = await login({ EmailOrUsername: "admin@example.com", Password: "Admin@123" })
      console.log("loginResp", loginResponse)
    }

    loginUser();
    return () => {
      setRecipes(null);
      setLoading(true); 
    };
  }, [])

  if (loading)
    return <Loader />

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

