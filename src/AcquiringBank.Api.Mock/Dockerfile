#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AcquiringBank.Api.Mock/AcquiringBank.Api.Mock.csproj", "AcquiringBank.Api.Mock/"]
RUN dotnet restore "AcquiringBank.Api.Mock/AcquiringBank.Api.Mock.csproj"
COPY . .
WORKDIR "/src/AcquiringBank.Api.Mock"
RUN dotnet build "AcquiringBank.Api.Mock.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AcquiringBank.Api.Mock.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AcquiringBank.Api.Mock.dll"]