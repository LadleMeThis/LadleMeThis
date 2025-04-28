import React from 'react'

const RecipeContainerTitle = ({text, name}) => {
  return (
    <div className="recipe-card-container-title">
    <h1>{text}</h1> 
    <h1 className="recipe-card-container-title-name">{name}</h1>
</div>
  )
}

export default RecipeContainerTitle