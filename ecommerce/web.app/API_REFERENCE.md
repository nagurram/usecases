# API Quick Reference

## Base URLs

- **API**: `https://localhost:7123/api`
- **BFF**: `http://localhost:5000/api`
- **React Frontend**: `http://localhost:3000`

## Products API

### Get All Products
```http
GET /api/products
```

**Response:**
```json
[
  {
    "id": 1,
    "name": "Laptop",
    "description": "High-performance laptop",
    "price": 1299.99,
    "stockQuantity": 10,
    "category": "Electronics",
    "imageUrl": null,
    "createdAt": "2024-01-01T00:00:00Z",
    "isActive": true
  }
]
```

### Get Product by ID
```http
GET /api/products/1
```

### Get Products by Category
```http
GET /api/products/category/Electronics
```

### Create Product
```http
POST /api/products
Content-Type: application/json

{
  "name": "New Product",
  "description": "Description",
  "price": 99.99,
  "stockQuantity": 50,
  "category": "Electronics"
}
```

### Update Product
```http
PUT /api/products/1
Content-Type: application/json

{
  "name": "Updated Product",
  "description": "Updated description",
  "price": 149.99,
  "stockQuantity": 30,
  "category": "Electronics",
  "isActive": true
}
```

### Delete Product
```http
DELETE /api/products/1
```

---

## Cart API

### Get Cart
```http
GET /api/cart/customer123
```

**Response:**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "customerId": "customer123",
  "items": [
    {
      "productId": 1,
      "productName": "Laptop",
      "price": 1299.99,
      "quantity": 1,
      "imageUrl": null
    }
  ],
  "lastModified": "2024-01-01T12:00:00Z"
}
```

### Add to Cart
```http
POST /api/cart/customer123/items
Content-Type: application/json

{
  "productId": 1,
  "productName": "Laptop",
  "price": 1299.99,
  "quantity": 1,
  "imageUrl": null
}
```

### Update Cart Item Quantity
```http
PUT /api/cart/customer123/items/1
Content-Type: application/json

{
  "quantity": 2
}
```

### Remove Item from Cart
```http
DELETE /api/cart/customer123/items/1
```

### Clear Cart
```http
DELETE /api/cart/customer123
```

---

## Orders API

### Get Customer Orders
```http
GET /api/orders/customer/customer123
```

**Response:**
```json
[
  {
    "id": 1,
    "orderNumber": "ORD-20240101120000",
    "customerId": "customer123",
    "items": [
      {
        "id": 1,
        "productId": 1,
        "productName": "Laptop",
        "quantity": 1,
        "unitPrice": 1299.99,
        "lineTotal": 1299.99
      }
    ],
    "totalAmount": 1299.99,
    "status": "Pending",
    "orderDate": "2024-01-01T12:00:00Z",
    "shippingAddress": "123 Main St, City, State 12345",
    "trackingNumber": null
  }
]
```

### Get Order by ID
```http
GET /api/orders/1
```

### Create Order
```http
POST /api/orders
Content-Type: application/json

{
  "customerId": "customer123",
  "items": [
    {
      "productId": 1,
      "productName": "Laptop",
      "quantity": 1,
      "unitPrice": 1299.99,
      "lineTotal": 1299.99
    }
  ],
  "totalAmount": 1299.99,
  "shippingAddress": "123 Main St, City, State 12345"
}
```

### Update Order Status
```http
PUT /api/orders/1/status
Content-Type: application/json

{
  "status": "Processing"
}
```

**Status Values:**
- `Pending` (0)
- `Processing` (1)
- `Shipped` (2)
- `Delivered` (3)
- `Cancelled` (4)

### Cancel Order
```http
PUT /api/orders/1/cancel
```

---

## Error Responses

### 400 Bad Request
```json
{
  "message": "Product name is required"
}
```

### 404 Not Found
```json
{
  "message": "Product with ID 999 not found"
}
```

### 500 Internal Server Error
```json
{
  "error": "Failed to fetch products"
}
```

---

## Health Checks

### API Health
```http
GET /api/health
```

**Response:**
```json
{
  "status": "healthy",
  "timestamp": "2024-01-01T12:00:00Z"
}
```

### BFF Health
```http
GET /health
```

**Response:**
```json
{
  "status": "BFF healthy",
  "timestamp": "2024-01-01T12:00:00Z"
}
```

---

## JavaScript/Axios Examples

### Using the Frontend API Client

```javascript
import { productAPI, cartAPI, orderAPI } from './api/client';

// Get all products
const products = await productAPI.getAllProducts();

// Add to cart
await cartAPI.addToCart('customer123', {
  productId: 1,
  productName: 'Laptop',
  price: 1299.99,
  quantity: 1
});

// Get orders
const orders = await orderAPI.getCustomerOrders('customer123');

// Create order
const newOrder = await orderAPI.createOrder({
  customerId: 'customer123',
  items: [...cartItems],
  totalAmount: 1299.99,
  shippingAddress: '123 Main St'
});
```
