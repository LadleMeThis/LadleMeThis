"use client"
import Link from "next/link";
import { useEffect, useState } from "react";
import { usePathname, useRouter } from "next/navigation";
import { FaSearch, FaBars, FaTimes } from "react-icons/fa";
import { fetchCategories } from "@/scripts/categories";
import { fetchTags } from "@/scripts/tags";
import { logout } from "@/scripts/auth";
import LoginRegisterModal from "@/components/loginRegisterModal/loginRegisterModal";
import Image from "next/image";
import logo from "@/imgs/logo.png"
import { useAuth } from "@/context/AuthContext";

export default function Navbar() {
    const [categories, setCategories] = useState([]);
    const [tags, setTags] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [searchQuery, setSearchQuery] = useState("");
    const router = useRouter();
    const pathname = usePathname();
    const recipeDisplayPaths = ["/category", "/tag", "/my-recipes"]
    const [menuOpen, setMenuOpen] = useState(false);
    const [isAnimating, setIsAnimating] = useState(false);
    const { isAuthenticated, setIsAuthenticated, checkAuthentication } = useAuth()
    const [dropdownOpen, setDropdownOpen] = useState("");



    const openModal = () => setIsModalOpen(true);
    const closeModal = () => setIsModalOpen(false);
    const handleDropdownClick = (text) => {
        if (dropdownOpen == text)
            setDropdownOpen("")
        else
            setDropdownOpen(text)
    }



    function toggleMenu() {
        if (menuOpen) {
            setIsAnimating(true);
            setTimeout(() => {
                setMenuOpen(false);
                setIsAnimating(false);
            }, 300);
        } else {
            setMenuOpen(true);
        }
    };

    function closeMenu() {
        setMenuOpen(false);
        setIsAnimating(false);
    };


    useEffect(() => {
        const getCategories = async () => {
            const data = await fetchCategories()
            setCategories(data)
        }
        getCategories();


        const getTags = async () => {
            const data = await fetchTags()
            setTags(data)
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

    useEffect(() => {
        if (!isModalOpen)
            checkAuthentication();

    }, [isModalOpen]);


    function handleSearch(e) {
        if (e.key === "Enter" && !recipeDisplayPaths.some(path => pathname.includes(path))) {
            const updatedParams = new URLSearchParams();
            updatedParams.set("recipeName", searchQuery);
            router.push(`/recipes?${updatedParams.toString()}`, { scroll: false });
        }
    }

    function navigateTo(path) {
        closeMenu();
        router.push(path);
    }

    const handleLogout = async () => {
        await logout()
        setIsAuthenticated(false)
        setIsModalOpen(false)
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
                    isAuthenticated && (
                        <>
                            <button className="secondary-btn" onClick={() => navigateTo("/create")}>Create Recipe</button>
                            <button className="secondary-btn" onClick={() => navigateTo("/my-recipes")}>My Recipes</button>
                            <button className="secondary-btn" onClick={() => navigateTo("/profile")}>Profile</button>
                        </>
                    )
                }
                <div className="dropdown">
                    <span className="dropbtn" onClick={() => handleDropdownClick("categories")}>Categories</span>
                    <div className={`dropdown-content${dropdownOpen == "categories" ? " open" : ""}`}>
                        {categories.map(category => <Link onClick={closeMenu} key={category.categoryId} href={`/category/${category.categoryId}/${encodeURIComponent(category.name)}`}>{category.name}</Link>)}
                    </div>
                </div>
                <div className="dropdown">
                    <span className="dropbtn" onClick={() => handleDropdownClick("tags")}>Tags</span>
                    <div className={`dropdown-content${dropdownOpen == "tags" ? " open" : ""}`}>
                        {tags.map(tag => <Link onClick={closeMenu} key={tag.tagId} href={`/tag/${tag.tagId}/${encodeURIComponent(tag.name)}`}>{tag.name}</Link>)}
                    </div>
                </div>
                <div className="login-logout">
                    {
                        isAuthenticated ? <button className="primary-btn" onClick={() => handleLogout()}>Logout</button> :
                            <button className="primary-btn" onClick={openModal}>Login</button>
                    }
                </div>
            </div>
            <LoginRegisterModal isOpen={isModalOpen} onClose={closeModal} />

        </nav>
    );
}