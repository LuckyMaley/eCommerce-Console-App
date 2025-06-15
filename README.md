# eCommerce-Console-App




## Description
Instant Order is an e-commerce system that offers customers quality clothes at low cost. Customers can navigate through various categories in the system to view different products catered to by Instant Order. Admins can manage products and customer orders.
The system will have a clear navigation menu for easy browsing through product categories. The system will have a fast and accurate search functionality, allowing customers to find products quickly. Moreover, the system will have filters and sorting options to refine search results based on price, brand, or category criteria.
The system will have well-organised product listings displaying key information such as product name, price, and availability. The system will include product ratings and reviews to help users make informed purchase decisions. Customers will be able to add items to their cart without navigating away from the product page, and they can add products to their Wishlist.
The system will integrate user accounts to store payment and shipping details for seamless checkout. Moreover, it will consist of a streamlined checkout process with minimal steps to complete an order. Customers and Admins will be able to view orders and manage shipping addresses from their account dashboard.

## Tools
![Static Badge](https://img.shields.io/badge/Visual%20Studio-2022%20or%20later-green) ![Static Badge](https://img.shields.io/badge/.Net%20Framework-6.0-blue)

## Branching Strategy

A structured branching strategy must be followed to keep the codebase organized:

- **main**: Stable, production-ready code.
- **dev**: Ongoing development. Feature branches are merged here first

- **feature/**: New features.
  - Example: `feature/add-user`
- **bugfix/**: Bug fixes.
  - Example: `bugfix/fix-user-order`
- **chore/**: Maintenance tasks, documentation,or configurations.
  - Example: `chore/add-read-me`
- **Hotfix branches**: Urgent fixes to `main`.
   - Example: `hotfix/critical-bug-in-production`

### Notes:
- Use **kebab-case** (lowercase with hyphens) for branch names (e.g., `feature/dotnet-make-payment`).
- Branch names should be **descriptive** but concise.
- Avoid spaces, uppercase letters, or special characters.

## Development Workflow

1. **Create a New Branch**:
   ```bash
   git checkout -b feature/add-user
   ```

2. **Make Changes**:
   ```bash
   git add .
   git commit -m "Implement add user feature"
   ```

3. **Push Your Branch**:
   ```bash
   git push origin feature/add-user
   ```

4. **Sync with the `dev` Branch**:
   Before creating a merge request, ensure your branch is up-to-date with the latest changes from the `dev` branch:
   ```bash
   git pull origin dev
   ```
   If there are any merge conflicts, resolve them in your branch locally. Once resolved, commit the changes and push them back to your branch:
   
   ```bash
   git push origin feature/add-user
   ```

5. **Create a Merge Request**:
   - Open a pull request on GitHub targeting `dev` for code review.

## Installation
Visual Studio 2022 - The primary IDE for developing, debugging, and deploying the system. Visual Studio provides a rich set of features, including code editing, version control integration, and project management tools.
Programming Language and Framework:
C# - The system is primarily developed using the C# programming language, leveraging its robust features, strong typing, and extensive .NET libraries.
.NET Framework 6.0 - This version of the .NET framework is utilised for building and running the system, offering enhanced performance, security, and language features.


 ### Steps

1. Go to the documents folder and then clone the project there (you can use git bash or any cmd):
```
cd Documents
```
```
git clone https://github.com/LuckyMaley/eCommerce-Console-App.git
```

2. Open the [project solution](/LLM_eCommerce_OOD3/LLM_eCommerce_OOD3.sln) on Visual Studio, and run the application.

## Visuals
![Screenshot (2339)](https://github.com/user-attachments/assets/f9e5a624-4add-452a-ba0a-1edb6764d095)

![Screenshot (2340)](https://github.com/user-attachments/assets/81487444-2167-4f7f-a910-8584fb2610e6)

![Screenshot (2341)](https://github.com/user-attachments/assets/e29f307f-854e-410e-8579-4d344097f388)

![Screenshot (2342)](https://github.com/user-attachments/assets/35658f98-c9b2-489a-bef6-82793f5259bd)

![Screenshot (2343)](https://github.com/user-attachments/assets/08525b7a-3545-4323-a193-5b5157cc2c2c)

![Screenshot (2344)](https://github.com/user-attachments/assets/3419550e-218a-4680-a40e-97f4dddbf942)

![Screenshot (2345)](https://github.com/user-attachments/assets/d3ce3351-3c2f-401f-9704-ab6478ebda17)

![Screenshot (2346)](https://github.com/user-attachments/assets/578f7fcf-c3d3-4b55-9de0-ccd9132a9f9a)

![Screenshot (2347)](https://github.com/user-attachments/assets/093cb751-c3cd-4934-884c-3a4ce63e4609)

![Screenshot (2348)](https://github.com/user-attachments/assets/5648191f-034f-41f8-9234-9fd265dfce32)

![Screenshot (2349)](https://github.com/user-attachments/assets/1c94e6d3-88e0-4c6d-84b9-a21fd7d7b59d)

![Screenshot (2350)](https://github.com/user-attachments/assets/9ea46f67-865e-4ff6-88ec-b53702c5ec1e)

![Screenshot (2351)](https://github.com/user-attachments/assets/9e9465d5-41ed-4b7f-ba96-350bedef1300)

![Screenshot (2352)](https://github.com/user-attachments/assets/b5f2b462-e62b-4af4-91e2-bf230ec0daee)

![Screenshot (2353)](https://github.com/user-attachments/assets/6da95b92-1613-4c7e-9185-2033ff9c613a)

![Screenshot (2354)](https://github.com/user-attachments/assets/1bb39f82-cf54-43cd-856a-8ffde852cb4a)

![Screenshot (2355)](https://github.com/user-attachments/assets/6dc49d43-a5a5-4e8e-8f71-3d1639a0e81b)

![Screenshot (2356)](https://github.com/user-attachments/assets/d42436e2-7fbf-4baa-9923-08ba27122611)

![Screenshot (2357)](https://github.com/user-attachments/assets/0368a44e-52d3-4628-b01a-4db74de13724)

![Screenshot (2358)](https://github.com/user-attachments/assets/cdda60b8-240a-45ee-8eb8-43080a1f6282)

![Screenshot (2359)](https://github.com/user-attachments/assets/b57230c1-8893-4aa7-b025-dbabae5a4617)

![Screenshot (2360)](https://github.com/user-attachments/assets/3c9342c3-3e73-4b45-9e73-82d75f0d45f4)

![Screenshot (2361)](https://github.com/user-attachments/assets/61d2baa4-dbee-46f1-9f16-24021310dd31)

![Screenshot (2362)](https://github.com/user-attachments/assets/7ca0181c-7b21-4753-9e43-667ba0a3dcaf)

![Screenshot (2363)](https://github.com/user-attachments/assets/fa55cd52-9230-47e3-848b-262d1f0d28d6)



