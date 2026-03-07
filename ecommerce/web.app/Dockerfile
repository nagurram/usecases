# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# ============================================================================
# Stage 1: Build React Client App with Node.js
# ============================================================================
FROM node:18-alpine AS clientapp-build
WORKDIR /src/clientapp

# Copy package files
COPY clientapp/package*.json ./

# Install dependencies
RUN npm ci

# Copy source code
COPY clientapp/ .

# Build React app (outputs to dist directory)
RUN npm run build

# ============================================================================
# Stage 2: Base ASP.NET Core runtime
# ============================================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ============================================================================
# Stage 3: Build .NET Service Project
# ============================================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnetbuild
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project file
COPY ["web.app.csproj", "."]

# Restore dependencies
RUN dotnet restore "./web.app.csproj"

# Copy source code
COPY . .

# Copy built React app from clientapp-build stage to wwwroot
COPY --from=clientapp-build /src/clientapp/dist ./wwwroot

# Build .NET project
WORKDIR "/src/."
RUN dotnet build "./web.app.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ============================================================================
# Stage 4: Publish .NET Service
# ============================================================================
FROM dotnetbuild AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./web.app.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ============================================================================
# Stage 5: Final Production Image
# ============================================================================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "web.app.dll"]