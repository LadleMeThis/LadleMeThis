"use client"

import { fetchRecipesByCategory } from "@/scripts/recipe";
import RecipeCard from "@/components/recipeCard/RecipeCard";
import { useParams, useSearchParams } from "next/navigation";
import { useEffect, useState } from "react"
import Loader from "@/components/loader/Loader";

export default function Category() {
    const searchParams = useSearchParams();
    const recipeName = searchParams.get("recipeName");
    const [recipes, setRecipes] = useState([]);
    const [displayedRecipes, setDisplayedRecipes] = useState([]);
    const { categoryId, categoryName } = useParams();
    const [loading, setLoading] = useState(true);




    useEffect(() => {
        const getRecipes = async () => {
            const data = await fetchRecipesByCategory(categoryId)
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
    }, [categoryId])


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
            <div className="recipe-card-container-title">
                <h1>Category:</h1> 
                <h1 className="recipe-card-container-title-name">{decodeURIComponent(categoryName)}</h1>
            </div>
            <div className="recipe-card-wrapper">
                {displayedRecipes?.map(recipe => <RecipeCard key={recipe.recipeId} recipe={recipe} />)}
            </div>
        </div>
    )
}