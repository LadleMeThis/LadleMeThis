import { FaClock, FaUtensils } from "react-icons/fa";

const RecipeInfo = ({ prepTime, cookTime, servingSize }) => (
  <p className="recipe-info">
    <FaClock className="recipe-icon" /> {prepTime} min prep
    <FaClock className="recipe-icon" /> {cookTime} min cook
    <FaUtensils className="recipe-icon" /> Serves {servingSize}
  </p>
);

export default RecipeInfo;
