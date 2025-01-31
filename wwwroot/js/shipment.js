document.addEventListener('DOMContentLoaded', () => {
    const statusPoints = document.querySelectorAll('.status-point');
    const progressLine = document.querySelector('.progress-line');
    const trackingItems = document.querySelectorAll('.tracking-item');
    const detailModal = document.getElementById('detailModal');
    const modalTitle = document.getElementById('modalTitle');
    const modalDescription = document.getElementById('modalDescription');
    const closeModal = document.querySelector('.close-modal');

    // Buttons click handlers
    document.getElementById('editShipment').addEventListener('click', () => alert('Edit shipment clicked'));
    document.getElementById('printPackingSlip').addEventListener('click', () => alert('Print packing slip clicked'));
    document.getElementById('refundOrder').addEventListener('click', () => alert('Refund Order clicked'));
    // document.getElementById('cancelOrder').addEventListener('click', () => alert('Cancel order clicked'));
    document.getElementById('cancelOrder').addEventListener('click', () => alert('Cancel order clicked'));
    document.getElementById('cancelOrder').addEventListener('click', () => {
        if (confirm("Are you sure you want to cancel this order?")) {
            window.location.href = "product.html"; // Change this to your actual product page URL
        }
    });



    // Update Tracking Progress
    const updateTrackingProgress = () => {
        const activePoints = 2; // Simulating 2 stages completed
        statusPoints.forEach((point, index) => {
            if (index < activePoints) {
                point.classList.add('active');
            }
        });
        progressLine.style.width = `${(activePoints - 1) * 25}%`;
    };

    // Tracking item click handler
    trackingItems.forEach(item => {
        item.addEventListener('click', () => {
            const details = item.getAttribute('data-details');
            const location = item.querySelector('div:first-child').textContent;

            modalTitle.textContent = location;
            modalDescription.textContent = details;
            detailModal.style.display = 'block';
        });
    });

    // Status point click handler
    statusPoints.forEach((point, index) => {
        point.addEventListener('click', () => {
            const stage = point.getAttribute('data-stage');
            modalTitle.textContent = stage;
            modalDescription.textContent = `Current stage of your order: ${stage}`;
            detailModal.style.display = 'block';
        });
    });

    // Close modal
    closeModal.addEventListener('click', () => {
        detailModal.style.display = 'none';
    });

    // Close modal when clicking outside
    window.addEventListener('click', (event) => {
        if (event.target == detailModal) {
            detailModal.style.display = 'none';
        }
    });

    // Initial progress update
    updateTrackingProgress();
});
