FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CryptographyInDotNet/CryptographyInDotNet.csproj", "CryptographyInDotNet/"]
RUN dotnet restore "CryptographyInDotNet/CryptographyInDotNet.csproj"
COPY . .
WORKDIR "/src/CryptographyInDotNet"
RUN dotnet build "CryptographyInDotNet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CryptographyInDotNet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptographyInDotNet.dll"]
