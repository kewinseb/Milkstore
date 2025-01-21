document.addEventListener("DOMContentLoaded", function () {
    // Handle Add to Cart button click
    const cartButtons = document.querySelectorAll(".add-to-cart");

    cartButtons.forEach(button => {
        button.addEventListener("click", function () {
            const productId = this.getAttribute("data-product-id");
            const quantityControls = document.querySelector(`.quantity-controls[data-product-id="${productId}"]`);

            // Hide Add to Cart button and show quantity controls
            this.classList.add("hidden");
            quantityControls.classList.remove("hidden");
            quantityControls.classList.add("fade-in");
        });
    });

    // Handle quantity increment and decrement
    const quantityButtons = document.querySelectorAll(".quantity-controls button");
    quantityButtons.forEach(button => {
        button.addEventListener("click", function () {
            const input = this.parentElement.querySelector(".quantity");
            let currentValue = parseInt(input.value) || 0;
            if (this.classList.contains("increment")) {
                input.value = currentValue + 1;
            } else if (this.classList.contains("decrement") && currentValue > 0) {
                input.value = currentValue - 1;
            }
        });
    });
});