"use client";
import Loader from "@/components/loader/Loader";
import { useParams, useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";


export default function MyRecipes() {
    const searchParams = useSearchParams();
    const recipeName = searchParams.get("recipeName");
    const [recipes, setRecipes] = useState([]);
    const [displayedRecipes, setDisplayedRecipes] = useState([]);
    const { categoryId } = useParams();
    const [loading, setLoading] = useState(true);



    useEffect(() => {

    }, [])






    // if (loading) {
    //     return <Loader />
    // }


    return (
        <div className="my-recipes-main">

        </div>
    )
}