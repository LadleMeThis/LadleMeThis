import React from "react";
import IngredientItem from "./IngredientItem";

const IngredientList = ({ ingredients, selectedIngredients, toggleIngredient }) => (
    <div className="ingredient-list">
        {ingredients.map((ingredient) => (
            <IngredientItem
                key={ingredient}
                ingredient={ingredient}
                isSelected={selectedIngredients.includes(ingredient)}
                onClick={toggleIngredient}
            />
        ))}
    </div>
);

export default IngredientList;
