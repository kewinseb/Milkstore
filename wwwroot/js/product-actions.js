let cart = JSON.parse(sessionStorage.getItem('cart')) || []; // Retrieve cart from session storage or initialize as an array

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
blurOverlay.addEventListener('click', () => toggleCart(false)); // ✅ Close cart when clicking on overlay

// Delegate event handling to the document for buttons (better performance)
document.addEventListener('click', (event) => {
    const target = event.target;

    // Add/Remove Product Button
    if (target.classList.contains('add-to-bag') || target.classList.contains('remove-from-bag')) {
        const productId = target.dataset.productId;
        const productName = target.dataset.productName;
        const productPrice = parseFloat(target.dataset.productPrice);
        const productImage = target.dataset.productImage;

        const existingProduct = cart.find(item => item.id === productId);

        if (!existingProduct) {
            addToBag(productId, productName, productPrice, productImage);
        } else {
            removeFromBag(productId);
        }

        toggleCart(true); // Ensure cart opens when an item is added
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

// Function to Add Product to Cart (stored in an array)
function addToBag(id, name, price, image) {
    const existingProduct = cart.find(item => item.id === id);

    if (existingProduct) {
        existingProduct.quantity++;
    } else {
        cart.push({ id, name, price, image, quantity: 1 });
    }

    updateCartUI();
    updateProductButton(id, true);

    fetch('/ProductController/AddToCart', {  // Corrected path if needed
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            ProductProductId: id, // Correct casing
            CreatedAt: new Date().toISOString(), // Correct casing and format for dates
            UpdatedAt: new Date().toISOString(),
            Quantity: cart.find(item => item.id === id).quantity,// Get correct quantity
        })
    })
        .then(response => response.json())
        .then(data => console.log(data.Message))
        .catch(error => console.error('Error:', error));

    updateSessionStorage(); // Move this *after* the fetch
}

// Function to Remove Product from Cart
function removeFromBag(id) {
    cart = cart.filter(product => product.id !== id);
    updateSessionStorage();
    updateCartUI();
    updateProductButton(id, false);

    fetch('/ProductController/RemoveFromCart', {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(id) // Passing the integer directly
    })
        .then(response => response.json())
        .then(data => console.log(data.Message))
        .catch(error => console.error('Error:', error));
}

// Function to Update Quantity
function updateQuantity(id, change) {
    const product = cart.find(item => item.id === id);
    if (product) {
        product.quantity = Math.max(1, product.quantity + change); // Prevent quantity from going below 1
        //if (product.quantity === 1 && change === -1) {  // If decrementing from 1, remove the item
        //    removeFromBag(id);
        //    return; // Exit the function to prevent further updates
        //}
        updateSessionStorage();
        updateCartUI();

        fetch('/ProductController/UpdateCartQuantity', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                ProductProductId: id,
                Quantity: product.quantity
            })
        })
            .then(response => response.json())
            .then(data => console.log(data.Message))
            .catch(error => console.error('Error:', error));
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
    let totalAmount = 0;
    let totalQuantity = 0;

    cart.forEach(product => {
        totalAmount += product.price * product.quantity;
        totalQuantity += product.quantity;

        const cartItem = document.createElement('div');
        cartItem.className = 'cart-item';
        cartItem.dataset.productId = product.id;

        cartItem.innerHTML = `
            <img src="${product.image}" alt="${product.name}" />
            <div class="cart-item-details">
                <p class="cart-item-title">${product.name}</p>
                <p>Price: ₹${(product.price * product.quantity).toFixed(2)}</p>
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

    cartCountEl.textContent = cart.length; // Number of unique items

    cartCountEl.classList.toggle('hidden', cart.length === 0);
    cartCountEl.classList.toggle('jello-animation', cart.length > 0); // Jello animation logic

    if (cart.length === 0) {
        cartBody.innerHTML = `
            <section class="page_404">
                <div class="four_zero_four_bg"></div>
                <div class="content_box">
                    <h3 class="h2">
                        Sorry, No products in the cart 🛒!
                    </h3>
                </div>
            </section>
        `;
    } else {
        cartCountEl.classList.add('jello-animation');
        setTimeout(() => {
            cartCountEl.classList.remove('jello-animation');
        }, 1000);
    }
}

function updateSessionStorage() {
    sessionStorage.setItem('cart', JSON.stringify(cart));
}

document.addEventListener('DOMContentLoaded', updateCartUI);