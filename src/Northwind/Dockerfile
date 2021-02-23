FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY publish/* ./
ENTRYPOINT ["dotnet", "Northwind.dll"]
