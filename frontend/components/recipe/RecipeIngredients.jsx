const RecipeIngredients = ({ ingredients }) => (
    <div className="recipe-ingredients">
      <h2 className="recipe-section-title">Ingredients</h2>
      <ul>
        {ingredients.map((ingredient) => (
          <li key={ingredient.ingredientId} className="recipe-ingredient">
            {ingredient.name} ({ingredient.unit})
          </li>
        ))}
      </ul>
    </div>
  );
  
  export default RecipeIngredients;
  