@echo off
REM Ecommerce Application Startup Script for Windows

echo ========================================
echo Ecommerce Application - Startup Script
echo ========================================
echo.

REM Check if the correct directory
if not exist "web.app.csproj" (
    echo ERROR: Please run this script from the root directory where web.app.csproj is located
    pause
    exit /b 1
)

echo.
echo This script will start all three services.
echo You will need three terminal windows open.
echo.
echo Press any key to start...
pause

REM Terminal 1: ASP.NET API
echo Starting ASP.NET Web API...
start cmd /k "title ASP.NET API && dotnet run"

REM Wait a few seconds
timeout /t 3 /nobreak

REM Terminal 2: BFF
echo Starting Node.js BFF...
start cmd /k "cd clientapp\bff && npm run dev && pause"

REM Wait a few seconds
timeout /t 2 /nobreak

REM Terminal 3: React Frontend
echo Starting React Frontend...
start cmd /k "cd clientapp\web && npm run dev && pause"

echo.
echo ========================================
echo All services started!
echo ========================================
echo.
echo API:    https://localhost:7123
echo BFF:    http://localhost:5000
echo React:  http://localhost:3000
echo.
echo Swagger: https://localhost:7123/swagger
echo.
echo Press any key to exit this window...
pause
