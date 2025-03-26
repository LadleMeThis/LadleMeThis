"use client"

import { fetchRecipesByCategory } from "@/scripts/scripts";
import RecipeCard from "@/src/components/recipeCard/RecipeCard";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react"

export default function Recipes() {
    const [recipes, setRecipes] = useState([]);
    const { categoryId } = useParams();




    useEffect(() => {
        const getRecipes = async () => {
            const data = await fetchRecipesByCategory(categoryId)
            setRecipes(data)
        }
        getRecipes();
    }, [categoryId])




    return (
        <>
            <div className="recipe-card-wrapper wrapper">
                {recipes?.map(recipe => <RecipeCard key={recipe.recipeId} recipe={recipe} />)}
            </div>
        </>
    )
}