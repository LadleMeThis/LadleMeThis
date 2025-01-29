
CREATE TABLE "Users" (
	"UserId" int NOT NULL,
	"Username" varchar(255) NOT NULL,
	"Email" varchar(255) NOT NULL UNIQUE,
	"PasswordHash" varchar(255) NOT NULL,
	"DIsplayName" varchar(100) NOT NULL,
	"DateCreated" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY("UserId")
);


CREATE TABLE "Recipes" (
	"RecipeId" int NOT NULL,
	"UserId" int NOT NULL,
	"Name" varchar(255) NOT NULL,
	"Description" text(undefined) NOT NULL,
	"Instructions" text(65535) NOT NULL,
	"PrepTime" int NOT NULL,
	"CookTime" int NOT NULL,
	"Servings" int NOT NULL,
	"DateCreated" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY("RecipeId")
);


CREATE TABLE "Ingredients" (
	"IngredientId" int NOT NULL,
	"Name" varchar(255) NOT NULL,
	"Unit" varchar(255) NOT NULL,
	PRIMARY KEY("IngredientId")
);


CREATE TABLE "RecipeIngredients" (
	"RecipeId" int NOT NULL,
	"IngredientId" int NOT NULL,
	"Quantity" int NOT NULL,
	PRIMARY KEY("RecipeId", "IngredientId")
);


CREATE TABLE "Categories" (
	"CategoryId" int NOT NULL,
	"Name" varchar(255) NOT NULL,
	PRIMARY KEY("CategoryId")
);


CREATE TABLE "RecipeCategories" (
	"RecipeId" int NOT NULL,
	"CategoryId" int NOT NULL,
	PRIMARY KEY("RecipeId", "CategoryId")
);


CREATE TABLE "RecipeRatings" (
	"RatingId" int,
	"RecipeId" int,
	"UserId" int,
	"Rating" int,
	"Review" text(undefined),
	"DateCreated" TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY("RatingId")
);


CREATE TABLE "SavedRecipes" (
	"UserId" int NOT NULL,
	"RecipeId" int NOT NULL,
	"DateSaved" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY("UserId", "RecipeId")
);


CREATE TABLE "Tags" (
	"TagId" int NOT NULL,
	"Name" varchar(255) NOT NULL,
	PRIMARY KEY("TagId")
);


CREATE TABLE "RecipeTags" (
	"RecipeId" int NOT NULL,
	"TagId" int NOT NULL,
	PRIMARY KEY("RecipeId", "TagId")
);


ALTER TABLE "Recipes"
ADD FOREIGN KEY("UserId") REFERENCES "Users"("UserId")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "RecipeIngredients"
ADD FOREIGN KEY("RecipeId") REFERENCES "Recipes"("RecipeId")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "RecipeIngredients"
ADD FOREIGN KEY("IngredientId") REFERENCES "Ingredients"("IngredientId")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "RecipeCategories"
ADD FOREIGN KEY("RecipeId") REFERENCES "Recipes"("RecipeId")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "RecipeCategories"
ADD FOREIGN KEY("CategoryId") REFERENCES "Categories"("CategoryId")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "RecipeRatings"
ADD FOREIGN KEY("RecipeId") REFERENCES "Recipes"("RecipeId")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "RecipeRatings"
ADD FOREIGN KEY("UserId") REFERENCES "Users"("UserId")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "SavedRecipes"
ADD FOREIGN KEY("UserId") REFERENCES "Users"("UserId")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "SavedRecipes"
ADD FOREIGN KEY("RecipeId") REFERENCES "Recipes"("RecipeId")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "RecipeTags"
ADD FOREIGN KEY("RecipeId") REFERENCES "Recipes"("RecipeId")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "RecipeTags"
ADD FOREIGN KEY("TagId") REFERENCES "Tags"("TagId")
ON UPDATE NO ACTION ON DELETE NO ACTION;