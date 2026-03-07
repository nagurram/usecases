# Ecommerce Application - Complete Setup Guide

## Overview

This is a complete ecommerce application with three tiers:

1. **ASP.NET Core 8 Web API** - Backend API
2. **Node.js BFF (Backend for Frontend)** - Middleware layer  
3. **React Frontend** - Client application

## Project Structure

```
.
├── web.app.csproj                 # Main ASP.NET Core project
├── Program.cs                      # API configuration
├── Models/                         # Data models
│   ├── Product.cs
│   ├── Order.cs
│   ├── Customer.cs
│   └── Cart.cs
├── Services/                       # Business logic
│   ├── IProductService.cs
│   ├── ProductService.cs
│   ├── IOrderService.cs
│   ├── OrderService.cs
│   ├── ICartService.cs
│   └── CartService.cs
├── Controllers/                    # API endpoints
│   ├── ProductsController.cs
│   ├── OrdersController.cs
│   └── CartController.cs
└── clientapp/
    ├── bff/                        # Node.js BFF Layer
    │   ├── server.js
    │   ├── package.json
    │   └── .env
    └── web/                        # React Frontend
        ├── src/
        │   ├── App.jsx
        │   ├── main.jsx
        │   ├── api/
        │   │   └── client.js
        │   └── context/
        │       └── CartContext.jsx
        ├── index.html
        ├── package.json
        └── vite.config.js
```

## Installation & Setup

### 1. ASP.NET Web API Setup

```bash
# Navigate to project root
cd E:\Narendra\github\usecases

# Restore packages
dotnet restore

# Build
dotnet build

# Run
dotnet run
```

The API will be available at: `https://localhost:7123` (or `http://localhost:5123`)

### 2. BFF (Backend for Frontend) Setup

```bash
cd clientapp/bff

# Install dependencies
npm install

# Create .env file from example
copy .env.example .env

# Run in development
npm run dev
```

The BFF will be available at: `http://localhost:5000`

### 3. React Frontend Setup

```bash
cd clientapp/web

# Install dependencies
npm install

# Run in development
npm run dev
```

The frontend will be available at: `http://localhost:3000`

## Running All Services

You'll need three terminal windows:

**Terminal 1 - ASP.NET API:**
```bash
cd E:\Narendra\github\usecases
dotnet run
```

**Terminal 2 - BFF:**
```bash
cd E:\Narendra\github\usecases\clientapp\bff
npm run dev
```

**Terminal 3 - React:**
```bash
cd E:\Narendra\github\usecases\clientapp\web
npm run dev
```

Then navigate to: `http://localhost:3000`

## API Endpoints

### Products
```
GET    /api/products              - Get all products
GET    /api/products/:id          - Get product by ID
GET    /api/products/category/:category - Get by category
POST   /api/products              - Create product
PUT    /api/products/:id          - Update product
DELETE /api/products/:id          - Delete product
```

### Cart
```
GET    /api/cart/:customerId           - Get cart
POST   /api/cart/:customerId/items     - Add item
PUT    /api/cart/:customerId/items/:id - Update quantity
DELETE /api/cart/:customerId/items/:id - Remove item
DELETE /api/cart/:customerId           - Clear cart
```

### Orders
```
GET    /api/orders/customer/:customerId - Get orders
GET    /api/orders/:id                  - Get order details
POST   /api/orders                      - Create order
PUT    /api/orders/:id/status           - Update status
PUT    /api/orders/:id/cancel           - Cancel order
```

### Health Check
```
GET /api/health - API health status
GET /health     - BFF health status
```

## Testing the API

### Using cURL

```bash
# Get all products
curl https://localhost:7123/api/products

# Create a product
curl -X POST https://localhost:7123/api/products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test Product",
    "description": "A test product",
    "price": 99.99,
    "stockQuantity": 10,
    "category": "Electronics"
  }'

# Get cart
curl http://localhost:5000/api/cart/customer123
```

### Using Swagger UI

Open `https://localhost:7123/swagger` in your browser to test the API interactively.

## Architecture Details

### ASP.NET Web API

**Models**: Define the data structures
- Product: name, price, stock, category
- Order: order items, total, status
- Cart: items, customer reference
- Customer: personal information

**Services**: Business logic layer
- ProductService: CRUD operations
- OrderService: Order processing
- CartService: Cart management

**Controllers**: REST endpoints
- ProductsController
- OrdersController  
- CartController

**Dependency Injection**: All services registered in `Program.cs`

### BFF Layer (Node.js)

The BFF acts as a proxy and aggregator:
- Routes requests from React to the ASP.NET API
- Handles CORS (enabled for localhost:3000)
- Can aggregate multiple API calls
- Can transform data for frontend needs

### React Frontend

**Context API**: CartContext for state management
**Axios Client**: API communication
**Vite**: Build tool and dev server
**React Router**: (Ready to use) Client-side routing

## Key Features

✅ Complete CRUD operations  
✅ Shopping cart functionality  
✅ Order management  
✅ Category filtering  
✅ In-memory data storage (replace with database as needed)  
✅ CORS configured for frontend communication  
✅ Swagger documentation  
✅ Error handling  
✅ Async/await patterns  

## Database Integration (Future)

To add a real database, replace the in-memory storage in services with:

1. Install Entity Framework Core:
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
# or
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

2. Create a DbContext:
```csharp
public class EcommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
}
```

3. Update services to use DbContext instead of in-memory lists

## Deployment

### ASP.NET API
```bash
dotnet publish -c Release
# Deploy the contents of bin/Release/net8.0/publish/
```

### BFF & React
```bash
# Build React
cd clientapp/web
npm run build

# Output in dist/ folder
```

## Environment Variables

**API**: Uses standard ASP.NET Core environment variables
**BFF**: Create `clientapp/bff/.env`:
```
PORT=5000
REACT_APP_API_BASE_URL=https://localhost:7123
NODE_ENV=development
```

**React**: Uses Vite environment (`.env` files)

## Troubleshooting

**CORS Issues**: Ensure BFF is running and React is on localhost:3000
**API Connection**: Check that ASP.NET API is running on the configured URL
**Port Conflicts**: Change port numbers in respective config files if ports are in use

## Next Steps

1. Connect to a real database (SQL Server, PostgreSQL, SQLite)
2. Add authentication/authorization (JWT tokens)
3. Add input validation
4. Implement error handling middleware
5. Add logging
6. Create more React components and pages
7. Add unit tests
8. Set up CI/CD pipeline
