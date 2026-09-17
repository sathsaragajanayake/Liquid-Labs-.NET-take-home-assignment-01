FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY LiquidLabsAssignment/LiquidLabsAssignment/LiquidLabsAssignment.csproj LiquidLabsAssignment/LiquidLabsAssignment/
RUN dotnet restore LiquidLabsAssignment/LiquidLabsAssignment/LiquidLabsAssignment.csproj

COPY LiquidLabsAssignment/LiquidLabsAssignment/ LiquidLabsAssignment/LiquidLabsAssignment/

WORKDIR /src/LiquidLabsAssignment/LiquidLabsAssignment
RUN dotnet publish LiquidLabsAssignment.csproj -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "LiquidLabsAssignment.dll"]