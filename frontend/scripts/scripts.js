
export const fetchRecipeById = async (recipeId) => {
	const response = await fetch(`/api/recipe/${recipeId}`, {
		method: "GET",
		headers: {},
	});

	if (!response.ok) {
		console.log(response);
		return;
	}
	return await response.json();
};

export const fetchIngredients = async () => {
	const response = await fetch("/api/ingredients", {
		method: "GET",
		headers: {},
	});

	if (!response.ok) {
		console.log(response);
		return;
	}
	return await response.json();
};

export const fetchRecipesByIngredients = async (ingredientIds) => {
	const query = ingredientIds.map((id) => `ingredientIds=${id}`).join("&");

	const response = await fetch(`/api/recipes/ingredients?${query}`, {
		method: "GET",
		headers: {},
	});

	if (!response.ok) {
		console.log(response);
		return;
	}
	return await response.json();
};
