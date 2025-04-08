export const fetchCategories = async () => {
    const response = await fetch(`/api/categories`);

    if (!response.ok) 
        throw new Error("Something went wrong");
    
    return await response.json();
  };
  