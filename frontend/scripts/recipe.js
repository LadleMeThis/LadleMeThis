export const fetchRecipeById = async (recipeId) => {
	const response = await fetch(`/api/recipe/${recipeId}`);

	if (!response.ok) throw new Error("Something went wrong");

	return await response.json();
};

export const updateRecipe = async (recipeId, recipeData) => {
	const response = await fetch(`/api/recipe/${recipeId}`, {
		method: "PUT",
		headers: { "content-type": "application/json" },
		body: JSON.stringify(recipeData),
	});

	if (!response.ok) throw new Error("Something went wrong");
};

export const fetchRecipesByIngredients = async (ingredientIds) => {
	const query = ingredientIds.map((id) => `ingredientIds=${id}`).join("&");
	const response = await fetch(`/api/recipes/ingredients?${query}`);

	if (!response.ok) throw new Error("Something went wrong");

	return await response.json();
};

export const fetchRecipesByCategory = async (categoryId, recipeName = "") => {
	const response = await fetch(`/api/recipes/category/${categoryId}?recipeName=${recipeName}`);

	if (!response.ok) throw new Error("Something went wrong");

	return await response.json();
};

export const fetchRecipes = async () => {
	const response = await fetch(`/api/recipes`);

	if (!response.ok) 
        throw new Error("Something went wrong");

	return await response.json();
};

export const fetchCreate = async (recipe) => {
	const response = await fetch(`/api/recipes`, {
		method: "POST",
		headers: { "content-type": "application/json" },
		credentials: "include",
		body: JSON.stringify(recipe),
	});

	if (!response.ok) 
        throw new Error("Something went wrong");
};

export const fetchRecipesByTag = async (tagId) => {
	const response = await fetch(`/api/recipes/tag/${tagId}`);

	if (!response.ok)
        throw new Error("Something went wrong");

	return await response.json();
};

export const fetchRecipesByName = async (recipeName) => {
	const response = await fetch(`/api/recipes/${recipeName}`);

	if (!response.ok) 
        throw new Error("Something went wrong");
	
    return await response.json();
};

export async function getMyRecipes() {
	const response = await fetch(`/api/recipes/my-recipes`);

	if (!response.ok) 
		throw new Error("Something went wrong");	

	return await response.json();
}