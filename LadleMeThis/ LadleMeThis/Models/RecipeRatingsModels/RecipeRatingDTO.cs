using LadleMeThis.Models.UserModels;

namespace LadleMeThis.Models.RecipeRatingsModels;

public record RecipeRatingDTO(
	int RatingId,
	string Review,
	int Rating,
	DateTime DateCreated,
	UserReviewDTO  User
);