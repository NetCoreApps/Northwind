FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY publish/* ./
RUN test -e ./Northwind.sqlite && mkdir -p App_Data && mv ./Northwind.sqlite ./App_Data/Northwind.sqlite
ENTRYPOINT ["dotnet", "Northwind.dll"]
