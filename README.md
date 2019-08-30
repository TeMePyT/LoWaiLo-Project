# LoWaiLo-Project
A C# Web Module Defense Project

http://lowailo.azurewebsites.net/

admin: TeMePyT

password: 123456

## Description

**“LoWaiLo”** is a project for chinese restaurant, built with **ASP.NET Core 2.2** and **SQL Server** for data storage. Guest users can see the home page, the reviews for the restaurant, the menu, the product details page and reviews, to add products in the shopping carts. Registered users can write site reviews, product reviews, checkout an order.

## Functionality

###	User Login 
- Login in current application using email and password of already registered user.
###	User Register
- Register a new user by providing email, password and username.
###	User Logout
- Logouts from the application.
###	Home
- Information about the restaurant.
- Carousel with the menu categories.
- Promo video.
### About us
- Detailed information about the restaurant.
### Reviews
- List of all reviews about the restaurant. Reviews with rating below 2 are in black box, with rating above 2 - white box.
- Registered users can write a review about the restaurant.
### Menu
- List of all menu categories and their corresponding products.
- The product picture is a link to the product details.
- Guests/Users can add products to their cart.
### Product details
- Details about the product
- Guests/Users can add product to their cart and selec quantity.
- List of all product reviews. Reviews with rating below 2 are in black box, with rating above 2 - white box.
- Registered users can write a review about the product.
### Contacts
- Detailed information about the restaurant address, phones for contact and google map with the location.
### My orders
- List of user`s orders(ordered by date) and basic information about them.
- Link to order details.
### Panel cart
- Guests/Users can edit product quantity or remove product from cart.
- Option to checkout the order.

## Administrator area

### Index
- Lists of pending and accepted orders.
- Options to cancel or process order.
- Option for order details.
### Orders
#### Shipped orders
- List of orders, that are being shipped.
- Ability to cancel order or mark it as delivered.
- Option for order details.
#### History
- List of all delivered orders(ordered by date).
- List of all canceled orders(ordered by date).
### Menu
#### Categories
##### All
- List of all categories.
- Options to Edit or Delete category.
##### Create
- Form for creating a category.
#### Products
##### All
- List of all products, ordered by CategoryID, then by ID.
- Options to Edit or Delete product.
- Option for product details.
##### Create
- Form for creating a product.
#### Addons
#####All
- List of all addons.
- Options to Edit or Delete Addon.

