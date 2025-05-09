"use client"

import { fetchRecipesByTag } from "@/scripts/recipe";
import RecipeCard from "@/components/recipeCard/RecipeCard";
import { useParams, useSearchParams } from "next/navigation";
import { useEffect, useState } from "react"
import Loader from "@/components/loader/Loader";
import RecipeContainerTitle from "@/components/recipeContainerTitle/RecipeContainerTitle";

export default function Tag() {
    const [recipes, setRecipes] = useState([]);
    const { tagId, tagName } = useParams();
    const [loading, setLoading] = useState(true);
    const searchParams = useSearchParams();
    const recipeName = searchParams.get("recipeName");
    const [displayedRecipes, setDisplayedRecipes] = useState([]);


    useEffect(() => {
        const getRecipes = async () => {
            const data = await fetchRecipesByTag(tagId)
            setRecipes(data)
            setDisplayedRecipes(data);

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


    useEffect(() => {
        async function filterRecipes() {
            if (recipeName) {
                setDisplayedRecipes(recipes.filter(recipe =>
                    recipe.name.toLowerCase().includes(recipeName.toLowerCase())
                ));
            } else {
                setDisplayedRecipes(recipes);
            }
        }

        filterRecipes();
    }, [recipeName])



    if (loading)
        return <Loader />



    return (
        <div className="recipe-card-container">
            <RecipeContainerTitle text="Tag:" name={decodeURIComponent(tagName)} />
            <div className="recipe-card-wrapper">
                {displayedRecipes?.map(recipe => <RecipeCard key={recipe.recipeId} recipe={recipe} />)}
            </div>
        </div>
    )
}