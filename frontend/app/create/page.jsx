"use client";
import React, { useState, useEffect } from "react";
import { fetchCategories } from "@/scripts/categories";
import { fetchCreate } from "@/scripts/recipe";
import { fetchIngredients } from "@/scripts/ingredients";
import { fetchTags } from "@/scripts/tags";
import Loader from "@/components/loader/Loader";
import CreateFormGroup from "@/components/create/CreateFormGroup";
import Tabs from "@/components/create/Tabs";
import TabContent from "@/components/create/TabContent";
import { useToast } from "@/context/ToastContext";

const CreateRecipe = () => {
  const [formData, setFormData] = useState({
    name: "",
    prepTime: "",
    cookTime: "",
    instructions: "",
    servingSize: "",
    ingredients: [],
    tags: [],
    categories: [],
  });

  const [ingredients, setIngredients] = useState([]);
  const [tags, setTags] = useState([]);
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [activeTab, setActiveTab] = useState("ingredients");
  const { showToast } = useToast() 

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      const [ingredientsData, tagsData, categoriesData] = await Promise.all([
        fetchIngredients(),
        fetchTags(),
        fetchCategories(),
      ]);

      setIngredients(ingredientsData);
      setTags(tagsData);
      setCategories(categoriesData);
      setLoading(false);
    };

    fetchData();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleItemClick = (id) => {
    setFormData((prev) => {
      const isSelected = prev[activeTab].some((i) => i === id);
      return {
        ...prev,
        [activeTab]: isSelected
          ? prev[activeTab].filter((i) => i !== id)
          : [...prev[activeTab], id],
      };
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if(formData.categories.length == 0){
      showToast("Missing categories!")
      return;
    }
    else if(formData.tags.length == 0){
      showToast("Missing tags!")
      return;
    }
    else if(formData.ingredients.length == 0){
      showToast("Missing ingredients!")
      return;
    }

    try {
      await fetchCreate(formData)
      showToast("Successful recipe creation!")
    } catch (error) {
      showToast(error.message)
    }
  };

  const renderTabContent = () => {
    const dataMap = {
      ingredients,
      tags,
      categories,
    };

    const currentData = dataMap[activeTab] || [];

    return (
      <TabContent
        currentData={currentData}
        formData={formData}
        activeTab={activeTab}
        handleItemClick={handleItemClick}
      />
    );
  };

  if (loading)
    return <Loader />;

  return (
    <div className="create-recipe">
      <div className="create-recipe-header">
        <h1 className="create-recipe-header-title">Create Recipe</h1>
        <p className="create-recipe-header-subtitle">Share your delicious ideas!</p>
      </div>

      <form className="create-recipe-form" onSubmit={handleSubmit}>
        <div>
          <CreateFormGroup
            label="Recipe Name"
            name="name"
            type="text"
            value={formData.name}
            onChange={handleInputChange}
            placeholder="Enter recipe name"
          />
          <CreateFormGroup
            label="Prep Time (minutes)"
            name="prepTime"
            type="number"
            value={formData.prepTime}
            onChange={handleInputChange}
            placeholder="Enter preparation time"
          />
          <CreateFormGroup
            label="Cook Time (minutes)"
            name="cookTime"
            type="number"
            value={formData.cookTime}
            onChange={handleInputChange}
            placeholder="Enter cooking time"
          />
          <CreateFormGroup
            label="Instructions"
            name="instructions"
            type="textarea"
            value={formData.instructions}
            onChange={handleInputChange}
            placeholder="Enter cooking instructions"
            rows={5}
          />
          <CreateFormGroup
            label="Serving Size"
            name="servingSize"
            type="number"
            value={formData.servingSize}
            onChange={handleInputChange}
            placeholder="Enter serving size"
          />
        </div>

        <div className="create-recipe-tabs">
          <Tabs
            tabs={[
              { id: "ingredients", label: "Ingredients", count: formData.ingredients.length },
              { id: "tags", label: "Tags", count: formData.tags.length },
              { id: "categories", label: "Categories", count: formData.categories.length },
            ]}
            activeTab={activeTab}
            onTabChange={setActiveTab}
          />
          {renderTabContent()}
        </div>

        <button type="submit" className="btn-submit">
          Create Recipe
        </button>
      </form>
    </div>
  );
};

export default CreateRecipe;
