import RecipeCard from "@/src/components/recipeCard/RecipeCard";


const recipes = [
  {
    RecipeId: 1,
    Name: "Spaghetti Carbonara",
    FullTime: 30,
    ServingSize: 2,
    Rating: 4.8,
    Tags: ["Italian", "Pasta", "Quick Meal"],
    Categories: ["Dinner", "Comfort Food"]
  },
  {
    RecipeId: 2,
    Name: "Chicken Tikka Masala",
    FullTime: 45,
    ServingSize: 4,
    Rating: 4.7,
    Tags: ["Indian", "Spicy", "Curry"],
    Categories: ["Dinner", "Spicy Food"]
  },
  {
    RecipeId: 3,
    Name: "Avocado Toast",
    FullTime: 10,
    ServingSize: 1,
    Rating: 4.5,
    Tags: ["Breakfast", "Healthy", "Vegetarian"],
    Categories: ["Breakfast", "Quick Meal"]
  },
  {
    RecipeId: 4,
    Name: "Beef Stroganoff",
    FullTime: 50,
    ServingSize: 4,
    Rating: 4.6,
    Tags: ["Russian", "Beef", "Creamy"],
    Categories: ["Dinner", "Comfort Food"]
  },
  {
    RecipeId: 5,
    Name: "Caesar Salad",
    FullTime: 15,
    ServingSize: 2,
    Rating: 4.3,
    Tags: ["Healthy", "Salad", "Quick Meal"],
    Categories: ["Lunch", "Healthy"]
  }
];



export default function Home() {
  return (
    <div className="recipe-card-wrapper">
        {/* {recipes.map(recipe => <RecipeCard key={recipe.RecipeId} recipe={recipe} />)} */}
    </div>
  );
}

