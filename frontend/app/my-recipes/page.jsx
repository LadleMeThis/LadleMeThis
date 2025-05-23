"use client";
import Loader from "@/components/loader/Loader";
import { getMyRecipes } from "@/scripts/recipe";
import RecipeCard from "@/components/recipeCard/RecipeCard";
import { useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import RecipeContainerTitle from "@/components/recipeContainerTitle/RecipeContainerTitle";


export default function MyRecipes() {
    const searchParams = useSearchParams();
    const recipeName = searchParams.get("recipeName");
    const [myRecipes, setMyRecipes] = useState([]);
    const [displayedRecipes, setDisplayedRecipes] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        try {
            async function fetchMyRecipes() {
                const data = await getMyRecipes();
                setMyRecipes(data);
                setTimeout(() => {
                    setLoading(false);
                }, 1500);
            }
            fetchMyRecipes();
        } catch (e) {
            console.log(e)
        }
    }, [])



    useEffect(() => {
        async function filterRecipes() {
            if (recipeName) {
                setDisplayedRecipes(myRecipes.filter(recipe =>
                    recipe.name.toLowerCase().includes(recipeName.toLowerCase())
                ));
            } else {
                setDisplayedRecipes(myRecipes);
            }
        }

        filterRecipes();
    }, [recipeName, myRecipes])






    if (loading) {
        return <Loader />
    }

    return (
        <div className="recipe-card-container">
           <RecipeContainerTitle text="My" name="Recipes" />
            {
                <div className="recipe-card-wrapper">
                    {displayedRecipes.map(recipe => <RecipeCard navigateTo={`/modify?recipeId=${recipe.recipeId}`} key={recipe.recipeId} recipe={recipe} />)}
                </div>
            }
        </div>
    )
}