"use client"
import { useEffect } from "react";
import RecipeCard from "@/src/components/recipeCard/RecipeCard";
import IngredientSearch from "@/components/ingredientSearch/IngredientSearch";


const recipes = [
  {
    RecipeId: 1,
    Name: "Spaghetti Carbonara",
    FullTime: 30,
    ServingSize: 2,
    Rating: 4.8,
    Tags: ["Italian", "Pasta", "Quick Meal"],
    Categories: ["Dinner", "Comfort Food"]
  },
  {
    RecipeId: 2,
    Name: "Chicken Tikka Masala",
    FullTime: 45,
    ServingSize: 4,
    Rating: 4.7,
    Tags: ["Indian", "Spicy", "Curry"],
    Categories: ["Dinner", "Spicy Food"]
  },
  {
    RecipeId: 3,
    Name: "Avocado Toast",
    FullTime: 10,
    ServingSize: 1,
    Rating: 4.5,
    Tags: ["Breakfast", "Healthy", "Vegetarian"],
    Categories: ["Breakfast", "Quick Meal"]
  },
  {
    RecipeId: 4,
    Name: "Beef Stroganoff",
    FullTime: 50,
    ServingSize: 4,
    Rating: 4.6,
    Tags: ["Russian", "Beef", "Creamy"],
    Categories: ["Dinner", "Comfort Food"]
  },
  {
    RecipeId: 5,
    Name: "Caesar Salad",
    FullTime: 15,
    ServingSize: 2,
    Rating: 4.3,
    Tags: ["Healthy", "Salad", "Quick Meal"],
    Categories: ["Lunch", "Healthy"]
  }
];

export default function Home() {


  useEffect(() => {
    const test = async () => {
      const response = await fetch(
        "/api/categories",
        {
          method: "GET",
          headers: {},
        }
      );
      if (!response.ok) {
        console.log(response);
        return;
      }

      console.log(await response.json());
    };

    test()

  }, [])

  return (
    <>
      <IngredientSearch />
      <div className="main-title wrapper">
        <h1>VERY GOOD VERY NICE TOP 5</h1>
      </div>
      <div className="recipe-card-wrapper wrapper">
        {recipes.map(recipe => <RecipeCard key={recipe.RecipeId} recipe={recipe} />)}
      </div>
    </>
  );
}

