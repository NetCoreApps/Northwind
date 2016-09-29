FROM microsoft/dotnet:latest
COPY src /app
WORKDIR /app
RUN ["dotnet", "restore"]
WORKDIR /app/Northwind
RUN ["dotnet", "build"]
EXPOSE 5001/tcp
ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5001"]