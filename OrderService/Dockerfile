# Use the official .NET runtime image as a base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the .csproj file to the container
COPY ["OrderService.csproj", "./"]

# Restore any dependencies (via NuGet)
RUN dotnet restore "OrderService.csproj"

# Copy the rest of the application code to the container
COPY . .

# Set the working directory to /src and build the project
WORKDIR "/src"
RUN dotnet build "OrderService.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "OrderService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image: Copy the published app and set entry point
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.dll"]
