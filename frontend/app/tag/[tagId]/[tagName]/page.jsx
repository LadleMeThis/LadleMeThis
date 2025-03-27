"use client"

import { fetchRecipesByTag } from "@/scripts/scripts";
import RecipeCard from "@/src/components/recipeCard/RecipeCard";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react"
import Loader from "@/components/loader/Loader";

export default function Tag() {
    const [recipes, setRecipes] = useState([]);
    const { tagId } = useParams();
    const [loading, setLoading] = useState(true);


    useEffect(() => {
        const getRecipes = async () => {
            const data = await fetchRecipesByTag(tagId)
            setRecipes(data)

            setTimeout(() => {
                setLoading(false);
              }, 1500);
        }
        getRecipes();

        return () => {
            setRecipes(null);
            setLoading(true); 
          };
    }, [tagId])


    if (loading)
        return <Loader />



    return (
        <>
            <div className="recipe-card-wrapper wrapper">
                {recipes?.map(recipe => <RecipeCard key={recipe.recipeId} recipe={recipe} />)}
            </div>
        </>
    )
}