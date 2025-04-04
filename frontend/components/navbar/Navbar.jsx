"use client"
import Link from "next/link";
import { useEffect, useState } from "react";
import { usePathname, useRouter, useSearchParams } from "next/navigation";
import { FaSearch, FaBars, FaTimes } from "react-icons/fa";
import { fetchCategories, fetchTags } from "@/scripts/scripts";
import LoginRegisterModal from "@/components/loginRegisterModal/loginRegisterModal";
import Image from "next/image";
import logo from "@/imgs/logo.png"



// search bar should work differently, depending on the current path

// on any page that displays recipes should work as a filter

// on any other page should work as a simple search


export default function Navbar() {
    const [categories, setCategories] = useState([]);
    const [tags, setTags] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [user, setUser] = useState(true); // this is just for demonstration
    const [searchQuery, setSearchQuery] = useState("");
    const router = useRouter();
    const pathname = usePathname();
    const recipeDisplayPaths = ["/category", "/tag", "/my-recipes"] // pages where search bar should function as a filter
    const [menuOpen, setMenuOpen] = useState(false);
    const [isAnimating, setIsAnimating] = useState(false);

    //home btn, create, modify, my recipes
    //after pressing a btn, dont forget to close the hamburger menu

    const openModal = () => setIsModalOpen(true);
    const closeModal = () => setIsModalOpen(false);


    function toggleMenu() {
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

    function closeMenu() {
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


    function navigateTo(path) {
        closeMenu();
        router.push(path);
    }

    return (
        <nav className="navbar">
            <div className="logo" onClick={() => router.push("/")}>
                <Image width={200} height={200} src={logo} alt="The site's cool logo" />
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
                <button className="secondary-btn" onClick={() => navigateTo("/")}>
                    Home
                </button>
                {
                    user &&
                    <button className="secondary-btn" onClick={() => navigateTo("/create")} >Create Recipe</button>
                    &&
                    <button className="secondary-btn" onClick={() => navigateTo("/my-recipes")}>My Recipes</button>
                }
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
                <div className="login-logout">
                    {
                        user ? <button className="primary-btn" onClick={() => logout()}>Logout</button> :
                            <button className="primary-btn" onClick={openModal}>Login</button>
                    }
                </div>
            </div>
            <LoginRegisterModal isOpen={isModalOpen} onClose={closeModal} />

        </nav>
    );
}