#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Document/DocumentAPI.csproj", "Document/"]
COPY ["DocumentApplication/DocumentApplication.csproj", "DocumentApplication/"]
COPY ["DocumentDomain/DocumentDomain.csproj", "DocumentDomain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["BankRecordAPIClient/BankRecordAPIClient.csproj", "BankRecordAPIClient/"]
COPY ["BankRecordDomain/BankRecordDomain.csproj", "BankRecordDomain/"]
COPY ["DocumentData/DocumentData.csproj", "DocumentData/"]
RUN dotnet restore "Document/DocumentAPI.csproj"
COPY . .
WORKDIR "/src/Document"
RUN dotnet build "DocumentAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocumentAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocumentAPI.dll"]