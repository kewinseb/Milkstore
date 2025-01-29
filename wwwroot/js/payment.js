document.addEventListener('DOMContentLoaded', () => {
    const paymentMethods = document.querySelectorAll('input[name="payment-method"]');
    const paymentForms = document.querySelectorAll('.payment-form');
    const errorMessage = document.getElementById('error-message');
    const submitButton = document.querySelector('.submit-button');

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

                // Clear error message when a method is selected
                errorMessage.textContent = '';
            });
        });
    } else {
        console.warn('No payment methods or forms found.');
    }

    // Submit button click event
    if (submitButton) {
        submitButton.addEventListener('click', async function (e) {
            e.preventDefault(); // Prevent default form submission
            await submitPayment();
        });
    }

    // Function to validate and submit the payment form
    async function submitPayment() {
        errorMessage.textContent = ''; // Clear previous errors
        const selectedMethod = document.querySelector('input[name="payment-method"]:checked');

        // Check if a payment method is selected
        if (!selectedMethod) {
            errorMessage.textContent = 'Please select a payment method.';
            return false;
        }

        // If payment method is "debit-credit", validate and send data
        if (selectedMethod.value === 'debit-credit') {
            const cardNumber = document.getElementById('card-number').value.trim();
            const cvv = document.getElementById('cvv').value.trim();

            // Validate card number and CVV
            if (!/^\d{16}$/.test(cardNumber)) {
                errorMessage.textContent = 'Invalid card number. Please enter exactly 16 digits.';
                return;
            }
            if (!/^\d{3}$/.test(cvv)) {
                errorMessage.textContent = 'Invalid CVV. Please enter exactly 3 digits.';
                return;
            }

            // Send the data to the server
            try {
                const response = await fetch('/PaymentController/SavePayment', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        cardNumber: cardNumber,
                        cvv: cvv,
                        paymentMethod: selectedMethod.value,
                    }),
                });

                if (response.ok) {
                    const result = await response.text();
                    alert(result); // Display success message
                } else {
                    const errorText = await response.text();
                    errorMessage.textContent = `Error: ${errorText}`;
                }
            } catch (error) {
                errorMessage.textContent = 'An error occurred while processing the payment.';
                console.error('Error:', error);
            }
        } else if (selectedMethod.value === 'cod') {
            alert('Cash on Delivery selected. Proceeding with the order...');
        }
    }

    // Cancel payment function
    window.cancelPayment = function () {
        if (window.history.length > 1) {
            window.history.back();
        } else {
            alert('No previous page to navigate back to.');
        }
    };
});

