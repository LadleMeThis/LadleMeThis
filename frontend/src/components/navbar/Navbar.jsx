export default function Navbar() {


const userLoggedIn = false; // this is just for demonstration

    return (
        <nav className="navbar">
            <div className="logo">
                <a href="/">
                    <img src="laddleME.webp" alt="The site's cool logo" />
                </a>
            </div>
            <div className="search-bar">
                <input type="text" placeholder="Search a recipe..." />
            </div>
            <div className="login">
                <button>Login</button>
            </div>
        </nav>
    );
}


// category + tags  => takes you to the "cards" with the filtered values

// instead of the a tag, use the Link