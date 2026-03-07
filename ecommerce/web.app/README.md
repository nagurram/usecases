# Ecommerce Application - Complete Solution

A modern, three-tier ecommerce application built with **ASP.NET Core 8**, **Node.js BFF**, and **React**.

## 🏗️ Architecture

```
┌─────────────────┐
│  React Frontend │  (http://localhost:3000)
│   (Vite + React)│
└────────┬────────┘
         │
    ┌────▼──────────┐
    │  Node.js BFF  │  (http://localhost:5000)
    │  (Express.js) │
    └────┬──────────┘
         │
    ┌────▼──────────────┐
    │ ASP.NET Core 8    │  (https://localhost:7123)
    │ Web API           │
    └───────────────────┘
```

## 🚀 Quick Start

### Prerequisites
- **.NET 8 SDK** (for ASP.NET Web API)
- **Node.js 18+** (for BFF and React)
- **npm** or **yarn**

### 1️⃣ Install Dependencies

**ASP.NET API** (automatic with dotnet):
```bash
dotnet restore
```

**BFF**:
```bash
cd clientapp/bff
npm install
```

**React**:
```bash
cd clientapp/web
npm install
```

### 2️⃣ Start All Services

**Option A: One Command (Windows)**
```bash
startup.bat
```

**Option B: One Command (macOS/Linux)**
```bash
chmod +x startup.sh
./startup.sh
```

**Option C: Three Separate Terminals**

Terminal 1:
```bash
dotnet run
```

Terminal 2:
```bash
cd clientapp/bff
npm run dev
```

Terminal 3:
```bash
cd clientapp/web
npm run dev
```

### 3️⃣ Access the Application

- **Frontend**: http://localhost:3000
- **API**: https://localhost:7123
- **API Docs**: https://localhost:7123/swagger
- **BFF**: http://localhost:5000

## 📁 Project Structure

```
usecases/
├── Models/                           # Data models
│   ├── Product.cs
│   ├── Order.cs
│   ├── Customer.cs
│   └── Cart.cs
│
├── Services/                         # Business logic
│   ├── IProductService.cs
│   ├── ProductService.cs
│   ├── IOrderService.cs
│   ├── OrderService.cs
│   ├── ICartService.cs
│   └── CartService.cs
│
├── Controllers/                      # REST API endpoints
│   ├── ProductsController.cs
│   ├── OrdersController.cs
│   ├── CartController.cs
│   └── WeatherForecastController.cs  # (sample)
│
├── Program.cs                        # ASP.NET configuration
├── web.app.csproj
│
├── clientapp/
│   │
│   ├── bff/                          # Backend for Frontend
│   │   ├── server.js
│   │   ├── package.json
│   │   ├── .env.example
│   │   └── README.md
│   │
│   └── web/                          # React Frontend
│       ├── src/
│       │   ├── App.jsx
│       │   ├── main.jsx
│       │   ├── index.css
│       │   ├── api/
│       │   │   └── client.js         # API client
│       │   └── context/
│       │       └── CartContext.jsx   # Cart state management
│       │
│       ├── index.html
│       ├── package.json
│       ├── vite.config.js
│       └── README.md
│
├── SETUP_GUIDE.md                    # Detailed setup instructions
├── API_REFERENCE.md                  # API endpoint reference
├── startup.bat                       # Windows startup script
├── startup.sh                        # Linux/macOS startup script
└── README.md                         # This file
```

## 🔌 API Endpoints

### Products
```
GET    /api/products
GET    /api/products/:id
GET    /api/products/category/:category
POST   /api/products
PUT    /api/products/:id
DELETE /api/products/:id
```

### Cart
```
GET    /api/cart/:customerId
POST   /api/cart/:customerId/items
PUT    /api/cart/:customerId/items/:productId
DELETE /api/cart/:customerId/items/:productId
DELETE /api/cart/:customerId
```

### Orders
```
GET    /api/orders/customer/:customerId
GET    /api/orders/:id
POST   /api/orders
PUT    /api/orders/:id/status
PUT    /api/orders/:id/cancel
```

See **API_REFERENCE.md** for detailed examples.

## 🛠️ Technology Stack

### Backend (ASP.NET Core 8)
- ✅ .NET 8 SDK
- ✅ ASP.NET Core Web API
- ✅ Swagger/OpenAPI
- ✅ Built-in Dependency Injection
- ✅ CORS support

### BFF (Node.js)
- ✅ Express.js
- ✅ Axios for HTTP requests
- ✅ CORS middleware
- ✅ Environment variables (dotenv)

### Frontend (React)
- ✅ React 18.2
- ✅ Vite build tool
- ✅ Axios HTTP client
- ✅ React Router v6 (configured)
- ✅ Context API for state management

## 📋 Features

✨ **Product Management**
- List all products
- View product details
- Filter by category
- Create/Update/Delete products

🛒 **Shopping Cart**
- Add items to cart
- Update quantities
- Remove items
- Clear cart

📦 **Order Management**
- Create orders from cart
- View order history
- Track order status
- Cancel orders (if not shipped)

## 🔐 Configuration

### ASP.NET API
Set CORS allowed origins in `Program.cs`:
```csharp
options.WithOrigins("http://localhost:3000", "http://localhost:5173")
```

### BFF (.env)
```env
PORT=5000
REACT_APP_API_BASE_URL=https://localhost:7123
NODE_ENV=development
```

### React (vite.config.js)
```javascript
server: {
  port: 3000,
  proxy: {
    '/api': {
      target: 'http://localhost:5000',
      changeOrigin: true,
    },
  },
}
```

## 🧪 Testing the API

### Using Swagger UI
1. Navigate to https://localhost:7123/swagger
2. Try out endpoints interactively

### Using cURL
```bash
# Get all products
curl https://localhost:7123/api/products

# Create a product
curl -X POST https://localhost:7123/api/products \
  -H "Content-Type: application/json" \
  -d '{"name":"Test","price":99.99,"category":"Electronics","stockQuantity":10}'
```

### Using VS Code REST Client
Create a `.http` file:
```http
@baseUrl = https://localhost:7123

GET {{baseUrl}}/api/products

POST {{baseUrl}}/api/products
Content-Type: application/json

{
  "name": "Test Product",
  "price": 99.99,
  "category": "Electronics",
  "stockQuantity": 10
}
```

## 🗄️ Data Persistence

Currently using **in-memory storage**. To add database support:

### Option 1: SQLite (Recommended for development)
```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

### Option 2: SQL Server
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

### Option 3: PostgreSQL
```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

See SETUP_GUIDE.md for detailed database integration steps.

## 🐛 Troubleshooting

| Issue | Solution |
|-------|----------|
| CORS errors | Ensure BFF is running and React is on localhost:3000 |
| API connection failed | Check ASP.NET is running on https://localhost:7123 |
| Port already in use | Change port in respective config files |
| npm install fails | Clear npm cache: `npm cache clean --force` |
| .NET restore fails | Check .NET 8 SDK is installed: `dotnet --version` |

## 📚 Documentation

- **SETUP_GUIDE.md** - Detailed setup and installation instructions
- **API_REFERENCE.md** - Complete API endpoint reference with examples
- **clientapp/bff/README.md** - BFF specific documentation
- **clientapp/web/README.md** - React frontend documentation

## 🚀 Deployment

### Development
```bash
# Already covered above
npm run dev
dotnet run
```

### Production

**Build ASP.NET**:
```bash
dotnet publish -c Release
```

**Build React**:
```bash
cd clientapp/web
npm run build
# Output: dist/ folder
```

**Build BFF**:
```bash
# Node.js BFF runs as-is, no build needed
```

## 📝 License

This project is provided as-is for educational purposes.

## 💡 Next Steps

1. ✅ Connect to a real database (SQL Server, PostgreSQL, SQLite)
2. ✅ Add user authentication (JWT, OAuth)
3. ✅ Add input validation and error handling
4. ✅ Implement logging
5. ✅ Add more React pages and components
6. ✅ Write unit tests
7. ✅ Set up CI/CD pipeline (GitHub Actions, Azure DevOps)
8. ✅ Deploy to Azure/AWS/Heroku

## 🤝 Support

For issues or questions, refer to the documentation files included or check the specific service logs:
- **API logs**: Console output when running `dotnet run`
- **BFF logs**: Console output when running `npm run dev`
- **React logs**: Browser console (F12)

---

**Happy coding! 🎉**
