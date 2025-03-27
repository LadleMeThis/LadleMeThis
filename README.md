# LadleMeThis 🍲

Welcome to **LadleMeThis** – the recipe platform that's just as delicious as the dishes it serves! Whether you're a seasoned chef or a kitchen rookie, our platform helps you find, share, and get inspired by tasty creations from around the world.

## Table of Contents

- [About](#about)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## About 🍴

*LadleMeThis* is an innovative and fun recipe platform that lets you discover dishes, plan meals, and share your culinary creations with others. Inspired by the concept of using what's in your fridge, *LadleMeThis* ensures that no ingredient goes to waste – or worse, forgotten!

Our mission: Make cooking simple, fun, and approachable for everyone. With a sprinkle of whimsy and a dash of practicality, we aim to bring joy to your kitchen.

## Installation ⚙️

### Prerequisites

- Docker and Docker Compose must be installed on your machine. If you don't have them yet, follow the installation instructions on the [official Docker website](https://docs.docker.com/get-docker/) and [Docker Compose documentation](https://docs.docker.com/compose/install/).

To get started with *LadleMeThis*, follow these simple steps:

1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/ladle-me-this.git
    ```
    
2. Build and start the backend with Docker Compose:
    ```bash
    cd backend
    docker-compose up --build
    ```

3. Install dependencies for frontend (Next.js):
    ```bash
    cd ladle-me-this
    npm install  # for the frontend
    ```

4. Run the frontend locally:
    ```bash
    npm run dev  # frontend
    ```

4. Open your browser and go to `http://localhost:3000` to see the magic happen!

## Usage 🍽️

Using *LadleMeThis* is easy and fun! Here’s how you can get started:

1. **Discover Recipes:** 
   - Browse a wide variety of recipes by category, cuisine, or ingredients.
   - Use the search bar to find recipes based on what you have in your fridge or pantry. Simply type in your available ingredients, and we'll suggest recipes that match!

2. **What's in My Fridge?** 🥕
   - Click on the **"What's in My Fridge?"** button and enter the ingredients you currently have.
   - Based on the items you provide, *LadleMeThis* will recommend recipes you can make right now. No more last-minute takeout decisions!
   



---

_“Life is short, eat the cake... or whatever delicious thing you're making today!”_
