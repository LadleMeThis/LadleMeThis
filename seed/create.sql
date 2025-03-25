-- Insert 7 categories
INSERT INTO Categories (Name)
VALUES
('Appetizer'),
('Main Course'),
('Dessert'),
('Salad'),
('Soup'),
('Side Dish'),
('Beverage');

-- Insert 7 tags
INSERT INTO Tags (Name)
VALUES
('Vegan'),
('Gluten-Free'),
('Spicy'),
('Low Carb'),
('Quick'),
('Healthy'),
('Comfort Food');

-- Insert 14 ingredients
INSERT INTO Ingredients (Name, Unit)
VALUES
('Tomato', 'pcs'),
('Cucumber', 'pcs'),
('Chicken', 'g'),
('Cheese', 'g'),
('Lettuce', 'pcs'),
('Garlic' , 'pcs'),
('Olive Oil', 'ml'),
('Rice' , 'g'),
('Pasta', 'g'),
('Potato', 'g'),
('Avocado' , 'pcs'),
('Spinach' , 'g'),
('Basil' , 'pcs'),
('Onion' , 'g');

-- Insert users (Recipe Seeder and Rating Users)
INSERT INTO AspNetUsers (
    Id, UserName, NormalizedUserName, Email, NormalizedEmail, 
    EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, 
    PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, 
    LockoutEnd, LockoutEnabled, AccessFailedCount
)
VALUES
(
    NEWID(), 'recipeSeeder', 'RECIPESEEDER', 'seeder@example.com', 'SEEDER@EXAMPLE.COM', 
    0, 'password_hash', NEWID(), NEWID(), 
    NULL, 0, 0, 
    NULL, 1, 0
),
(
    NEWID(), 'ratingUser1', 'RATINGUSER1', 'ratinguser1@example.com', 'RATINGUSER1@EXAMPLE.COM', 
    0, 'password_hash', NEWID(), NEWID(), 
    NULL, 0, 0, 
    NULL, 1, 0
),
(
    NEWID(), 'ratingUser2', 'RATINGUSER2', 'ratinguser2@example.com', 'RATINGUSER2@EXAMPLE.COM', 
    0, 'password_hash', NEWID(), NEWID(), 
    NULL, 0, 0, 
    NULL, 1, 0
);





-- Insert recipes for 'Appetizer'
INSERT INTO Recipes (Name, Instructions, PrepTime, CookTime, ServingSize, UserId)
VALUES
('Bruschetta', 'Toast bread, top with tomatoes, garlic, and basil.', 10, 5, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Stuffed Mushrooms', 'Stuff mushrooms with cheese, bake at 375°F for 15 minutes.', 15, 20, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Deviled Eggs', 'Hard boil eggs, remove yolk, mix with mayo and mustard.', 10, 10, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Cucumber Sandwiches', 'Slice cucumbers and place between bread slices.', 5, 5, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Garlic Bread', 'Toast bread with garlic butter and herbs.', 5, 5, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Caprese Salad', 'Combine tomatoes, mozzarella, and basil with olive oil.', 10, 0, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Mini Quiches', 'Prepare mini quiches with cheese, ham, and eggs.', 15, 25, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder'));

-- Insert recipes for 'Main Course'
INSERT INTO Recipes (Name, Instructions, PrepTime, CookTime, ServingSize, UserId)
VALUES
('Spaghetti Bolognese', 'Cook pasta and top with meat sauce.', 10, 30, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Grilled Chicken', 'Grill chicken breasts with seasoning.', 15, 30, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Beef Stew', 'Cook beef with vegetables and broth until tender.', 20, 120, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Vegetable Stir-Fry', 'Stir-fry vegetables with soy sauce and garlic.', 10, 10, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Chicken Alfredo', 'Cook pasta and mix with creamy chicken sauce.', 15, 20, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Salmon Fillet', 'Bake salmon fillet with lemon and herbs.', 10, 20, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Eggplant Parmesan', 'Layer eggplant with cheese and bake.', 20, 30, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder'));

-- Insert recipes for 'Dessert'
INSERT INTO Recipes (Name, Instructions, PrepTime, CookTime, ServingSize, UserId)
VALUES
('Chocolate Cake', 'Mix ingredients, bake at 350°F for 30 minutes.', 15, 30, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Apple Pie', 'Bake pie with apples, sugar, and cinnamon.', 20, 45, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Lemon Meringue Pie', 'Prepare crust, fill with lemon curd, top with meringue.', 15, 45, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Brownies', 'Mix ingredients, bake at 350°F for 20 minutes.', 10, 20, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Panna Cotta', 'Prepare cream mixture, chill for 3 hours.', 15, 0, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Cheesecake', 'Bake crust, fill with cheesecake batter and bake.', 20, 60, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Tiramisu', 'Layer coffee-soaked biscuits with mascarpone cream.', 15, 0, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder'));

-- Insert recipes for 'Salad'
INSERT INTO Recipes (Name, Instructions, PrepTime, CookTime, ServingSize, UserId)
VALUES
('Caesar Salad', 'Mix lettuce, croutons, parmesan, and Caesar dressing.', 10, 0, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Greek Salad', 'Combine cucumbers, tomatoes, olives, and feta cheese.', 10, 0, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Cobb Salad', 'Top lettuce with chicken, bacon, avocado, and eggs.', 15, 0, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Caprese Salad', 'Combine tomatoes, mozzarella, and basil with olive oil.', 10, 0, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Spinach Salad', 'Toss spinach with nuts, cranberries, and balsamic vinaigrette.', 10, 0, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Avocado Salad', 'Combine avocado, cherry tomatoes, and lime dressing.', 10, 0, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Asian Salad', 'Mix cabbage, carrots, and sesame dressing.', 10, 0, 4, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder'));

-- Insert recipes for 'Soup'
INSERT INTO Recipes (Name, Instructions, PrepTime, CookTime, ServingSize, UserId)
VALUES
('Tomato Soup', 'Simmer tomatoes with herbs and blend until smooth.', 15, 30, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Chicken Soup', 'Cook chicken, vegetables, and broth until tender.', 15, 45, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Lentil Soup', 'Simmer lentils, tomatoes, and spices until soft.', 10, 30, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Potato Soup', 'Simmer potatoes with onions, blend and add cream.', 15, 40, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Butternut Squash Soup', 'Simmer squash with onions and blend until smooth.', 15, 30, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Minestrone Soup', 'Cook vegetables, beans, and pasta in broth.', 20, 40, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('French Onion Soup', 'Caramelize onions, add broth, and bake with cheese.', 15, 45, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder'));

-- Insert recipes for 'Side Dish'
INSERT INTO Recipes (Name, Instructions, PrepTime, CookTime, ServingSize, UserId)
VALUES
('Garlic Mashed Potatoes', 'Boil potatoes, mash with butter and garlic.', 15, 20, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Roasted Vegetables', 'Roast carrots, potatoes, and onions with olive oil.', 10, 40, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Rice Pilaf', 'Simmer rice with vegetables and spices.', 10, 25, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Grilled Asparagus', 'Grill asparagus with olive oil and seasoning.', 10, 15, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Baked Beans', 'Simmer beans with brown sugar and bacon.', 10, 40, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Corn on the Cob', 'Boil corn and top with butter and seasoning.', 5, 15, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Mac and Cheese', 'Cook pasta and mix with cheese sauce.', 15, 25, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder'));

-- Insert recipes for 'Beverage'
INSERT INTO Recipes (Name, Instructions, PrepTime, CookTime, ServingSize, UserId)
VALUES
('Iced Tea', 'Brew tea, chill, and serve over ice.', 10, 0, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Lemonade', 'Mix lemon juice, sugar, and water.', 5, 0, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Smoothie', 'Blend fruit, yogurt, and ice.', 5, 0, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Coffee', 'Brew coffee and serve hot.', 5, 0, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Hot Chocolate', 'Heat milk and mix with chocolate.', 5, 5, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Milkshake', 'Blend milk, ice cream, and flavoring.', 5, 0, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Cocktail', 'Mix alcohol and fruit juice or soda.', 10, 0, 6, (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder'));

-- Insert ratings for different recipes
INSERT INTO [LadleMeThis].[dbo].[Ratings] ([Review], [Rating], [DateCreated], [RecipeId], [UserId])
VALUES
('Great appetizer, loved it!', 5, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Bruschetta'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Simple but delicious, will make again.', 4, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Bruschetta'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('The spaghetti was amazing, my family loved it.', 5, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Spaghetti Bolognese'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('A little too salty for my taste.', 3, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Spaghetti Bolognese'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Delicious, though the cake could be a bit sweeter.', 4, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Chocolate Cake'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Perfect dessert, I love the texture.', 5, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Chocolate Cake'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Good recipe, but the soup could be thicker.', 3, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Tomato Soup'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Absolutely love this soup, so comforting!', 5, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Tomato Soup'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('The grilled chicken was perfectly cooked.', 5, GETDATE(), (SELECT RecipeId  FROM Recipes WHERE Name = 'Grilled Chicken'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('A bit dry, could use more seasoning.', 3, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Grilled Chicken'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Tasty, but I would add more garlic next time.', 4, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Garlic Bread'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Very crunchy and flavorful!', 5, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Garlic Bread'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('This salad is refreshing, great for summer!', 5, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Caesar Salad'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('It could use a bit more dressing.', 4, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Caesar Salad'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Amazing drink, perfect balance of sweetness and tartness.', 5, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Lemonade'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder')),
('Too sweet for my liking, but refreshing.', 3, GETDATE(), (SELECT RecipeId FROM Recipes WHERE Name = 'Lemonade'), (SELECT Id FROM AspNetUsers WHERE UserName = 'recipeSeeder'));



