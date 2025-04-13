FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY SportCenterApi.csproj .
RUN dotnet restore /src/SportCenterApi.csproj
COPY . .
RUN dotnet publish /src/SportCenterApi.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migrator
WORKDIR /tools
RUN dotnet tool install --tool-path /tools/.dotnet-tools dotnet-ef --version 8.*
ENV PATH="/tools/.dotnet-tools:${PATH}"
WORKDIR /src
COPY --from=build /src /src



FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_HTTP_PORT=80
EXPOSE 80
ENTRYPOINT ["dotnet", "SportCenterApi.dll"]