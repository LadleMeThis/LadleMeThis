export const fetchIngredients = async () => {
	const response = await fetch("/api/ingredients");

	if (!response.ok)
		throw new Error("Something went wrong");	
	
	return await response.json();
};