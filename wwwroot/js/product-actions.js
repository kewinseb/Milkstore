let cart = {}; // Store cart items

// Cached DOM elements
const cartContainer = document.getElementById('cartContainer');
const blurOverlay = document.getElementById('blurOverlay');
const cartBody = document.getElementById('cartBody');
const totalAmountEl = document.getElementById('total');
const cartCountEl = document.getElementById('cart-count');
const cartIcon = document.getElementById('cartIcon');
const closeCartBtn = document.getElementById('closeCartBtn');

// Initially hide cart count circle
cartCountEl.classList.add('hidden');

// Open & Close Cart Functions
function toggleCart(open) {
    cartContainer.classList.toggle('active', open);
    blurOverlay.style.display = open ? 'block' : 'none';
}

// Event Listeners
cartIcon.addEventListener('click', () => toggleCart(true));
closeCartBtn.addEventListener('click', () => toggleCart(false));

// Delegate event handling to the document for buttons (better performance)
document.addEventListener('click', (event) => {
    const target = event.target;

    // Add/Remove Product Button
    if (target.classList.contains('add-to-bag') || target.classList.contains('remove-from-bag')) {
        const productId = target.dataset.productId;
        const productName = target.dataset.productName;
        const productPrice = parseFloat(target.dataset.productPrice);
        const productImage = target.dataset.productImage;

        if (!cart[productId]) {
            addToBag(productId, productName, productPrice, productImage);
        } else {
            removeFromBag(productId);
        }

        // Ensure cart opens when an item is added
        toggleCart(true);
    }

    // Quantity Increment/Decrement
    if (target.classList.contains('increment') || target.classList.contains('decrement')) {
        const productId = target.closest('.cart-item').dataset.productId;
        const change = target.classList.contains('increment') ? 1 : -1;
        updateQuantity(productId, change);
    }

    // Remove Product from Cart (Trash Icon)
    if (target.classList.contains('delete-icon')) {
        const productId = target.closest('.cart-item').dataset.productId;
        removeFromBag(productId);
    }
});

// Function to Add Product to Cart
function addToBag(id, name, price, image) {
    cart[id] = cart[id] || { name, price, image, quantity: 1 };
    updateCartUI();
    updateProductButton(id, true);
}

// Function to Remove Product from Cart
function removeFromBag(id) {
    delete cart[id];
    updateCartUI();
    updateProductButton(id, false);
}

// Function to Update Quantity
function updateQuantity(id, change) {
    if (cart[id]) {
        cart[id].quantity = Math.max(0, cart[id].quantity + change);
        if (cart[id].quantity === 0) removeFromBag(id);
        updateCartUI();
    }
}

// Function to Update "Add to Bag" Button
function updateProductButton(id, isAdded) {
    const button = document.querySelector(`.add-to-bag[data-product-id="${id}"], .remove-from-bag[data-product-id="${id}"]`);
    if (button) {
        button.textContent = isAdded ? 'Remove from Bag' : 'Add to Bag';
        button.classList.toggle('add-to-bag', !isAdded);
        button.classList.toggle('remove-from-bag', isAdded);
    }
}

// Function to Update Cart UI
function updateCartUI() {
    cartBody.innerHTML = '';
    let totalAmount = 0, totalQuantity = 0;

    Object.entries(cart).forEach(([id, product]) => {
        totalAmount += product.price * product.quantity; // ✅ Price updates based on quantity
        totalQuantity += product.quantity;

        const cartItem = document.createElement('div');
        cartItem.className = 'cart-item';
        cartItem.dataset.productId = id;

        cartItem.innerHTML = `
            <img src="${product.image}" alt="${product.name}" />
            <div class="cart-item-details">
                <p class="cart-item-title">${product.name}</p>
                <p>Price: ₹${(product.price * product.quantity).toFixed(2)}</p> <!-- ✅ Updated Price Display -->
                <div class="quantity-controls">
                    <button class="decrement">-</button>
                    <span class="cart-item-quantity">${product.quantity}</span>
                    <button class="increment">+</button>
                </div>
            </div>
            <i class="fa-solid fa-trash delete-icon"></i>
        `;

        cartBody.appendChild(cartItem);
    });

    totalAmountEl.textContent = `Total: ₹${totalAmount.toFixed(2)}`;

    // Show the number of products added (not the quantity)
    const totalProductsAdded = Object.keys(cart).length; // Get the number of distinct products added
    cartCountEl.textContent = totalProductsAdded;

    // Show or hide the cart count circle based on the number of products added
    cartCountEl.classList.toggle('hidden', totalProductsAdded === 0); // Hide if no products

    // If no products are in the cart, show frown icon and message with jello animation
    if (totalProductsAdded === 0) {
        cartBody.innerHTML = `
            <section class="page_404">
		<div class="four_zero_four_bg">
		</div>

		<div class="content_box">
		<h3 class="h2">
		Sorry, No products in the cart 🛒!
		</h3>
	</div>
</section>
        `;
    } else {
        // Trigger the jello animation for the cart count circle when there are products in the cart
        cartCountEl.classList.add('jello-animation');
        setTimeout(() => {
            cartCountEl.classList.remove('jello-animation');
        }, 1000); // Duration of jello animation
    }
}

// Call this function on page load or whenever the user navigates to the cart
document.addEventListener('DOMContentLoaded', updateCartUI); // This will trigger when the page is loaded

// If you are using a navigation or routing system, call updateCartUI when navigating to the cart page

