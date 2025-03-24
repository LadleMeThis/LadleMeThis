import { FaClock, FaTags, FaUtensils, FaStar } from "react-icons/fa";
import { BiCategory } from "react-icons/bi";

export default function RecipeCard({ recipe }) {
    return (
        <div className="recipe-card">
            <a href="/specificRecipe" className="clickable-recipe-card">
                <div className="img-container">
                    <img src="/bacon2.jpg" alt="Picture of the current food" />
                </div>
                <div className="recipe-name">
                    <h4>{recipe.Name}</h4>
                </div>
                <div className="extra-info">
                    <span>
                    <FaClock /> {recipe.FullTime} min prep  <br />
                    </span>
                    <span>
                    <FaUtensils /> Serves {recipe.ServingSize} <br />
                    </span>
                    <span>
                    <FaStar /> {recipe.Rating} / 5 <br />
                    </span>
                    <span>
                    <FaTags /> {recipe.Tags.join(", ")} <br />
                    </span>
                    <span>
                    <BiCategory /> {recipe.Categories.join(", ")}
                    </span>
                </div>
            </a>
        </div>
    )
}



// RecipeId: 5,
// Name: "Caesar Salad",
// FullTime: 15,
// ServingSize: 2,
// Rating: 4.3,
// Tags: ["Healthy", "Salad", "Quick Meal"],
// Categories: ["Lunch", "Healthy"]




// to display/store on the card:

// int RecipeId,
// string Name,
// int FullTime,
// int ServingSize,
// int Rating,
// Tag[] Tags,
// Category[] Categories



