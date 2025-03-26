
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
	const data = await response.json();
	return data;
};


export async function fetchRecipesByCategory(categoryId) {
	const response = await fetch(`/api/recipes/category/${categoryId}`, {
		method: "GET",
		headers: {},
	});

	if (!response.ok) {
		console.log(response);
		return;
	}
	const data = await response.json();
	return data;
}


export async function fetchCategories() {
	const response = await fetch(`/api/categories`, {
		method: "GET",
		headers: {},
	});

	if (!response.ok) {
		console.log(response);
		return;
	}
	const data = await response.json();
	return data;
}

export async function fetchTags() {
	const response = await fetch(`/api/tags`, {
		method: "GET",
		headers: {},
	});

	if (!response.ok) {
		console.log(response);
		return;
	}
	const data = await response.json();
	return data;
}

export async function fetchRecipesByTag(tagId) {
	const response = await fetch(`/api/recipes/tag/${tagId}`, {
		method: "GET",
		headers: {},
	});

	if (!response.ok) {
		console.log(response);
		return;
	}
	const data = await response.json();
	return data;
}
