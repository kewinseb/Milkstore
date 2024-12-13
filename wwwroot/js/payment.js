document.addEventListener('DOMContentLoaded', () => {
    const paymentMethods = document.querySelectorAll('input[name="payment-method"]');
    const paymentForms = document.querySelectorAll('.payment-form');
    const errorMessage = document.getElementById('error-message');

    // Hide all forms on page load
    paymentForms.forEach(form => form.style.display = 'none');

    if (paymentMethods.length > 0) {
        paymentMethods.forEach(method => {
            method.addEventListener('change', function () {
                // Hide all forms
                paymentForms.forEach(form => form.style.display = 'none');

                // Show the selected form
                const selectedForm = document.getElementById(this.id + '-form');
                if (selectedForm) selectedForm.style.display = 'block';
            });
        });
    } else {
        console.warn('No payment methods or forms found.');
    }

    // Validation function
    window.validateForm = function () {
        errorMessage.textContent = ''; // Clear previous errors
        const selectedMethod = document.querySelector('input[name="payment-method"]:checked');
        if (!selectedMethod) {
            errorMessage.textContent = 'Please select a payment method.';
            return;
        }

        const form = document.getElementById(selectedMethod.id + '-form');
        if (!form) {
            errorMessage.textContent = 'An error occurred. Please try again.';
            return;
        }

        const inputs = form.querySelectorAll('input[required]');
        for (let input of inputs) {
            if (input.value.trim() === '') {
                errorMessage.textContent = 'Please fill in all required fields.';
                return;
            }
        }

        // Additional validation or security measures can go here
        errorMessage.textContent = '';
        alert('Payment Submitted Successfully');
    };

    // Cancel payment function
    window.cancelPayment = function () {
        if (window.history.length > 1) {
            window.history.back();
        } else {
            alert('No previous page to navigate back to.');
        }
    };
});