export const fetchTags = async () => {
    const response = await fetch(`/api/tags`);
   
    if (!response.ok) 
        throw new Error("Something went wrong");
    
    return await response.json();
  };
  