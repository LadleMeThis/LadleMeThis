import React, { useEffect, useState } from 'react';

const Loader = () => {
    const [loaderClass, setLoaderClass] = useState('');
    const classes = ['cola', 'blue-ice', 'color-ice', 'pizza', 'cookies', 'chocolate', 'sausage', 'watermelon'];

    useEffect(() => {
        const randomIndex = Math.floor(Math.random() * classes.length);
        setLoaderClass(classes[randomIndex]);
    }, []);

    return <div className="loader-container">
        <div className={loaderClass} />
    </div>
};

export default Loader;
