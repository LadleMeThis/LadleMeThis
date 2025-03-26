import RecipeRating from "@/components/recipe/rating";

const RecipeRatings = ({ ratings }) => (
  <div className="recipe-ratings">
    <h2 className="recipe-section-title">Ratings</h2>
    {ratings.map((rating) => (
      <div key={rating.ratingId} className="recipe-rating">
        <div className="recipe-user">{rating.user.userName}</div>
        <RecipeRating rating={rating.rating} />
        <p className="recipe-review">{rating.review}</p>
        <p className="recipe-review-date">{new Date(rating.dateCreated).toLocaleDateString()}</p>
      </div>
    ))}
  </div>
);

export default RecipeRatings;
