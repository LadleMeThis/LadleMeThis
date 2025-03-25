import RecipeRating from "@/components/recipe/rating";

const RecipeRatings = ({ ratings }) => (
  <div className="recipe-ratings">
    <h2 className="recipe-section-title">Ratings</h2>
    {ratings.map((rating) => (
      <div key={rating.RatingId} className="recipe-rating">
        <div className="recipe-user">{rating.User.UserName}</div>
        <RecipeRating rating={rating.Rating} />
        <p className="recipe-review">{rating.Review}</p>
        <p className="recipe-review-date">{new Date(rating.DateCreated).toLocaleDateString()}</p>
      </div>
    ))}
  </div>
);

export default RecipeRatings;
