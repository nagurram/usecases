# 🛠️ Troubleshooting Guide

## 🚀 Getting Started Issues

### Issue: "dotnet: command not found"
**Problem**: .NET SDK is not installed or not in PATH
**Solution**:
1. Download .NET 8 SDK from https://dotnet.microsoft.com/en-us/download
2. Install it
3. Restart your terminal
4. Verify: `dotnet --version`

### Issue: "npm: command not found"
**Problem**: Node.js/npm is not installed
**Solution**:
1. Download Node.js from https://nodejs.org/
2. Install LTS version (includes npm)
3. Restart your terminal
4. Verify: `npm --version`

### Issue: "Port 7123 is already in use"
**Problem**: ASP.NET API port is occupied
**Solution** (choose one):
1. Kill the process using the port:
   - Windows: `netstat -ano | findstr :7123`
   - macOS/Linux: `lsof -i :7123 | grep LISTEN`
2. Change the port in `Program.cs`
3. Change the port in environment variables

---

## 📦 Installation Issues

### Issue: "npm install fails with permission denied"
**Problem**: NPM cache or permission issues
**Solution**:
```bash
# Clear npm cache
npm cache clean --force

# Reinstall
npm install

# Or use sudo (not recommended)
sudo npm install
```

### Issue: "dotnet restore fails"
**Problem**: NuGet packages can't download
**Solution**:
```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore again
dotnet restore

# Or try with different source
dotnet restore --source https://api.nuget.org/v3/index.json
```

### Issue: Package "Swashbuckle.AspNetCore" not found
**Problem**: NuGet source issue
**Solution**:
```bash
# Try updating packages
dotnet add package Swashbuckle.AspNetCore --version 6.6.2

# Or check your NuGet.config
```

---

## 🏃 Running Services Issues

### Issue: "ASP.NET API fails to start"
**Solution**:
```bash
# Check for errors
dotnet run

# If port conflict:
# Change in launchSettings.json or use different port

# Check if already running:
netstat -ano | findstr :7123
```

### Issue: "BFF server fails to start"
**Solution**:
```bash
# Check for errors
cd clientapp/bff
npm run dev

# If npm command not found:
npm install -g npm

# If port 5000 is in use:
# Edit package.json and change port in server.js
```

### Issue: "React dev server won't start"
**Solution**:
```bash
# Check Vite installation
cd clientapp/web
npm install

# Check Node version (should be 18+)
node --version

# Try deleting cache
rm -rf node_modules package-lock.json
npm install
```

---

## 🌐 Connection Issues

### Issue: "Cannot connect to API from React"
**Problem**: CORS, firewall, or port mismatch
**Solution**:
1. Verify ASP.NET API is running: Check https://localhost:7123/swagger
2. Verify BFF is running: Check http://localhost:5000/health
3. Check CORS configuration in Program.cs:
```csharp
options.WithOrigins("http://localhost:3000", "http://localhost:5173")
```
4. Check BFF proxy URL in .env:
```
REACT_APP_API_BASE_URL=https://localhost:7123
```

### Issue: "CORS error in browser console"
**Error**: "Access to XMLHttpRequest has been blocked by CORS policy"
**Solution**:
1. Make sure BFF is running on port 5000
2. Ensure React is accessing BFF (not direct API)
3. Check BFF CORS middleware in server.js
4. Restart all services

### Issue: "Cannot reach localhost from another computer"
**Problem**: Firewall or binding issue
**Solution**:
1. In Program.cs, allow public access:
```csharp
app.UseHttpsRedirection();
// Skip for development if needed
```
2. Bind to all interfaces:
```bash
# Use environment variable
ASPNETCORE_URLS=https://*:7123
```

---

## 🔧 API Endpoint Issues

### Issue: "GET /api/products returns 404"
**Problem**: Controller or route not found
**Solution**:
1. Verify file exists: `Controllers/ProductsController.cs`
2. Check route attribute: `[Route("api/[controller]")]`
3. Verify service is registered in Program.cs
4. Check spelling: should be `/api/products` (lowercase)

### Issue: "POST /api/products returns 400 Bad Request"
**Problem**: Invalid JSON or missing fields
**Solution**:
1. Check Content-Type header: `application/json`
2. Verify JSON format
3. Check required fields in model
4. Example valid JSON:
```json
{
  "name": "Product Name",
  "description": "Description",
  "price": 99.99,
  "stockQuantity": 10,
  "category": "Electronics"
}
```

### Issue: "API returns 500 Internal Server Error"
**Problem**: Exception in service
**Solution**:
1. Check ASP.NET console output for stack trace
2. Verify the service is implemented
3. Check for null reference exceptions
4. Verify in-memory data structure

### Issue: "Health check endpoint not working"
**Solution**:
```bash
# Test it
curl https://localhost:7123/api/health

# If fails, check Program.cs
# Line should have:
app.MapGet("/api/health", () => new { status = "healthy", timestamp = DateTime.UtcNow })
```

---

## 🎨 React Issues

### Issue: "React page shows blank or errors"
**Solution**:
1. Open browser DevTools (F12)
2. Check Console tab for errors
3. Common issues:
   - API not running
   - Port mismatch
   - Syntax error in JSX
4. Check React DevTools extension

### Issue: "Cannot find module 'axios'"
**Solution**:
```bash
cd clientapp/web
npm install axios
```

### Issue: "useCart hook not working"
**Solution**:
1. Verify CartProvider wraps your app:
```jsx
<CartProvider>
  <App />
</CartProvider>
```
2. Check CartContext.jsx is in context folder
3. Verify import statement in component

### Issue: "API calls always fail"
**Solution**:
1. Check BFF is running: `http://localhost:5000/health`
2. Check ASP.NET API is running: `https://localhost:7123/api/health`
3. Check vite.config.js proxy configuration
4. Verify API URL in client.js:
```javascript
const BFF_BASE_URL = 'http://localhost:5000';
```

---

## 🗂️ File Issues

### Issue: "File not found" when running
**Problem**: Wrong working directory
**Solution**:
```bash
# Always run from root directory
cd E:\Narendra\github\usecases

# For BFF
cd E:\Narendra\github\usecases\clientapp\bff

# For React
cd E:\Narendra\github\usecases\clientapp\web
```

### Issue: "node_modules folder is huge"
**Solution**:
```bash
# This is normal (200+ MB)
# To clean up:
npm install --production
# Or delete and reinstall
rm -rf node_modules
npm install
```

### Issue: ".env file not found in BFF"
**Solution**:
```bash
cd clientapp/bff

# Create from example
copy .env.example .env

# Or manually create:
# PORT=5000
# REACT_APP_API_BASE_URL=https://localhost:7123
```

---

## 🔒 SSL/HTTPS Issues

### Issue: "Unable to verify the first certificate"
**Problem**: SSL certificate issue on localhost
**Solution**:
1. This is normal for development
2. Accept the warning in browser
3. Or disable SSL certificate verification (NOT for production):

For Node.js:
```bash
set NODE_TLS_REJECT_UNAUTHORIZED=0
npm run dev
```

For .NET:
```bash
set ASPNETCORE_URLS=http://+:5123
dotnet run
```

### Issue: "localhost certificate error"
**Solution**:
1. Generate development certificate:
```bash
dotnet dev-certs https
```
2. Trust the certificate (Windows):
```bash
dotnet dev-certs https --trust
```

---

## 🧪 Testing Issues

### Issue: "Cannot test API with Swagger"
**Problem**: Swagger UI not loading
**Solution**:
1. Open: https://localhost:7123/swagger
2. If 404, check Program.cs:
```csharp
builder.Services.AddSwaggerGen();
app.UseSwagger();
app.UseSwaggerUI();
```
3. Verify in development environment

### Issue: "cURL command not working"
**Solution**:
```bash
# Windows needs to escape quotes
curl -X POST https://localhost:7123/api/products ^
  -H "Content-Type: application/json" ^
  -d "{\"name\":\"Product\",\"price\":99.99}"

# Or use PowerShell
$body = @{name="Product"; price=99.99} | ConvertTo-Json
Invoke-WebRequest -Uri https://localhost:7123/api/products `
  -Method POST -Body $body -ContentType application/json
```

---

## 📊 Performance Issues

### Issue: "Application is slow"
**Solution**:
1. Check system resources (CPU, RAM, Disk)
2. Close other applications
3. For React development:
   - Disable browser extensions
   - Clear browser cache
4. For .NET:
   - Use Release build: `dotnet run -c Release`
5. For Node.js:
   - Check system resources

### Issue: "npm install takes forever"
**Solution**:
```bash
# Use npm ci instead
npm ci

# Or try different registry
npm config set registry https://registry.npmjs.org/

# Increase timeout
npm install --fetch-timeout=120000
```

---

## 🔍 Debugging Tips

### Enable detailed logging
```bash
# .NET
set ASPNETCORE_ENVIRONMENT=Development

# Node.js
set DEBUG=*
npm run dev

# React (in browser console)
console.log()
```

### Check running processes
```bash
# Windows
netstat -ano | findstr LISTENING

# macOS/Linux
lsof -i -P -n | grep LISTEN
```

### Kill a process
```bash
# Windows
taskkill /PID <PID> /F

# macOS/Linux
kill -9 <PID>
```

---

## 📚 Getting Help

1. **Check Documentation**:
   - QUICK_REFERENCE.md
   - SETUP_GUIDE.md
   - API_REFERENCE.md

2. **Check Logs**:
   - ASP.NET console output
   - BFF console output
   - Browser DevTools
   - Network tab

3. **Try Common Fixes**:
   - Restart all services
   - Clear caches (npm, dotnet)
   - Delete node_modules and reinstall
   - Check ports are not in use

4. **Verify Prerequisites**:
   - .NET 8 installed: `dotnet --version`
   - Node.js 18+ installed: `node --version`
   - npm installed: `npm --version`

---

## 🎯 Checklist Before Getting Help

- [ ] Restarted all services?
- [ ] Checked all ports are running (7123, 5000, 3000)?
- [ ] Verified firewall/antivirus not blocking?
- [ ] Checked console output for errors?
- [ ] Verified correct working directory?
- [ ] Checked for typos in commands?
- [ ] Verified prerequisites are installed?
- [ ] Cleared npm cache: `npm cache clean --force`?
- [ ] Cleared dotnet cache: `dotnet nuget locals all --clear`?
- [ ] Tried restarting computer?

---

**Still stuck? Check the specific error message in the troubleshooting guide above or review the detailed documentation files.**

**Remember: Most issues are related to ports, environment setup, or missing prerequisites.**
