const RecipeIngredients = ({ ingredients }) => (
    <div className="recipe-ingredients">
      <h2 className="recipe-section-title">Ingredients</h2>
      <ul>
        {ingredients.map((ingredient) => (
          <li key={ingredient.IngredientId} className="recipe-ingredient">
            {ingredient.Name} ({ingredient.Unit})
          </li>
        ))}
      </ul>
    </div>
  );
  
  export default RecipeIngredients;
  