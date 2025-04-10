
const RecipeImage = ({recipe}) => (
  <div className="recipe-image">
    <img
      src={recipe.recipeImage.imageUrl}
      alt={recipe.recipeImage.alt}
    />
    <div className="photo-creator">
        Image was made by : <a href={recipe.recipeImage.photographerUrl}>{recipe.recipeImage.photographerName}</a>
      </div>
  </div>
);

export default RecipeImage;
