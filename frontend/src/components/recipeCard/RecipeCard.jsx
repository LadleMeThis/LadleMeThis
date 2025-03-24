import "./../../../styles/recipeCard.scss";

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
                Preparation time: {recipe.FullTime} mins <br />
                Serving size : {recipe.ServingSize} ppl <br />
                Rating : {recipe.Rating} / 5 <br />
                Tags : {recipe.Tags.join(", ")} <br /> 
                Categories : {recipe.Categories.join(", ")}
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



