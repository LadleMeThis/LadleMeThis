namespace LadleMeThis.Models.RecipeRatings;

public record RecipeRatingDto(
	int RatingId,
	string Review,
	int Rating,
	DateTime DateCreated,
	UserDto User
);