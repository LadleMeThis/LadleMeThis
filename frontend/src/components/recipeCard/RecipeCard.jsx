"use client";
import { FaClock, FaTags, FaUtensils, FaStar } from "react-icons/fa";
import { BiCategory } from "react-icons/bi";
import { useRouter } from "next/navigation";
import { useState, useEffect } from "react";

export default function RecipeCard({ recipe, navigateTo }) {
    const [imageUrl, setImageUrl] = useState(null);
    const router = useRouter();



    function handleClick() {
        if (!navigateTo) {
            router.push(`/recipe/${recipe.recipeId}/${recipe.name}`)
        } else {
            router.push(navigateTo)
        }
    }

    useEffect(() => {
        fetch("https://api.pexels.com/v1/search?query=food&per_page=1", {
            headers: {
                Authorization: apiKey
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data.photos && data.photos.length > 0) {
                setImageUrl(data.photos[0].src.original);
            }
        })
        .catch(error => {
            console.error("Error fetching food image:", error);
            setImageUrl("/bacon2.jpg");
        });
    }, []);


    return (
        <div onClick={() => handleClick()} className="recipe-card">
            <div className="img-container">
                <img src={imageUrl} alt="Picture of the current food" />
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
    )
}