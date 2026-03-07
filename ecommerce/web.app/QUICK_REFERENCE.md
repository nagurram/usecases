# Quick Reference Card

## 🎯 Start Here

```bash
# Install dependencies (one time)
cd clientapp/bff && npm install
cd ../web && npm install

# Run everything (Windows)
startup.bat

# Run everything (Mac/Linux)
./startup.sh
```

## 🌐 Access Points

| Service | URL | Status |
|---------|-----|--------|
| Frontend | http://localhost:3000 | React App |
| BFF | http://localhost:5000 | Node.js Proxy |
| API | https://localhost:7123 | ASP.NET Core |
| Swagger | https://localhost:7123/swagger | API Docs |

## 📦 Project Structure

```
usecases/
├── Models/              ← Data models
├── Services/            ← Business logic
├── Controllers/         ← API endpoints
├── Program.cs           ← Configuration
├── clientapp/
│   ├── bff/            ← Backend for Frontend
│   └── web/            ← React frontend
└── Documentation files
```

## 🔌 Main API Routes

### Products (`/api/products`)
- `GET` - All products
- `GET {id}` - One product
- `GET category/{cat}` - By category
- `POST` - Create
- `PUT {id}` - Update
- `DELETE {id}` - Delete

### Cart (`/api/cart/{customerId}`)
- `GET` - View cart
- `POST /items` - Add item
- `PUT /items/{id}` - Update qty
- `DELETE /items/{id}` - Remove item
- `DELETE` - Clear cart

### Orders (`/api/orders`)
- `GET /customer/{id}` - User's orders
- `GET {id}` - One order
- `POST` - Create order
- `PUT {id}/status` - Update status
- `PUT {id}/cancel` - Cancel

## 🛠️ Typical Workflow

### 1. Get Products
```bash
curl https://localhost:7123/api/products
```

### 2. Add to Cart
```bash
curl -X POST http://localhost:5000/api/cart/user123/items \
  -H "Content-Type: application/json" \
  -d '{"productId":1,"quantity":2,"price":99.99,"productName":"Item"}'
```

### 3. View Cart
```bash
curl http://localhost:5000/api/cart/user123
```

### 4. Create Order
```bash
curl -X POST http://localhost:5000/api/orders \
  -H "Content-Type: application/json" \
  -d '{"customerId":"user123","items":[...],"totalAmount":199.98,"shippingAddress":"123 Main St"}'
```

## 💻 Development Commands

```bash
# Terminal 1: API
cd E:\Narendra\github\usecases
dotnet run

# Terminal 2: BFF  
cd E:\Narendra\github\usecases\clientapp\bff
npm run dev

# Terminal 3: React
cd E:\Narendra\github\usecases\clientapp\web
npm run dev
```

## 📝 React Component Usage

```javascript
import { productAPI, cartAPI, orderAPI } from './api/client';

// Get products
const { data } = await productAPI.getAllProducts();

// Add to cart
await cartAPI.addToCart('user123', item);

// Get cart
const { data: cart } = await cartAPI.getCart('user123');

// Create order
await orderAPI.createOrder(order);
```

## 🎨 React Example Components

Located in: `clientapp/web/src/components/Examples.jsx`

- ProductList
- ProductsByCategory
- CreateProductForm
- ShoppingCart
- AddToCartButton
- OrderHistory
- CheckoutForm

## ⚙️ Configuration Files

| File | Purpose |
|------|---------|
| `Program.cs` | API setup & services |
| `clientapp/bff/.env` | BFF configuration |
| `clientapp/web/vite.config.js` | React build config |
| `.csproj` | Project references |

## 🐛 Troubleshooting

| Problem | Solution |
|---------|----------|
| Port in use | Kill process or change port |
| CORS error | Check BFF is running |
| API not found | Verify URL: https://localhost:7123 |
| npm error | Run `npm cache clean --force` |
| .NET error | Verify .NET 8: `dotnet --version` |

## 📚 Key Files to Know

- **SETUP_GUIDE.md** - Detailed setup instructions
- **API_REFERENCE.md** - Complete API docs
- **GETTING_STARTED.md** - Full overview
- **clientapp/bff/README.md** - BFF details
- **clientapp/web/README.md** - React details

## 🚀 Sample Data

Pre-loaded products:
1. Laptop - $1,299.99 (Electronics)
2. Wireless Mouse - $29.99 (Electronics)
3. USB-C Cable - $14.99 (Accessories)

## 📊 Technology Stack

```
Frontend:    React 18 + Vite
BFF:         Node.js + Express
Backend:     .NET 8 + ASP.NET Core
Storage:     In-memory (demo)
HTTP:        Axios
State:       Context API
```

## ✨ Features Available

✅ Product catalog with categories  
✅ Shopping cart functionality  
✅ Order management  
✅ Order status tracking  
✅ REST API  
✅ Swagger documentation  
✅ CORS configured  
✅ Error handling  

## 🎓 Learning Resources in Project

1. **Example Components** - React best practices
2. **API Client** - How to call backend
3. **Controllers** - RESTful design patterns
4. **Services** - Business logic layer
5. **Models** - Data structure design

## 🔗 Common Workflows

### List Products
```
Browser: http://localhost:3000/products
↓
React calls: productAPI.getAllProducts()
↓
BFF proxies: GET http://localhost:5000/api/products
↓
API responds: https://localhost:7123/api/products
```

### Add to Cart
```
Button click → addToCart('user123', item)
↓
cartAPI.addToCart()
↓
BFF: POST /api/cart/user123/items
↓
API stores in memory
```

### Checkout
```
Submit form → createOrder(cart)
↓
orderAPI.createOrder()
↓
BFF: POST /api/orders
↓
API creates order & returns order ID
```

---

**Ready to code? Start with `startup.bat` (Windows) or `./startup.sh` (Linux/Mac)!**
