"use client";
import React, { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import RecipeImage from "@/components/recipe/recipeImage";
import RecipeInfo from "@/components/recipe/RecipeInfo";
import RecipeCategoryTags from "@/components/recipe/RecipeCategoryTags";
import RecipeIngredients from "@/components/recipe/RecipeIngredients";
import RecipeRatings from "@/components/recipe/RecipeRatings";
import { fetchRecipeById } from "@/scripts/scripts";
import Loader from "@/components/loader/Loader";
import CreateRecipeRating from "@/components/recipe/CreateRecipeRating";
import { fetchCreateRating } from "@/scripts/scripts";
import { useToast } from "@/context/ToastContext";

const Recipe = () => {
  const [recipe, setRecipe] = useState(null);
  const [loading, setLoading] = useState(true);
  const { recipeId } = useParams();
  const [isAuthenticated, setIsAuthenticated] = useState(false)
  const [formData, setFormData] = useState({
    rating: 0,
    review: ""
  })
  const [ratings, setRatings] = useState([])
  const { showToast } = useToast();


  const checkAuthentication = async () => {
    try {
      const response = await fetch("/api/check-auth");
      const data = await response.json();

      setIsAuthenticated(data);

    } catch (err) {
      console.error(err);

      setIsAuthenticated(false);
    }
  };

  const clearFormdata = () => setFormData({
    rating: 0,
    review: ""
  })

  const handleSubmit = async (e) => {
    e.preventDefault()
    try {
      const ratingId = await fetchCreateRating(recipeId, formData)

      const updatedRatings = [...ratings]

      updatedRatings.push({
        ratingId: ratingId,
        user: {
          userName: null
        },
        rating: formData.rating,
        review: formData.review,
        dateCreated: new Date().toISOString()
      })

      setRatings(updatedRatings)
      clearFormdata()

      showToast("Successfully added rating")
    } catch (error) {
      showToast(error.message)
    }

  }

  const handleRatingChange = (changedRating) => {
    setFormData(current => ({
      ...current,
      rating: changedRating
    }))
  }

  const handleReviewChange = (changedReview) => {
    setFormData(current => ({
      ...current,
      review: changedReview
    }))
  }

  const isDisabled = () => !isAuthenticated || formData.rating === 0 || formData.review === "";

  useEffect(() => {
    const handleRecipe = async () => {
      const data = await fetchRecipeById(recipeId)
      setRecipe(data)
      setRatings(data.ratings)
      console.log(data.ratings[0]);
      console.log(data)

      setTimeout(() => {
        setLoading(false);
      }, 1500);

    };

    handleRecipe()
    checkAuthentication()

    return () => {
      setRecipe(null);
      setLoading(true);
    };

  }, [recipeId])

  useEffect(() => {
    console.log(isDisabled());

  }, [isAuthenticated, formData]);

  if (loading)
    return <Loader />


  return (
    <div className="recipe wrapper">
      <RecipeImage recipe={recipe} />
      <div className="photo-creator">
        Image was made by : <a href={recipe.recipeImage.photographerUrl}>{recipe.recipeImage.photographerName}</a>
      </div>
      <div className="recipe-header">
        <h1 className="recipe-title">{recipe.name}</h1>
        <RecipeInfo
          prepTime={recipe.prepTime}
          cookTime={recipe.cookTime}
          servingSize={recipe.servingSize}
        />
      </div>

      <RecipeCategoryTags categories={recipe.categories} tags={recipe.tags} />
      <p className="recipe-instructions">{recipe.instructions}</p>
      <RecipeIngredients ingredients={recipe.ingredients} />
      <RecipeRatings ratings={ratings} />
      {isAuthenticated && <CreateRecipeRating
        rating={formData.rating}
        handleRatingChange={handleRatingChange}
        review={formData.review}
        handleReviewChange={handleReviewChange}
        handleSubmit={handleSubmit}
        isDisabled={isDisabled} />}
    </div>
  );
};

export default Recipe;
