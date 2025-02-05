document.addEventListener('DOMContentLoaded', () => {
    const statusPoints = document.querySelectorAll('.status-point');
    const progressLine = document.querySelector('.progress-line');
    const trackingItems = document.querySelectorAll('.tracking-item');
    const detailModal = document.getElementById('detailModal');
    const modalTitle = document.getElementById('modalTitle');
    const modalDescription = document.getElementById('modalDescription');
    const closeModal = document.querySelector('.close-modal');

    // Ensure elements exist before adding event listeners
    const editShipment = document.getElementById('editShipment');
    const printPackingSlip = document.getElementById('printPackingSlip');

    if (editShipment) {
        editShipment.addEventListener('click', () => alert('Edit shipment clicked'));
    } else {
        console.warn("Element with ID 'editShipment' not found.");
    }

    if (printPackingSlip) {
        printPackingSlip.addEventListener('click', () => alert('Print packing slip clicked'));
    } else {
        console.warn("Element with ID 'printPackingSlip' not found.");
    }

    const updateTrackingProgress = () => {
        const activePoints = 2;
        statusPoints.forEach((point, index) => {
            if (index < activePoints) {
                point.classList.add('active');
            }
        });
        if (progressLine) {
            progressLine.style.width = `${(activePoints - 1) * 25}%`;
        }
    };

    trackingItems.forEach(item => {
        item.addEventListener('click', () => {
            if (detailModal && modalTitle && modalDescription) {
                const details = item.getAttribute('data-details');
                const location = item.querySelector('div:first-child')?.textContent || "Unknown";
                modalTitle.textContent = location;
                modalDescription.textContent = details;
                detailModal.style.display = 'block';
            }
        });
    });

    statusPoints.forEach((point) => {
        point.addEventListener('click', () => {
            if (detailModal && modalTitle && modalDescription) {
                const stage = point.getAttribute('data-stage');
                modalTitle.textContent = stage;
                modalDescription.textContent = `Current stage of your order: ${stage}`;
                detailModal.style.display = 'block';
            }
        });
    });

    if (closeModal) {
        closeModal.addEventListener('click', () => {
            if (detailModal) detailModal.style.display = 'none';
        });
    }

    window.addEventListener('click', (event) => {
        if (detailModal && event.target == detailModal) {
            detailModal.style.display = 'none';
        }
    });

    updateTrackingProgress();
});
