import React from "react";
import IngredientItem from "./IngredientItem";

const IngredientList = ({ ingredients, selectedIngredients, toggleIngredient }) => (
    <div className="ingredient-list">
        {ingredients.map((ingredient) => (
            <IngredientItem
                key={ingredient.key+ingredient.name}
                ingredient={ingredient.name}
                isSelected={selectedIngredients.some(i => i.ingredientId == ingredient.ingredientId)}
                onClick={() => toggleIngredient(ingredient)}
            />
        ))}
    </div>
);

export default IngredientList;
