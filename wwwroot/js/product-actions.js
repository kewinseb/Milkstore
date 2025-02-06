// ✅ Load cart from sessionStorage
let cart = JSON.parse(sessionStorage.getItem('cart')) || {};

// Cached DOM elements
const cartContainer = document.getElementById('cartContainer');
const blurOverlay = document.getElementById('blurOverlay');
const cartBody = document.getElementById('cartBody');
const totalAmountEl = document.getElementById('total');
const cartCountEl = document.getElementById('cart-count');
const cartIcon = document.getElementById('cartIcon');
const closeCartBtn = document.getElementById('closeCartBtn');
const continueShoppingBtn = document.getElementById('continueShoppingBtn');

// ✅ Update cart UI on page load
updateCartUI();

// Open & Close Cart Functions
function toggleCart(open) {
    cartContainer.classList.toggle('active', open);
    blurOverlay.style.display = open ? 'block' : 'none';
}

// ✅ Fix: Continue Shopping Saves Cart Before Redirecting
function continueShopping() {
    toggleCart(false);
    sessionStorage.setItem('cart', JSON.stringify(cart)); // Save cart
    window.location.href = '/Product/Index'; // Redirect to product page
}

// ✅ Attach Event Listeners
cartIcon.addEventListener('click', () => toggleCart(true));
closeCartBtn.addEventListener('click', () => toggleCart(false));
continueShoppingBtn.addEventListener('click', continueShopping);

// ✅ Add to Cart Functionality
document.addEventListener('click', (event) => {
    const target = event.target;

    if (target.classList.contains('add-to-bag')) {
        const productId = target.dataset.productId;
        const productName = target.dataset.productName;
        const productPrice = parseFloat(target.dataset.productPrice);
        const productImage = target.dataset.productImage;

        if (!cart[productId]) {
            cart[productId] = { name: productName, price: productPrice, image: productImage, quantity: 1 };
        } else {
            cart[productId].quantity++;
        }

        sessionStorage.setItem('cart', JSON.stringify(cart)); // Save cart
        updateCartUI();
        toggleCart(true);
    }

    if (target.classList.contains('decrement') || target.classList.contains('increment')) {
        const productId = target.closest('.cart-item').dataset.productId;
        const change = target.classList.contains('increment') ? 1 : -1;
        updateQuantity(productId, change);
    }

    if (target.classList.contains('delete-icon')) {
        const productId = target.closest('.cart-item').dataset.productId;
        removeFromBag(productId);
    }
});

// ✅ Update Quantity Function
function updateQuantity(id, change) {
    if (cart[id]) {
        cart[id].quantity = Math.max(1, cart[id].quantity + change);
        sessionStorage.setItem('cart', JSON.stringify(cart)); // Save updated cart
        updateCartUI();
    }
}

// ✅ Remove Product from Cart
function removeFromBag(id) {
    delete cart[id];
    sessionStorage.setItem('cart', JSON.stringify(cart)); // Save updated cart
    updateCartUI();
}

// ✅ Update Cart UI Function
function updateCartUI() {
    cartBody.innerHTML = '';
    let totalAmount = 0;

    Object.entries(cart).forEach(([id, product]) => {
        totalAmount += product.price * product.quantity;

        cartBody.innerHTML += `
            <div class="cart-item" data-product-id="${id}">
                <img src="${product.image}" alt="${product.name}" />
                <div class="cart-item-details">
                    <p>${product.name}</p>
                    <p>₹${(product.price * product.quantity).toFixed(2)}</p>
                    <div class="quantity-controls">
                        <button class="decrement">-</button>
                        <span class="cart-item-quantity">${product.quantity}</span>
                        <button class="increment">+</button>
                    </div>
                </div>
                <i class="fa-solid fa-trash delete-icon"></i>
            </div>
        `;
    });

    totalAmountEl.textContent = `Total: ₹${totalAmount.toFixed(2)}`;

    // ✅ Show only the number of unique products in the cart
    const uniqueProductCount = Object.keys(cart).length;
    cartCountEl.textContent = uniqueProductCount;
    cartCountEl.classList.toggle('hidden', uniqueProductCount === 0);

    sessionStorage.setItem('cart', JSON.stringify(cart)); // Save cart
}

// ✅ Restore Cart on Page Load
document.addEventListener('DOMContentLoaded', () => {
    cart = JSON.parse(sessionStorage.getItem('cart')) || {}; // Load from storage
    updateCartUI();
});
