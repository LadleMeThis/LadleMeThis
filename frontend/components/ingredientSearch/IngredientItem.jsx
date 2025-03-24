import React from "react";
import { TbLadle } from "react-icons/tb";

const IngredientItem = ({ ingredient, isSelected, onClick }) => (
    <div
        className={`ingredient-item ${isSelected ? "selected" : ""}`}
        onClick={() => onClick(ingredient)}
    >
        <TbLadle className="ingredient-icon" />
        {ingredient}
    </div>
);

export default IngredientItem;
