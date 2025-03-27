"use client"

import { fetchRecipesByCategory, fetchRecipesByName } from "@/scripts/scripts";
import RecipeCard from "@/src/components/recipeCard/RecipeCard";
import { useParams, useSearchParams } from "next/navigation";
import { useEffect, useState } from "react"
import Loader from "@/components/loader/Loader";

export default function Category() {
    const searchParams = useSearchParams();
    const recipeName = searchParams.get("recipeName");
    const [recipes, setRecipes] = useState([]);
    const [displayedRecipes, setDisplayedRecipes] = useState([]);
    const { categoryId } = useParams();
    const [loading, setLoading] = useState(true);



 
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

            const data = await fetchRecipesByCategory(categoryId)
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
    }, [categoryId, recipeName])



    if (loading)
        return <Loader />

    return (
        <>
            <div className="recipe-card-wrapper wrapper">
                {displayedRecipes?.map(recipe => <RecipeCard key={recipe.recipeId} recipe={recipe} />)}
            </div>
        </>
    )
}