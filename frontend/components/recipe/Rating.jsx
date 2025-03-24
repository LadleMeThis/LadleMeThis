import { FaStar, FaRegStar } from 'react-icons/fa';


const RecipeRating = ({ rating }) => {
    const totalStars = 5;
    const validRating = Math.max(0, Math.min(totalStars, rating));


    if (isNaN(validRating))
        return <div>Invalid Rating</div>;
      
    return (
      <div className="recipe-rating-score">
        {[...Array(validRating)].map((_, index) => (
          <FaStar key={`filled-${index}`} />
        ))}

        {[...Array(totalStars - validRating)].map((_, index) => (
          <FaRegStar key={`empty-${index}`} />
        ))}
      </div>
    );
  };


export default RecipeRating
