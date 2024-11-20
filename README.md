# Milkstore Project 🚚🥛

Welcome to **Milkstore**, a web-based application for managing milk delivery operations. This application helps you handle customers, products, orders, and deliveries efficiently.  

---

## 📋 Table of Contents

- [About the Project](#about-the-project)  
- [Technologies Used](#technologies-used)  
- [Getting Started](#getting-started)  
  - [Prerequisites](#prerequisites)  
  - [Installation](#installation)  
- [Database Configuration](#database-configuration)  
- [Usage](#usage)  
- [License](#license)  

---

## 📖 About the Project  

**Milkstore** is built using the ASP.NET Core MVC framework with the **Entity Framework Database-First** approach. It uses **Microsoft SQL Server** for data storage and provides an intuitive interface for handling everyday milk delivery tasks.  

Key Features:
- **Customer Management**: Add, update, and view customer information.  
- **Product Tracking**: Manage milk products and inventory.  
- **Order Processing**: Handle customer orders seamlessly.  
- **Delivery Scheduling**: Plan and track deliveries.  

---

## 💻 Technologies Used  

- **Backend**: ASP.NET Core MVC  
- **Frontend**: Razor Views (HTML, CSS, JavaScript)  
- **Database**: Microsoft SQL Server  
- **ORM**: Entity Framework (Database-First Approach)  

---

## 🚀 Getting Started  

### Prerequisites  
- .NET SDK latest 
- Microsoft SQL Server and Mangement studio 
- Visual Studio 2022

### Installation  

1. **Clone the repository**  
   Clone the repository from the main branch:  
   ```bash
   git clone "https://github.com/<your-username>/milkstore.git"

2. **Create a feature branch**  
   Create a new branch for the module you are working on:  
   ```bash
   git checkout -b "feature/Your_Module_Name"

3. **Open the project**
Open the solution in Visual Studio.

4. **Restore NuGet Packages**  
   Run the following command in the terminal:
   ```bash
   dotnet restore

5. **Configure the Connection String**  
   Update the `appsettings.json` file with your database connection string under `MilkstoreDbConnectionString`.  
   Example:
   ```json
   "ConnectionStrings": {
     "MilkstoreDbConnectionString": "Server=DESKTOP-HHOTP7Q;Database=Milkstore;Trusted_Connection=True;TrustServerCertificate=Yes"
   }

6. **Run the Application**  
   After configuring the connection string, run the application with the following command:
   ```bash
   dotnet run

---

## 🗄️ Database Configuration

1. Open the `appsettings.json` file in the root of the project.
2. Update the `MilkstoreDbConnectionString` with your database connection string.

   Example:
   ```json
   "ConnectionStrings": {
     "MilkstoreDbConnectionString": "Server=DESKTOP-HHOTP7Q;Database=Milkstore;Trusted_Connection=True;TrustServerCertificate=Yes"
   }

---

## 📚 Usage

1. Launch the application in your browser at `http://localhost:<port>`.
   
2. Use the web interface to manage customers, products, orders, and deliveries.

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.txt) file for details.
