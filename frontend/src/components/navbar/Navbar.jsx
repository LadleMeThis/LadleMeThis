"use client"
import Link from "next/link";
import { useEffect, useState } from "react";
import { usePathname, useRouter, useSearchParams } from "next/navigation";
import { FaSearch } from "react-icons/fa";
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
    const [user, setUser] = useState(false); // this is just for demonstration

    const [searchQuery, setSearchQuery] = useState("");
    const router = useRouter();
    const pathname = usePathname();
    const recipeDisplayPaths = ["/category", "/tag"] // pages where search bar should function as a filter



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

            if(searchQuery === ""){
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
            <div className="search-bar">
                <input value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)}
                    onKeyDown={handleSearch}
                    placeholder="Search a recipe..." />
                <FaSearch />
            </div>
            <div className="dropdowns">
                <div className="dropdown">
                    <span className="dropbtn">Categories</span>
                    <div className="dropdown-content">
                        {categories.map(category => <Link key={category.categoryId} href={`/category/${category.categoryId}/${encodeURIComponent(category.name)}`}>{category.name}</Link>)}
                    </div>
                </div>
                <div className="dropdown">
                    <span className="dropbtn">Tags</span>
                    <div className="dropdown-content">
                        {tags.map(tag => <Link key={tag.tagId} href={`/tag/${tag.tagId}/${encodeURIComponent(tag.name)}`}>{tag.name}</Link>)}

                    </div>
                </div>
            </div>
            <div className="login-logout">
                {
                    user ? <button>Logout</button> :
                        <button onClick={openModal}>Login</button>
                }
            </div>
            <LoginRegisterModal isOpen={isModalOpen} onClose={closeModal} />

        </nav>
    );
}