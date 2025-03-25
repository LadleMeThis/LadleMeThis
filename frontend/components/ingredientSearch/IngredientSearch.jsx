"use client"
import React, { useState, useEffect } from "react";
import IngredientList from "./IngredientList";
import SearchButton from "./IngredientSearchButton";
import { fetchIngredients, fetchRecipesByIngredients } from "@/scripts/scripts";

const IngredientSearch = () => {
    const [ingredients, setIngredients] = useState([])

    useEffect(() => {
        const handleIngredients = async () => {          
          const data = await fetchIngredients()
          setIngredients(data)
        };
    
        handleIngredients()
    
      }, [])

    const [selectedIngredients, setSelectedIngredients] = useState([]);

    const toggleIngredient = (ingredient) => {
        setSelectedIngredients((prev) =>
            prev.includes(ingredient)
                ? prev.filter((item) => item.ingredientId !== ingredient.ingredientId)
                : [...prev, ingredient]
        );
    };

    const searchRecipes = async() => {
        const ids = selectedIngredients.map(i => i.ingredientId)
        const data = await fetchRecipesByIngredients(ids)
        console.log(data);

    };

    return (
        <div className="ingredient-search">
            <h1 className="ingredient-title">What's in Your Fridge?</h1>
            <p className="ingredient-subtitle">
                Select the ingredients you have to find matching recipes.
            </p>
            <IngredientList
                ingredients={ingredients}
                selectedIngredients={selectedIngredients}
                toggleIngredient={toggleIngredient}
            />
            <SearchButton
                onClick={searchRecipes}
                disabled={selectedIngredients.length === 0}
            />
        </div>
    );
};

export default IngredientSearch;
