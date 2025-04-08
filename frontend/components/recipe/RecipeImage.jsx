
const RecipeImage = ({recipe}) => (
  <div className="recipe-image">
    <img
      src={recipe.recipeImage.imageUrl}
      alt={recipe.recipeImage.alt}
    />
  </div>
);

export default RecipeImage;
