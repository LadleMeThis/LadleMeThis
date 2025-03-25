"use client"
import Link from "next/link";
import { useState } from "react";
import { useRouter } from "next/navigation";
import { FaSearch } from "react-icons/fa";




export default function Navbar() {


    const categories = [
        "Fruits & Vegetables",
        "Meat & Poultry",
        "Seafood",
        "Dairy & Eggs",
        "Grains & Cereals",
        "Snacks & Appetizers"
    ];

    const tags = [
        "Spicy",
        "Sweet",
        "Vegan",
        "Gluten-Free",
        "Low-Carb",
        "Nut-Free"
    ];

    const [user, setUser] = useState(false); // this is just for demonstration

    const [searchQuery, setSearchQuery] = useState("");
    const router = useRouter();

    function handleSearch(e) {
        if (e.key === "Enter" && searchQuery.trim()) {
            router.push(`/recipes?recipename=${encodeURIComponent(searchQuery)}`);
        }
    };

    function login() {
        setUser(true);
    }

    function logout() {
        setUser(false);
    }

    return (
        <nav className="navbar">
            <div className="logo">
                <Link href="/">
                    <img src="laddleME.webp" alt="The site's cool logo" />
                </Link>
            </div>
            <div className="search-bar">
                <input value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)}
                    onKeyDown={handleSearch} placeholder="Search a recipe..." />
                    <FaSearch />
            </div>
            <div className="dropdowns">
                <div className="dropdown">
                    <span className="dropbtn">Categories</span>
                    <div className="dropdown-content">
                        {categories.map(category => <Link key={category} href={`/recipes?categoryname=${encodeURIComponent(category)}`}>{category}</Link>)}
                    </div>
                </div>
                <div className="dropdown">
                    <span className="dropbtn">Tags</span>
                    <div className="dropdown-content">
                        {tags.map(tag => <Link key={tag} href={`/recipes?tagname=${encodeURIComponent(tag)}`}>{tag}</Link>)}

                    </div>
                </div>
            </div>
            <div className="login-logout">
                {user ?
                    <button onClick={logout}>Logout</button>
                    : <button onClick={login} >Login</button>}
            </div>
        </nav>
    );
}




// category + tags  => takes you to the "cards" with the filtered values

// instead of the a tag, use the Link