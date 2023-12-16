# Use the official .NET 6.0 SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

# Copy .csproj and restore dependencies
COPY FreelancingPlatform/*.csproj ./
RUN dotnet restore

# Copy all the other files and build the application
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official .NET 6.0 Runtime image to run up the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

COPY --from=build /app/out ./

# Standard command to run the application
ENTRYPOINT ["dotnet", "FreelancingPlatform.dll"]