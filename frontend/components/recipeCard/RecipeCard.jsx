"use client";
import { FaClock, FaTags, FaUtensils, FaStar } from "react-icons/fa";
import { BiCategory } from "react-icons/bi";
import { useRouter } from "next/navigation";
import { useState, useEffect } from "react";

export default function RecipeCard({ recipe, navigateTo }) {

    const recipeImg = recipe.recipeImage;
    const router = useRouter();

    const getRandomImage = () => {
        const randomPage = Math.floor(Math.random() * 100) + 1;
        return `https://api.pexels.com/v1/search?query=food&page=${randomPage}&per_page=1`;
    };

    function handleClick() {
        if (!navigateTo) {
            router.push(`/recipe/${recipe.recipeId}/${recipe.name}`);
        } else {
            router.push(navigateTo);
        }
    }



    return (
        <div onClick={() => handleClick()} className="recipe-card">
            <div className="img-container">
                <img src={recipeImg.imageUrl || "/bacon2.jpg"} alt={recipeImg.alt} />
            </div>
            <div className="recipe-name">
                <h4>{recipe.name}</h4>
            </div>
            <div className="extra-info">
                <FaClock /> Total Time: {recipe.fullTime} mins <br />
                <FaUtensils /> Serves {recipe.servingSize} <br />
                <FaStar /> {recipe.rating} / 5 <br />
                <FaTags /> {recipe.tags.map(tag => tag.name).join(", ")} <br />
                <BiCategory /> {recipe.categories.map(category => category.name).join(", ")}
            </div>
        </div>
    );
}
