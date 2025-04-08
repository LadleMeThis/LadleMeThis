import { FaStar, FaRegStar } from 'react-icons/fa';

const CreateRating = ({ rating, onChange }) => {
    const totalStars = 5;
    const validRating = Math.max(0, Math.min(totalStars, rating));

    const handleClick = (index) => {
        const clickedRating = index + 1;
        onChange(clickedRating)
    }


    if (isNaN(validRating))
        return <div>Invalid Rating</div>;

    return (
        <div className="create-rating-score">
            {[...Array(totalStars)].map((_, index) => (
                <span
                    key={index}
                    onClick={() => handleClick(index)}
                >
                    {index < validRating ? <FaStar /> : <FaRegStar />}
                </span>
            ))}
        </div>
    );
};


export default CreateRating
