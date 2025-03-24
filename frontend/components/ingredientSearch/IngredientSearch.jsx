"use client"
import React, { useState } from "react";
import IngredientList from "./IngredientList";
import SearchButton from "./IngredientSearchButton";

const IngredientSearch = () => {
    const dummyIngredients = [
        "Tomato",
        "Cheese",
        "Basil",
        "Chicken",
        "Garlic",
        "Mushroom",
    ];

    const [selectedIngredients, setSelectedIngredients] = useState([]);

    const toggleIngredient = (ingredient) => {
        setSelectedIngredients((prev) =>
            prev.includes(ingredient)
                ? prev.filter((item) => item !== ingredient)
                : [...prev, ingredient]
        );
    };

    const searchRecipes = () => {
        console.log("Searching recipes with ingredients:", selectedIngredients);
        setTimeout(() => {
            alert(`Recipes fetched for: ${selectedIngredients.join(", ")}`);
        }, 1000);
    };

    return (
        <div className="ingredient-search">
            <h1 className="ingredient-title">What's in Your Fridge?</h1>
            <p className="ingredient-subtitle">
                Select the ingredients you have to find matching recipes.
            </p>
            <IngredientList
                ingredients={dummyIngredients}
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
