FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as backendBuilder

WORKDIR /app/

COPY ./itec-backend/itec-backend.csproj ./itec-backend/
COPY ./web-1001-0110.sln ./


COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o out ./itec-backend/itec-backend.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

COPY --from=backendBuilder /app/out .
ENTRYPOINT ["dotnet", "itec-backend.dll"]
