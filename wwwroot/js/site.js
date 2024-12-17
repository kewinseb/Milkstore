// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Select all buttons globally
document.addEventListener("click", function (e) {
    if (e.target.tagName === "BUTTON") {
        const button = e.target;

        // Create ripple element
        const ripple = document.createElement("span");
        ripple.classList.add("ripple");

        // Calculate ripple position relative to the button
        const rect = button.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;

        ripple.style.left = `${x}px`;
        ripple.style.top = `${y}px`;

        // Append ripple to the button
        button.appendChild(ripple);

        // Remove ripple after animation ends
        ripple.addEventListener("animationend", () => {
            ripple.remove();
        });
    }
});
