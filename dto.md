##INGREDIENTDTO
int IngredientId
string Name

##TAGDTO
int TagId
string Name

##CATEGORYDTO
int CategoryId
string Name

##SHORTRATINGDTO
int Rating

##FULLRATINGDTO
int RatingId
int RecipeId
int UserId
int Rating
string Review
DateTime Date

##MODIFYRATINGDT
int RecipeId
int UserId
int Rating
string Review
DateTime Date

##RECIPECARDDTO
int RecipeId
string Name
int FullTime
int Servings
int Rating
TagDto[] Tags
CategoryDto[] Categories

##FULLRECIPEDTO
int RecipeId
string Name
int PrepTime
int CookTime
string Instructions
IngredientDto[] Ingredients
TagDto[] Tags
CategoryDto[] Categories
Rating[] Ratings

##MODIFYRECIPEDTO
string Name
int PrepTime
int CookTime
string Instructions
Dictionary<int,string> Ingredients
Dictionary<int,string> Tags
Dictionary<int,string> Categories
