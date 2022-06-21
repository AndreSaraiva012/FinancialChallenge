#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BuyRequest/BuyRequestsAPI.csproj", "BuyRequest/"]
COPY ["DataBuyRequest/BuyRequestData.csproj", "DataBuyRequest/"]
COPY ["BuyRequestDomain/BuyRequestDomain.csproj", "BuyRequestDomain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["ApplicationBuyRequest/ApplicationBuyRequest.csproj", "ApplicationBuyRequest/"]
COPY ["BankRecordAPIClient/BankRecordAPIClient.csproj", "BankRecordAPIClient/"]
COPY ["BankRecordDomain/BankRecordDomain.csproj", "BankRecordDomain/"]
RUN dotnet restore "BuyRequest/BuyRequestsAPI.csproj"
COPY . .
WORKDIR "/src/BuyRequest"
RUN dotnet build "BuyRequestsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BuyRequestsAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuyRequestsAPI.dll"]