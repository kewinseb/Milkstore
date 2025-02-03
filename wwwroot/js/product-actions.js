// Object to track the products and their quantities in the cart
let cart = {};

// Function to open the cart (popup)
function openCart() {
    document.getElementById('cartContainer').classList.add('active');
    document.getElementById('blurOverlay').style.display = 'block';
}

// Function to close the cart (popup)
function closeCart() {
    document.getElementById('cartContainer').classList.remove('active');
    document.getElementById('blurOverlay').style.display = 'none';
}

// Add event listener to the close button in the cart popup
document.getElementById('closeCartBtn').addEventListener('click', closeCart);

// Event listener for add/remove buttons on the product page
document.querySelectorAll('.add-to-bag').forEach(button => {
    button.addEventListener('click', function () {
        const productId = this.getAttribute('data-product-id');
        const productName = this.getAttribute('data-product-name');
        const productPrice = this.getAttribute('data-product-price');
        const productImage = this.getAttribute('data-product-image');

        if (this.classList.contains('add-to-bag')) {
            addToBag(productId, productName, productPrice, productImage, this);
            openCart();  // Open cart when item is added
        } else {
            removeFromBag(productId, productPrice, this);
        }
    });
});

// Function to add product to the cart (main page and popup)
function addToBag(id, name, price, image, button) {
    const cartBody = document.getElementById('cartBody');
    const totalElement = document.getElementById('total');
    const cartIconCount = document.getElementById('cart-count');

    // If the product already exists in the cart, update quantity
    if (cart[id]) {
        cart[id].quantity++;
    } else {
        // Otherwise, add the product to the cart object
        cart[id] = { name, price, image, quantity: 1 };
    }

    // Update header cart count (total quantity of all products)
    let totalQuantity = 0;
    for (const product in cart) {
        totalQuantity += cart[product].quantity;
    }
    cartIconCount.textContent = totalQuantity;

    // Update the cart slider (add/update item)
    updateCartSlider();

    // Update the button's text and class for removal
    button.classList.replace('add-to-bag', 'remove-from-bag');
    button.textContent = 'Remove from Bag';
}

// Function to remove product from the cart (main page and popup)
function removeFromBag(id, price, element) {
    const cartBody = document.getElementById('cartBody');
    const totalElement = document.getElementById('total');
    const cartIconCount = document.getElementById('cart-count');

    if (cart[id]) {
        // Decrease quantity or remove product if quantity is 0
        cart[id].quantity--;
        if (cart[id].quantity === 0) {
            delete cart[id];
        }
    }

    // Update header cart count (total quantity of all products)
    let totalQuantity = 0;
    for (const product in cart) {
        totalQuantity += cart[product].quantity;
    }
    cartIconCount.textContent = totalQuantity;

    // Update the cart slider (remove product if quantity is 0)
    updateCartSlider();

    // Update the button on the product page
    const productButton = document.querySelector(`button[data-product-id="${id}"]`);
    if (productButton) {
        productButton.classList.replace('remove-from-bag', 'add-to-bag');
        productButton.textContent = 'Add to Bag';
    }
}

// Function to update the cart slider with current products and their quantities
function updateCartSlider() {
    const cartBody = document.getElementById('cartBody');
    cartBody.innerHTML = ''; // Clear current cart items

    const totalElement = document.getElementById('total');
    let totalAmount = 0;

    // Loop through the cart object to display products and their quantities
    for (const id in cart) {
        const product = cart[id];
        const cartItem = document.createElement('div');
        cartItem.className = 'cart-item';
        cartItem.setAttribute('data-product-id', id);

        cartItem.innerHTML = `
            <img src="${product.image}" alt="${product.name}" />
            <div class="cart-item-details">
                <p class="cart-item-title">${product.name}</p>
                <p>Price: ₹${product.price}</p>
                <p>Quantity: <span class="cart-item-quantity">${product.quantity}</span></p>
            </div>
            <i class="fa-solid fa-trash delete-icon" onclick="removeFromBag('${id}', '${product.price}', this)" alt="Remove"></i>
        `;
        cartBody.appendChild(cartItem);

        totalAmount += product.price * product.quantity;
    }

    // Update the total amount in the cart
    totalElement.textContent = `Total: ₹${totalAmount.toFixed(2)}`;

    // Ensure quantity in the slider is updated after removal
    document.querySelectorAll('.cart-item').forEach(cartItem => {
        const productId = cartItem.getAttribute('data-product-id');
        const quantitySpan = cartItem.querySelector('.cart-item-quantity');
        if (cart[productId]) {
            quantitySpan.textContent = cart[productId].quantity;
        }
    });


    // Update the total amount in the cart
    totalElement.textContent = `Total: ₹${totalAmount.toFixed(2)}`;
}

// Event listener for the cart icon (in header)
document.getElementById('cartIcon').addEventListener('click', openCart);
