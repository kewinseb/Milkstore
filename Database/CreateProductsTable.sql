-- Create the Products table
CREATE TABLE Products (
    productId INT IDENTITY(1000, 1) PRIMARY KEY,
    productName VARCHAR(50) NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    stockQuantity INT NOT NULL,
    estimatedDelivery DATETIME2(7) NOT NULL,
    category VARCHAR(50) NOT NULL,
    createdAt DATETIME2(7) NOT NULL DEFAULT GETDATE(),
    updatedAt DATETIME2(7) NOT NULL DEFAULT GETDATE(),
    productImage NVARCHAR(255) NOT NULL
);
 
-- Insert dummy values into the Products table
INSERT INTO Products (productName, price, stockQuantity, estimatedDelivery, category, createdAt, updatedAt, productImage)
VALUES
('Aavin Toned Milk 500ml', 25.00, 150, '2025-01-16 09:00:00.0000000', 'Dairy - Aavin (Blue)', GETDATE(), GETDATE(), '/images/aavin-toned-milk-500ml.jpg'),
('Aavin Full Cream Milk 1L', 60.00, 100, '2025-01-17 10:00:00.0000000', 'Dairy - Aavin (Purple)', GETDATE(), GETDATE(), '/images/aavin-full-cream-milk-1l.jpg'),
('Thirumala Skimmed Milk 500ml', 22.00, 200, '2025-01-16 11:00:00.0000000', 'Dairy - Thirumala (Green)', GETDATE(), GETDATE(), '/images/thirumala-skimmed-milk-500ml.jpg'),
('Thirumala Standard Milk 1L', 55.00, 120, '2025-01-17 09:30:00.0000000', 'Dairy - Thirumala (Orange)', GETDATE(), GETDATE(), '/images/thirumala-standard-milk-1l.jpg'),
('Aavin Butter Milk 200ml', 12.00, 300, '2025-01-16 08:00:00.0000000', 'Dairy - Aavin (White)', GETDATE(), GETDATE(), '/images/aavin-butter-milk-200ml.jpg');