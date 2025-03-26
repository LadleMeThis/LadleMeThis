import { FaClock, FaTags, FaUtensils, FaStar } from "react-icons/fa";
import { BiCategory } from "react-icons/bi";
import Link from "next/link";

export default function RecipeCard({ recipe }) {
    return (
        <div className="recipe-card">
            <Link href="/specificRecipe" className="clickable-recipe-card">
                <div className="img-container">
                    <img src="/bacon2.jpg" alt="Picture of the current food" />
                </div>
                <div className="recipe-name">
                    <h4>{recipe.name}</h4>
                </div>
                <div className="extra-info">
                    <FaClock /> {recipe.fullTime} min prep  <br />
                    <FaUtensils /> Serves {recipe.servingSize} <br />
                    <FaStar /> {recipe.rating} / 5 <br />
                    <FaTags /> {recipe.tags.join(", ")} <br />
                    <BiCategory /> {recipe.categories.join(", ")}
                </div>
            </Link>
        </div>
    )
}