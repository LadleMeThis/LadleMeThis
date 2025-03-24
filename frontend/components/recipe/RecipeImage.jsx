import Image from "next/image";
import recipeImage from "@/imgs/carbonara.webp";

const RecipeImage = () => (
  <div className="recipe-image">
    <Image
      src={recipeImage}
      alt="Recipe Image"
      width={500}
      height={300}
    />
  </div>
);

export default RecipeImage;
