"use client"
import Link from "next/link";
import { useEffect, useState } from "react";
import { usePathname, useRouter, useSearchParams } from "next/navigation";
import { FaSearch, FaBars, FaTimes } from "react-icons/fa";
import { fetchCategories, fetchTags } from "@/scripts/scripts";
import LoginRegisterModal from "@/components/loginRegisterModal/loginRegisterModal";



// search bar should work differently, depending on the current path

// on any page that displays recipes should work as a filter

// on any other page should work as a simple search


export default function Navbar() {
    const [categories, setCategories] = useState([]);
    const [tags, setTags] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const openModal = () => setIsModalOpen(true);
    const closeModal = () => setIsModalOpen(false);
    const [user, setUser] = useState(true); // this is just for demonstration

    const [searchQuery, setSearchQuery] = useState("");
    const router = useRouter();
    const pathname = usePathname();
    const recipeDisplayPaths = ["/category", "/tag"] // pages where search bar should function as a filter
    const [menuOpen, setMenuOpen] = useState(false);
    const [isAnimating, setIsAnimating] = useState(false);

    //home btn, create, modify, my recipes
    //after pressing a btn, dont forget to close the hamburger menu



    const toggleMenu = () => {
        if (menuOpen) {
            setIsAnimating(true);
            setTimeout(() => {
                setMenuOpen(false);
                setIsAnimating(false);
            }, 300); // Match transition duration
        } else {
            setMenuOpen(true);
        }
    };

    const closeMenu = () => {
        setMenuOpen(false);
        setIsAnimating(false);
    };


    useEffect(() => {
        const getCatgories = async () => {
            const data = await fetchCategories()
            setCategories(data)
            console.log(categories)
        }
        getCatgories();


        const getTags = async () => {
            const data = await fetchTags()
            setTags(data)
            console.log(categories)
        }
        getTags();


    }, [])



    useEffect(() => {

        const currentParams = new URLSearchParams(window.location.search);
        if (currentParams.get("recipeName") !== searchQuery) {
            const updatedParams = new URLSearchParams(window.location.search);
            updatedParams.set("recipeName", searchQuery);

            if (searchQuery === "") {
                updatedParams.delete("recipeName");
            }



            if (recipeDisplayPaths.some(path => pathname.includes(path))) {
                router.replace(`${pathname}?${updatedParams.toString()}`, { scroll: false });
            }
        }
    }, [searchQuery]);




    function handleSearch(e) {
        if (e.key === "Enter" && !recipeDisplayPaths.some(path => pathname.includes(path))) {
            const updatedParams = new URLSearchParams();
            updatedParams.set("recipeName", searchQuery);
            router.push(`/recipes?${updatedParams.toString()}`, { scroll: false });
        }
    }


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
            <div className="hamburger" onClick={toggleMenu}>
                {menuOpen ? <FaTimes /> : <FaBars />}
            </div>

            <div className={`nav-links ${menuOpen ? "open" : ""} ${isAnimating ? "closing" : ""}`}>

                <div className="search-bar">
                    <input value={searchQuery}
                        onChange={(e) => setSearchQuery(e.target.value)}
                        onKeyDown={handleSearch}
                        placeholder="Search a recipe..." />
                    <FaSearch />
                </div>
                <button className="primary-btn">
                    Home
                </button>
                {
                    user &&
                    <div className="create-modify-btns">
                        <button className="primary-btn">Create Recipe</button>
                        <button className="primary-btn">My Recipes</button>
                    </div>
                }
                <div className="dropdowns">
                    <div className="dropdown">
                        <span className="dropbtn">Categories</span>
                        <div className="dropdown-content">
                            {categories.map(category => <Link onClick={closeMenu} key={category.categoryId} href={`/category/${category.categoryId}/${encodeURIComponent(category.name)}`}>{category.name}</Link>)}
                        </div>
                    </div>
                    <div className="dropdown">
                        <span className="dropbtn">Tags</span>
                        <div className="dropdown-content">
                            {tags.map(tag => <Link onClick={closeMenu} key={tag.tagId} href={`/tag/${tag.tagId}/${encodeURIComponent(tag.name)}`}>{tag.name}</Link>)}
                        </div>
                    </div>
                </div>
                <div className="login-logout">
                    {
                        user ? <button className="primary-btn">Logout</button> :
                            <button className="primary-btn" onClick={openModal}>Login</button>
                    }
                </div>
            </div>
            <LoginRegisterModal isOpen={isModalOpen} onClose={closeModal} />

        </nav>
    );
}