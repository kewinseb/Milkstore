document.addEventListener("DOMContentLoaded", function () {
    // Elements
    const cartCountElement = document.getElementById("cart-count");
    const cartItemsElement = document.getElementById("cart-items");

    // Mock cart data (In real-world scenarios, interact with backend API)
    let cart = [];

    // Update the cart UI (header count and popup)
    function updateCartUI() {
        // Calculate total quantity
        const totalQuantity = cart.reduce((total, item) => total + item.quantity, 0);
        cartCountElement.textContent = totalQuantity;

        // Update cart popup items
        cartItemsElement.innerHTML = cart.map(item => `
            <div class="cart-item">
                <img src="${item.image}" alt="${item.name}" class="cart-item-image">
                <div class="cart-item-details">
                    <span>${item.name}</span>
                    <span>Qty: ${item.quantity}</span>
                    <span>Total: ₹${(item.price * item.quantity).toFixed(2)}</span>
                </div>
            </div>
        `).join('');

        if (cart.length === 0) {
            cartItemsElement.innerHTML = '<p>Your cart is empty.</p>';
        }
    }

    // Handle Add to Cart button click
    const cartButtons = document.querySelectorAll(".add-to-cart");
    cartButtons.forEach(button => {
        button.addEventListener("click", function () {
            const productId = this.getAttribute("data-product-id");
            const quantityControls = document.querySelector(`.quantity-controls[data-product-id="${productId}"]`);
            const addToCartButton = this;

            // Show quantity controls and set default quantity to 1
            addToCartButton.classList.add("hidden");
            quantityControls.classList.remove("hidden");
            quantityControls.classList.add("fade-in");

            const quantityInput = quantityControls.querySelector(".quantity");
            quantityInput.value = 1;

            // Add product to the cart with quantity 1
            const productElement = this.closest(".product");
            const productName = productElement.querySelector("p").textContent;
            const productPrice = parseFloat(productElement.querySelector(".price").textContent.replace("₹", ""));
            const productImage = productElement.querySelector("img").src; // Fetch the product image

            // Add the product to the cart if it's not already there
            const existingProduct = cart.find(item => item.id === productId);
            if (!existingProduct) {
                cart.push({ id: productId, name: productName, price: productPrice, quantity: 1, image: productImage });
            }

            // Update the cart UI immediately after Add to Cart
            updateCartUI();
        });
    });

    // Handle quantity increment and decrement
    const quantityButtons = document.querySelectorAll(".quantity-controls button");
    quantityButtons.forEach(button => {
        button.addEventListener("click", function () {
            const productId = this.parentElement.getAttribute("data-product-id");
            const input = this.parentElement.querySelector(".quantity");
            let currentValue = parseInt(input.value) || 0;

            if (this.classList.contains("increment")) {
                input.value = currentValue + 1;

                // Update cart quantity
                const cartItem = cart.find(item => item.id === productId);
                if (cartItem) {
                    cartItem.quantity++;
                }
            } else if (this.classList.contains("decrement") && currentValue > 0) {
                input.value = currentValue - 1;

                // Update cart quantity
                const cartItem = cart.find(item => item.id === productId);
                if (cartItem && cartItem.quantity > 0) {
                    cartItem.quantity--;
                }

                // If quantity is 0, remove from cart and show Add to Cart button again
                if (cartItem.quantity === 0) {
                    cart = cart.filter(item => item.id !== productId);

                    // Hide quantity controls and show Add to Cart button
                    const addToCartButton = document.querySelector(`.add-to-cart[data-product-id="${productId}"]`);
                    this.parentElement.classList.add("hidden");
                    addToCartButton.classList.remove("hidden");
                }
            }

            // Update UI immediately after increment/decrement
            updateCartUI();
        });
    });

    // Initial UI update
    updateCartUI();
});
