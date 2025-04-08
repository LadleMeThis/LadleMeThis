
export const login = async (loginData) => {
    const response = await fetch(`/api/login`, {
      method: "POST",
      headers: { "content-type": "application/json" },
      body: JSON.stringify(loginData),
    });

    if (!response.ok)
         throw new Error("Error during login");
  };
  
  export const register = async (registerData) => {
    const response = await fetch(`/api/register`, {
      method: "POST",
      headers: { "content-type": "application/json" },
      body: JSON.stringify(registerData),
    });

    if (!response.ok) 
        throw new Error("Error during registration");
  };
  
  export const logout = async () => {
    const response = await fetch(`/api/logout`, {
      method: "POST",
      headers: { "content-type": "application/json" },
    });

    if (!response.ok) 
        throw new Error("Something went wrong");
  };
  