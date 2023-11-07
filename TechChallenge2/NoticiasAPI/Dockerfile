# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
RUN apk add --no-cache icu-libs icu-data-full
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /TechChallenge2
COPY ["NoticiasAPI.csproj", "./"]
RUN dotnet restore "NoticiasAPI.csproj"
COPY . .
WORKDIR "/TechChallenge2/."

RUN dotnet build "NoticiasAPI.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "NoticiasAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NoticiasAPI.dll"]