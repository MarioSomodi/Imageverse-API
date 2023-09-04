#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Imageverse.Api/Imageverse.Api.csproj", "src/Imageverse.Api/"]
COPY ["src/Imageverse.Application/Imageverse.Application.csproj", "src/Imageverse.Application/"]
COPY ["src/Imageverse.Domain/Imageverse.Domain.csproj", "src/Imageverse.Domain/"]
COPY ["src/Imageverse.Contracts/Imageverse.Contracts.csproj", "src/Imageverse.Contracts/"]
COPY ["src/Imageverse.Infrastructure/Imageverse.Infrastructure.csproj", "src/Imageverse.Infrastructure/"]
RUN dotnet restore "src/Imageverse.Api/Imageverse.Api.csproj"
COPY . .
WORKDIR "/src/src/Imageverse.Api"
RUN dotnet build "Imageverse.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Imageverse.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Imageverse.Api.dll"]