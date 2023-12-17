#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FreelancingPlatform/FreelancingPlatform.csproj", "FreelancingPlatform/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
RUN dotnet restore "FreelancingPlatform/FreelancingPlatform.csproj"
COPY . .
WORKDIR "/src/FreelancingPlatform"
RUN dotnet build "FreelancingPlatform.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreelancingPlatform.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreelancingPlatform.dll"]