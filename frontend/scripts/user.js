export const fetchProfile = async () => {
    const response = await fetch(`/api/user`, {
      method: "GET",
      credentials: "include",
    });

    if (!response.ok) 
        throw new Error("Something went wrong");

    return await response.json();
  };
  
  export const fetchUpdateProfile = async (updatedProfile) => {
    const response = await fetch(`/api/user`, {
      method: "PUT",
      headers: { "content-type": "application/json" },
      credentials: "include",
      body: JSON.stringify(updatedProfile),
    });

    if (!response.ok) 
        throw new Error("Something went wrong");
  };
  