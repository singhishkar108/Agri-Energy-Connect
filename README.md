<div align="center">

<h1>🌱🌻 Agri-Energy Connect 🌄🚜</h1>

</div>

---

## 📑Table of Contents

🧭 1. [**Introduction**](#-1-introduction)<br>
🛠️ 2. [**Setting Up the Project Locally**](#️-2-setting-up-the-project-locally)<br>
✅ 3. [**Features and Functionality**](#-3-features-and-functionality)<br>
🔐 4. [**Farmer & Employee Authentication & Navigation**](#-4-farmer--employee-authentication--navigation)<br>
🏗️ 5. [**Architecture**](#️-5-architecture)<br>
👥 6. [**Author and Contributions**](#-6-author-and-contributions)<br>
⚖️ 7. [**MIT License**](#️-7-mit-license)<br>
❓ 8. [**Frequently Asked Questions (FAQ)**](#-8-frequently-asked-questions-faq)<br>
📚 9. [**References**](#-9-references)<br>

---

## 🧭 1. Introduction

The **Agri-Energy Connect Platform** is a **prototype web application** designed to **bridge the gap between sustainable agriculture and green energy solutions**. This platform serves as a digital ecosystem where farmers can **showcase their agricultural products** and employees can manage farmer profiles and monitor product data efficiently. Developed using **Visual Studio Code and C#** in the **ASP.NET MVC framework**, the platform demonstrates key enterprise system characteristics such as scalability, usability, security, and maintainability.

### Key Features:

- **User Role Management**: Secure login functionality with two distinct roles** — **Farmers and Employees\*\* — ensuring role-based access and operations.
- **Product and Farmer Management**: Farmers can add and view their own products, while employees can register new farmers and filter product listings based on category or date range.
- **Relational Database Integration**: A SQL Server database stores and manages structured data related to users and products.
- **User-Friendly Interface**: A responsive and intuitive web UI to facilitate ease of use across various devices.

---

## 🛠️ 2. Setting Up the Project Locally

### Prerequisites:

To successfully compile and run this project, you must have the following installed on your system:

- **Operating Systems**: Any OS compatible with the .NET 8.0 Runtime and the corresponding SDK. This generally includes modern versions of Windows (Windows 10/11), macOS 10.15+, or Linux distributions that support the .NET 8 framework.
- **IDE**: Compatible version of Microsoft Visual Studio 2019+ (or an equivalent IDE like VS Code with extensions, such as C# Dev Kit).
- **Version Control**: Git for cloning the repository.
- **Database**: SQL Server instance (either local or remote) is necessary to integrate with the main data store.
- **Frameworks**:
  - Target Framework: .NET 8.0 (net8.0)
  - Web Framework: ASP.NET Core 8.0
- **RAM**: Minimum 4GB
- **Disk Space**: Minimum 200MB free space
- **Dependencies**:
  - CMCS.csproj:
    - Firebase.Auth (Version: 1.0.0)
    - Microsoft.EntityFrameworkCore.Design (Version: 8.0.4)
    - Microsoft.EntityFrameworkCore.SqlServer (Version: 8.0.4)
    - NuGet.Common (Version: 6.13.2)

### Project Configurations

#### `AgriEnergy.csproj`

This configuration defines the project as an **ASP.NET Core web application** targeting the **latest framework version**.

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Firebase.Auth" Version="1.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.4">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.4" />
    <PackageReference Include="NuGet.Common" Version="6.13.2" />
  </ItemGroup>

</Project>
```

#### `appsettings.json`

This configuration stores **connection strings**, **custom settings**, and **logging configuration**, which are loaded at runtime.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

### Installation

Follow these steps to get the application running on your local machine.

#### 1. Clone the Repository

- Naviagte and click the green "**Code**" button at the top of this repository.
- Copy the URL under the "**HTTPS**" tab (`https://github.com/singhishkar108/Agri-Energy-Connect.git`).
- Navigate to the directory where you want to save the project (e.g., `cd Documents/Projects`).
- Open your preferred terminal or command prompt and use the following command to clone the project:

```bash
git clone https://github.com/singhishkar108/Agri-Energy-Connect.git
```

- This will create a new folder with the repository's name and download all the files and the entire history into it.
- Alternatively, you may download as a **ZIP file** or clone using **GitHub Desktop**.

#### 2. Open in Visual Studio (Recommended)

1.  Open **Visual Studio 2022**.
2.  Navigate to **File \> Open \> Project/Solution**.
3.  Browse to the cloned repository and select the **Solution file (.sln)** to load the project.
4.  Visual Studio will automatically perform a package restore (`dotnet restore`).

The application will launch. You should see a message in the console indicating the application is running. The browser should open automatically to the default URL.

#### 3. Add Your Firebase API Key

- The authentication system requires your own **Firebase API key**.
- You must add this in your configuration (e.g., appsettings.json, environment variables, or injected configuration) before running the application.

Example placeholder:

```json
"Firebase": {
  "ApiKey": "YOUR_FIREBASE_API_KEY_HERE"
}
```

- Your placeholder value will be inserted in `Controllers/AuthController`.
- Make sure your controllers or services read this value properly.

> ❌ Do not commit real API keys to GitHub

#### 4. Configure Database Connection

The application connects to a **SQL database**. You must configure the connection string in the `appsettings.json` file. Create this file if it doesn't exist, and add the configuration using a placeholder structure.

> ⚠️ **Note**: If you are running locally, you will typically use a connection string pointing to a local SQL Server instance (e.g., using LocalDB).

#### 5. Apply Database Migrations

Use the **Entity Framework Core tools** to create the database schema based on the code's models. Run these commands from the main project directory (`AgriEnergy`):

```bash
# Update the database to the latest migration
dotnet ef database update
```

This command will create the `AgriEnergyDB` database (if it doesn't exist) and apply all necessary tables.

### Running

#### 1. Run in Visual Studio

1.  Select **Build \> Build Solution** (or press `F6`) to compile the project.
2.  Click the **Run** button (or press `F5`) to start the application with debugging, or `Ctrl+F5` to start without debugging.

#### 2. Run via Command Line (Alternative)

If you are using **Visual Studio Code** or prefer the CLI:

1.  Navigate to the project directory containing the `.csproj` file.
2.  Execute the following commands in sequence:

```bash
# Clean up any previous build files
dotnet clean

# Restore project dependencies
dotnet restore

# Build and run the application
dotnet run
```

#### 4. Access the Application

- The console output will indicate the local URL where the application is hosted (e.g., `https://localhost:7198`).
- Open your web browser and navigate to the displayed URL (e.g., `https://localhost:7198`). You should now see the **AgriEnergy home page**.

---

## ✅ 3. Features and Functionality

### 1. Database Development and Integration

- Design and integrate a **relational database** to manage information about farmers and their products.
- Populate the database with **sample data** to simulate real-world scenarios, ensuring the demonstration is robust and comprehensive.

### 2. User Role Definition and Authentication System

- **Two user roles** within the system:
  - **Farmer**: Can add products to their profile and view their own product listings.
  - **Employee**: Can add new farmer profiles, view all products from specific farmers, and use filters for product searching.
- Implemented a **secure login functionality** with **authentication mechanisms** to protect user data and ensure **role-specific access**.

### 3. Functional Features for Farmers and Employees

- **For Farmers**:
  - **Product addition feature** where farmers can add new products with details like name, category, and production date.
- **For Employees**:
  - Ability to add **new farmer profiles** with essential details.
  - Capability to **view and filter** a comprehensive list of products from any farmer based on criteria such as **date range** and **product type**.

---

## 🔐 4. Farmer & Employee Authentication & Navigation

### 1. Registration

- **Farmers** will have to be **invited via Gmail** to register for Agri-Energy Connect.
- The invitation email will originate from: `agrienergyconnect63@gmail.com`.
- Both **Farmers** and **Employees** will have to enter mandatory details: **email address**, **password**, **confirm password**, **name**, **date of birth (DoB)**, **bio**, etc.

### 2. Login

- Upon launching the application, the user will be greeted to the home page.
- Users (**Farmers & Employee**) may then navigate to the "**Sign In**" page to log in.

### 3. Navigating Various Features

- **Farmers** can browse existing products by navigating to the **Category tab**.
- **Farmers** may view all the products in the marketplace and add new products via '**Create New**'. This action will add products to the marketplace, as well as to their profile.
- Users may view their products, as well as other users' products, under the '**Our Suppliers**' tab.

### 4. Employee Only Features

- **Employees** may also view products from specific farmers under the '**Our Suppliers Page**'. This can be **filtered** using **Search**, **Category**, and **Date Range**.
- **Employees** may change **Farmers** to **Employees** (and vice versa), as well as **add new farmers via Gmail**, under the '**Add Farmers**' page.

---

## 🏗️ 5. Architecture

### Application Structure (ASP.NET Core MVC)

The application code adheres to the **MVC pattern**, which ensures a clear separation of concerns, making the codebase maintainable, testable, and scalable.

- **Model**: This layer manages the application's data and business logic. It includes the Entity Framework Core data context, the entity classes (e.g., Product, Order), and the service classes responsible for interacting with the database and external Azure APIs.
- **View**: The user interface (UI) is rendered using Razor views. This layer is responsible solely for presenting the data to the client and capturing user input.
- **Controller**: Controllers act as the entry point for handling user requests. They receive input, coordinate the necessary actions by calling methods in the Model layer, and determine which View to return to the user.

---

## 👥 6. Author and Contributions

### Primary Developer:

- I, **_Ishkar Singh_**, am the sole developer and author of this project:
  Email (for feedback or concerns): **ishkar.singh.108@gmail.com**

### Reporting Issues:

- If you encounter any bugs, glitches, or unexpected behaviour, please open an Issue on the GitHub repository.
- Provide as much detail as possible, including:
  - Steps to reproduce the issue
  - Error messages (if any)
  - Screenshots or logs (if applicable)
  - Expected vs. actual behaviour
- Clear and descriptive reports help improve the project effectively.

### Proposing Enhancements:

- Suggestions for improvements or feature enhancements are encouraged.
- You may open a Discussion or submit an Issue describing the proposed change.
- All ideas will be reviewed and considered for future updates.

---

## ⚖️ 7. MIT License

**Copyright © 2025 Ishkar Singh**<br>

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

---

## ❓ 8. Frequently Asked Questions (FAQ)

### 1. What software and knowledge do I need to run the Agri-Energy Connect project locally?

To run **AgriEnergy** locally, you'll need:

- **Visual Studio 2019 or later** or **Visual Studio Code** with extensions, such as C# Dev Kit installed on your system.
- **.NET Core 8 or later**.
- **Basic knowledge of the C# programming language**.

### 2. What steps should I take if the application becomes unresponsive?

If the application becomes unresponsive or freezes, try the following actions:

1. **Close and relaunch the application**:

   - This resolves most **temporary UI freezes** or non-critical memory handling issues.

2. **Check system resource usage**:

   - Open the **Task Manager** (Windows: `Ctrl + Shift + Esc`) and verify whether your system is experiencing high **CPU, memory, or disk usage**.
   - Look for any **background processes**—especially **.NET applications** or heavy programs—that may be interfering with the app’s performance.

3. **Confirm your system meets requirements**:

   - Ensure your device meets the **minimum runtime requirements** for the application, including the correct version of **.NET**.

4. **Restart your system if needed**:
   - If the application continues to freeze, performing a **full system restart** may help clear locked processes or resource conflicts.

- If the issue persists after these steps, please submit a **detailed report** via the **GitHub Issues page**.

### 3. How should I report a bug or unexpected application behaviour?

1. Thank you for helping improve the application. To report a bug:

   - **Open an Issue on GitHub**: Go to the repository’s **Issues tab** and **create a new issue**.

2. **Provide detailed information**:

- Include, where possible:

  - A **clear description** of the problem
  - **Step-by-step instructions** on how to reproduce the issue
  - **Expected behavior vs. actual behavior**
  - **Screenshots, console output, or error logs** (if applicable)
  - Your **system information** (operating system, .NET version, etc.)

3. **Use clear and descriptive titles**:
   - This helps **categorize and prioritize issues** efficiently.
   - The more detail provided, the easier it will be to diagnose and resolve the problem.

### 4. What should I do if the application fails to launch?

If the application fails to start, consider the following troubleshooting steps:

1. **Verify .NET Framework installation**:

   - Ensure that **.NET Framework 8** is installed on your system.
   - You may download the required runtime from **Microsoft’s official website** if necessary.

2. **Check the build integrity** (for source code users):

   - If running from the source code, ensure that the project **builds successfully** in your IDE with **no compilation errors**.
   - Confirm that all project dependencies and **NuGet packages** are properly **restored**.

3. **Ensure Firebase API key is present and file integrity**:

   - Ensure the **API key** is safely and correctly implemented in the application and in `AuthController`.
   - Make sure no essential application files are **missing or corrupted**. If unsure, **re-download the application or clone the repository again**.

4. **Run as administrator** (if required):

   - Certain systems may restrict application execution. Right-click the executable and select **Run as administrator**.

5. **Verify `launchSettings.json` and `appsettings.json`**:
   - Check the `launchSettings.json` file and ensure the correct application URL and launch profile are configured. Review `appsettings.json` to confirm that all required configuration keys, especially those for connection strings and API endpoints, are present and hold valid values.

- If the application still does not launch, please **report the issue through GitHub** with system details and any error messages received.

---

## 📚 9. References

- **Bro Code, n.d. C# Full Course for free.** [online] _[youtube.com](https://www.youtube.com/watch?v=wxznTygnRfQ)_ [Accessed 9 April 2025].
- **Code A Future, n.d. How to Consume APIs in ASP.NET Core MVC (Step by Step Project).** [online] _[youtube.com](https://youtu.be/oM568qeQ0wE?si=BK9mLai2OkS3fJ5J)_ [Accessed 14 April 2025].
- **rawpixel.com, n.d. Agriculture Illustration (Image).** [online] _[rawpixel.com](https://images.rawpixel.com/image_1300/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTA2L3YxMTYtYWdyaWN1bHR1cmUtMDJlXzEuanBn.jpg)_ [Accessed 11 May 2025].
- **SoftsWeb, n.d. API Authentication EXPLAINED! 🔐 OAuth vs JWT vs API Keys 🚀.** [online] _[youtube.com](https://youtu.be/GcVtElYa17s?si=Ck-wwd8DZ5T46hlx)_ [Accessed 16 April 2025].
- **Sustainer A Solutions, n.d. Sustainable Farming Image (WebP).** [online] _[sustainerasolutions.com](https://www.sustainerasolutions.com/images/917f2036-f069-4ad6-8b48-69e532b5afd8-pexels-pixabay-259280.webp)_ [Accessed 29 May 2025].
- **Unsplash, n.d. Aerial View of Farmland (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1471193945509-9ad0617afabf?q=80&w=1770&auto=format&fit=crop)_ [Accessed 17 May 2025].
- **Unsplash, n.d. Agriculture Workers (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1511117833895-4b473c0b85d6?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 10 June 2025].
- **Unsplash, n.d. Automated Solutions Image (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1662486777908-4c09a3f61c91?q=80&w=1476&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 24 June 2025].
- **Unsplash, n.d. Beetroot (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1639402480805-ea8ef529e028?q=80&w=1374&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 13 May 2025].
- **Unsplash, n.d. Carrots (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1598170845058-32b9d6a5da37?q=80&w=1374&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 28 May 2025].
- **Unsplash, n.d. Combine Harvester (Photo).** [online] _[unsplash.com](https://plus.unsplash.com/premium_photo-1661877153606-b5e74a491615?q=80&w=1472&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 22 May 2025].
- **Unsplash, n.d. Compost (Photo).** [online] _[unsplash.com](https://plus.unsplash.com/premium_photo-1680125265832-ffaf364a8aca?q=80&w=1374&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 20 June 2025].
- **Unsplash, n.d. Crop Field with Tractor (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1532601224476-15c79f2f7a51?q=80&w=1770&auto=format&fit=crop)_ [Accessed 5 May 2025].
- **Unsplash, n.d. Drone Farming View (Photo).** [online] _[unsplash.com](https://plus.unsplash.com/premium_photo-1681965616064-07c4441b384e?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 18 April 2025].
- **Unsplash, n.d. Farm Landscape (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1605000797499-95a51c5269ae?q=80&w=1471&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 7 June 2025].
- **Unsplash, n.d. Farmer with Crops (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1464226184884-fa280b87c399?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 20 April 2025].
- **Unsplash, n.d. Farming Equipment (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1614977645540-7abd88ba8e56?q=80&w=1373&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 19 May 2025].
- **Unsplash, n.d. Fertilizer (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1709633644385-e4e51e5646f1?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 1 May 2025].
- **Unsplash, n.d. Fruits (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1464965911861-746a04b4bca6?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 4 May 2025].
- **Unsplash, n.d. Green Energy Solutions Image (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1451847251646-8a6c0dd1510c?q=80&w=1632&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 15 May 2025].
- **Unsplash, n.d. Green Meadow (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1596733430284-f7437764b1a9?q=80&w=1770&auto=format&fit=crop)_ [Accessed 21 May 2025].
- **Unsplash, n.d. Potatoes (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1635774855536-9728f2610245?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 25 June 2025].
- **Unsplash, n.d. Robotic Digger (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1630267693768-824c7b5aaa1b?q=80&w=1374&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 2 June 2025].
- **Unsplash, n.d. Solar Panels (Photo).** [online] _[unsplash.com](https://plus.unsplash.com/premium_photo-1678864963908-5fa7d014159b?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 12 May 2025].
- **Unsplash, n.d. Strawberries (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1588165171080-c89acfa5ee83?q=80&w=1374&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 6 June 2025].
- **Unsplash, n.d. Tomatoes (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1582284540020-8acbe03f4924?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 25 April 2025].
- **Unsplash, n.d. Unspecified Crop Field (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1588152850700-c82ecb8ba9b1?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 19 June 2025].
- **Unsplash, n.d. Vegetables (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1597362925123-77861d3fbac7?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 3 May 2025].
- **Unsplash, n.d. Vineyard/Orchard (Photo).** [online] _[unsplash.com](https://images.unsplash.com/photo-1427434846691-47fc561d1179?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)_ [Accessed 13 June 2025].
- **Vecteezy, n.d. Agro Farm Logo Vector Art.** [online] _[vecteezy.com](https://www.vecteezy.com/vector-art/53953931-agro-farm-logo)_ [Accessed 27 May 2025].
