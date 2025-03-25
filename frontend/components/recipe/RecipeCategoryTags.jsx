import { FaTags } from "react-icons/fa";

const RecipeCategoryTags = ({ categories, tags }) => (
  <div className="recipe-categories-tags">
    <div className="recipe-categories">
      {categories.map((category) => (
        <span className="recipe-category" key={category.categoryId}>
          {category.name}
        </span>
      ))}
    </div>
    <div className="recipe-tags">
      {tags.map((tag) => (
        <span className="recipe-tag" key={tag.tagId}>
          <FaTags className="recipe-icon" /> {tag.name}
        </span>
      ))}
    </div>
  </div>
);

export default RecipeCategoryTags;
