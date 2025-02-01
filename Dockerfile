# Use official .NET Core SDK image to build the projects
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Set the working directory
WORKDIR /app

# Copy the solution file and restore dependencies for all projects
COPY OrderService.sln ./
COPY OrderService.API/OrderService.API.csproj ./OrderService.API/
COPY OrderService.Application/OrderService.Application.csproj ./OrderService.Application/
COPY OrderService.Domain/OrderService.Domain.csproj ./OrderService.Domain/
COPY OrderService.Infrastructure/OrderService.Infrastructure.csproj ./OrderService.Infrastructure/

# Restore all the dependencies (via nuget)
RUN dotnet nuget locals all --clear

RUN dotnet restore

# Copy all the source code for all projects
COPY . ./

# Build the solution
RUN dotnet build OrderService.sln -c Release --no-restore

# Publish the projects (this publishes the final executable that will run in the container)
RUN dotnet publish OrderService.sln -c Release -o /app/publish --no-build

# Use the official .NET Core Runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Set the entry point to the primary project's entry point
ENTRYPOINT ["dotnet", "OrderService.API.dll"]
