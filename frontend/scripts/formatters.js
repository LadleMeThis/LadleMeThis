
import { FaTags } from "react-icons/fa";
import { BiSolidCategoryAlt } from "react-icons/bi";
import { TbLadle } from "react-icons/tb";

export const formatRecipeToUpdate = (data) => {
    delete data.ratings;
    data.ingredients = data.ingredients?.map((i) => i.ingredientId);
    data.tags = data.tags?.map((i) => i.tagId);
    data.categories = data.categories?.map((i) => i.categoryId);
    
    return data;
  };
  
  export const getIdForActiveTab = (activeTab) => {
    switch (activeTab) {
      case "ingredients":
        return "ingredientId";
      case "categories":
        return "categoryId";
      case "tags":
        return "tagId";
      default:
        return null;
    }
  };
  
  export const getIconForActiveTab = (activeTab) => {
    switch (activeTab) {
      case "ingredients":
        return <TbLadle />;
      case "categories":
        return <BiSolidCategoryAlt />;
      case "tags":
        return <FaTags />;
      default:
        return <TbLadle />;
    }
  };
  