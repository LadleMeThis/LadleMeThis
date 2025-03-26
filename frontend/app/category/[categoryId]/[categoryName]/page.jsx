"use client"

import { fetchRecipesByCategory } from "@/scripts/scripts";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react"

export default function Recipes(){
    const [recipes, setRecipes] = useState([]);
      const { recipeId } = useParams();




useEffect(()=>{
     const getRecipes = async () => {
       const data = await fetchRecipesByCategory(categoryId)
       setRecipes(data)
}
},[])









    return(
        <>

        </>
    )
}