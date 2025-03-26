"use client"

import { fetchRecipesByTag } from "@/scripts/scripts";
import RecipeCard from "@/src/components/recipeCard/RecipeCard";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react"

export default function Tag() {
    const [recipes, setRecipes] = useState([]);
    const { tagId } = useParams();




    useEffect(() => {
        const getRecipes = async () => {
            const data = await fetchRecipesByTag(tagId)
            setRecipes(data)
        }
        getRecipes();
    }, [tagId])




    return (
        <>
            <div className="recipe-card-wrapper wrapper">
                {recipes?.map(recipe => <RecipeCard key={recipe.recipeId} recipe={recipe} />)}
            </div>
        </>
    )
}