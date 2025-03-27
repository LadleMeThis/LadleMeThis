"use client"
import React, { useState, useEffect } from "react";
import IngredientList from "./IngredientList";
import SearchButton from "./IngredientSearchButton";


const IngredientSearch = ({ingredients, selectedIngredients,toggleIngredient,searchRecipes}) => {


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
