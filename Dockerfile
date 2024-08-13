FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/CompreApp.Api/CompreApp.Api.csproj", "CompreApp.Api/"]
COPY ["src/CompreApp.Application/CompreApp.Application.csproj", "CompreApp.Application/"]
COPY ["src/CompreApp.Infra/CompreApp.Infra.csproj", "CompreApp.Infra/"]
COPY ["src/CompreApp.Domain/CompreApp.Domain.csproj", "CompreApp.Domain/"]
RUN dotnet restore "CompreApp.Api/CompreApp.Api.csproj"
COPY . ../
WORKDIR /src/CompreApp.Api
RUN dotnet build "CompreApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompreApp.Api.dll"]