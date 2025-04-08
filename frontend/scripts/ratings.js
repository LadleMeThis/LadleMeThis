export const fetchCreateRating = async (recipeId, recipeRating) => {
    const response = await fetch(`/api/recipe/${recipeId}/rating`, {
      method: "POST",
      headers: { "content-type": "application/json" },
      credentials: "include",
      body: JSON.stringify(recipeRating),
    });

    if (!response.ok) 
        return;
    
    return response.json();
  };
  