"use client"

import { fetchRecipesByCategory, fetchRecipesByName } from "@/scripts/scripts";
import RecipeCard from "@/src/components/recipeCard/RecipeCard";
import { useParams, useSearchParams } from "next/navigation";
import { useEffect, useState } from "react"

export default function Category() {
    const searchParams = useSearchParams();
    const recipeName = searchParams.get("recipeName");
    const [recipes, setRecipes] = useState([]);
    const [displayedRecipes, setDisplayedRecipes] = useState([]);
    const { categoryId } = useParams();



 
    useEffect(() => {


        const getRecipes = async () => {

            if (recipeName) {
                setDisplayedRecipes( recipes.filter(recipe =>
                    recipe.name.toLowerCase().includes(recipeName.toLowerCase())
                ));
            } else {
                const data = await fetchRecipesByCategory(categoryId)
                setRecipes(data)
                setDisplayedRecipes(data)
            }


        }
        getRecipes();

        
    }, [categoryId, recipeName])
    
    
    console.log("recipes", recipes)



    return (
        <>
            <div className="recipe-card-wrapper wrapper">
                {displayedRecipes?.map(recipe => <RecipeCard key={recipe.recipeId} recipe={recipe} />)}
            </div>
        </>
    )
}