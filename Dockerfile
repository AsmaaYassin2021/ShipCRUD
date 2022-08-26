FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /ShipCURDOperations
COPY *.sln .
COPY ShipCURDOperations.Common/*.csproj ./ShipCURDOperations.Common/
COPY ShipCURDOperations.Data/*.csproj ./ShipCURDOperations.Data/
COPY ShipCURDOperations.Business/*.csproj ./ShipCURDOperations.Business/
COPY ShipCURDOperations.API/*.csproj ./ShipCURDOperations.API/
COPY ShipCURDOperations.Tests/*.csproj ./ShipCURDOperations.Tests/
RUN dotnet restore
COPY . .
WORKDIR "/ShipCURDOperations/ShipCURDOperations.API"
RUN dotnet build "ShipCURDOperations.API.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "ShipCURDOperations.API.csproj" -c Release -o /app/publish

# test
RUN dotnet test /ShipCURDOperations/ShipCURDOperations.Tests/ShipCURDOperations.Tests.csproj

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShipCURDOperations.API.dll"]