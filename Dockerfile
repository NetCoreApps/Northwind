FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY src/*.sln .
COPY src/Northwind/*.csproj ./Northwind/
COPY src/Northwind.ServiceInterface/*.csproj ./Northwind.ServiceInterface/
COPY src/Northwind.ServiceModel/*.csproj ./Northwind.ServiceModel/
RUN dotnet restore

# copy everything else and build app
COPY src/Northwind/. ./Northwind/
COPY src/Northwind.ServiceInterface/. ./Northwind.ServiceInterface/
COPY src/Northwind.ServiceModel/. ./Northwind.ServiceModel/
WORKDIR /app/Northwind
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/Northwind/out ./
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "Northwind.dll"]
