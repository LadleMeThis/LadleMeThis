"use client"
import CreateRating from "./CreateRating";



const CreateRecipeRating = ({rating, handleRatingChange, review, handleReviewChange, handleSubmit, isDisabled}) => {
    return (
        <div className="create-rating">
            <CreateRating rating={rating} onChange={handleRatingChange} />
            <form className="create-rating-form" onSubmit={handleSubmit}>
                <textarea value={review} onChange={(e) => handleReviewChange(e.target.value)}/>
                <button type="submit" className={isDisabled() ? "disabled" : "" }> Add review</button>
            </form>
            <p className="recipe-review-date">{new Date(Date.now()).toLocaleDateString()}</p>
        </div>
    )
};

export default CreateRecipeRating;
