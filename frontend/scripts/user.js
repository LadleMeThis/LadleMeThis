export const fetchProfile = async (userId) => {
    const response = await fetch(`/api/user/${userId}`, {
      method: "GET",
      credentials: "include",
    });

    if (!response.ok) 
        throw new Error("Something went wrong");

    return await response.json();
  };
  
  export const fetchUpdateProfile = async (userId, updatedProfile) => {
    const response = await fetch(`/api/user/${userId}`, {
      method: "POST",
      headers: { "content-type": "application/json" },
      credentials: "include",
      body: JSON.stringify(updatedProfile),
    });

    if (!response.ok) 
        throw new Error("Something went wrong");
  };
  