#!/bin/bash

# Ecommerce Application Startup Script for macOS/Linux

echo "========================================"
echo "Ecommerce Application - Startup Script"
echo "========================================"
echo ""

# Check if the correct directory
if [ ! -f "web.app.csproj" ]; then
    echo "ERROR: Please run this script from the root directory where web.app.csproj is located"
    exit 1
fi

echo ""
echo "This script will start all three services in separate processes."
echo ""

# Terminal 1: ASP.NET API
echo "Starting ASP.NET Web API..."
dotnet run &
API_PID=$!

# Wait a few seconds
sleep 3

# Terminal 2: BFF
echo "Starting Node.js BFF..."
cd clientapp/bff
npm run dev &
BFF_PID=$!
cd ../..

# Wait a few seconds
sleep 2

# Terminal 3: React Frontend
echo "Starting React Frontend..."
cd clientapp/web
npm run dev &
REACT_PID=$!
cd ../..

echo ""
echo "========================================"
echo "All services started!"
echo "========================================"
echo ""
echo "API:    https://localhost:7123"
echo "BFF:    http://localhost:5000"
echo "React:  http://localhost:3000"
echo ""
echo "Swagger: https://localhost:7123/swagger"
echo ""
echo "PIDs:"
echo "API:   $API_PID"
echo "BFF:   $BFF_PID"
echo "React: $REACT_PID"
echo ""
echo "To stop all services, press Ctrl+C"
echo ""

# Wait for all processes
wait $API_PID $BFF_PID $REACT_PID
