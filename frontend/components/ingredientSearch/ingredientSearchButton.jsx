import React from "react";
import { GiCauldron } from "react-icons/gi";

const SearchButton = ({ onClick, disabled }) => (
    <button
        className="search-button"
        onClick={onClick}
        disabled={disabled}
    >
        <div className="search-button-content">
            <GiCauldron />
            <h1>Ladle me this</h1>
        </div>
    </button>
);

export default SearchButton;
