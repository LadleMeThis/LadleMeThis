"use client"
import { use, useEffect, useState } from "react";
import RecipeCard from "@/src/components/recipeCard/RecipeCard";
import IngredientSearch from "@/components/ingredientSearch/IngredientSearch";
import { fetchRecipes, login,fetchIngredients, fetchRecipesByIngredients  } from "@/scripts/scripts";
import Loader from "@/components/loader/Loader";





export default function Home() {
  const [recipes, setRecipes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [ingredients, setIngredients] = useState([])
  const [selectedIngredients, setSelectedIngredients] = useState([]);

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

  const searchRecipes = async() => {
    const ids = selectedIngredients.map(i => i.ingredientId)
    const data = await fetchRecipesByIngredients(ids)
    setRecipes(data)

};



  useEffect(() => {
    const getRecipes = async () => {
      const data = await fetchRecipes();
      const finalData = data.slice(0, 5)
      setRecipes(finalData);

      setTimeout(() => {
        setLoading(false);
      }, 1500);
    };

    getRecipes()
    handleIngredients()



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
    <div className="main-container wrapper">
      <IngredientSearch ingredients={ingredients}
      selectedIngredients={selectedIngredients}
      toggleIngredient={toggleIngredient}
      searchRecipes={searchRecipes}/>
      <div>
        <div className="main-title">
          <h1>VERY GOOD VERY NICE </h1>
        </div>
        <div className="recipe-card-wrapper">
          {recipes.map(recipe => <RecipeCard key={recipe.recipeId} recipe={recipe} />)}
        </div>
      </div>
    </div>
  );
}

