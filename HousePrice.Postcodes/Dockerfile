FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY HousePrice.Postcodes/HousePrice.Postcodes.csproj HousePrice.Postcodes/
RUN dotnet restore HousePrice.Postcodes/HousePrice.Postcodes.csproj
COPY . .
WORKDIR /src/HousePrice.Postcodes
RUN dotnet build HousePrice.Postcodes.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish HousePrice.Postcodes.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HousePrice.Postcodes.dll"]
