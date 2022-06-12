#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RBS.Email.Sender.WebApi/RBS.Email.Sender.WebApi.csproj", "RBS.Email.Sender.WebApi/"]
COPY ["RBS.Email.Sender.Models/RBS.Email.Sender.Common.csproj", "RBS.Email.Sender.Models/"]
COPY ["RBS.Email.Sender.Services/RBS.Email.Sender.Services.csproj", "RBS.Email.Sender.Services/"]
COPY ["RBS.Email.Sender.DataAccess/RBS.Email.Sender.DataAccess.csproj", "RBS.Email.Sender.DataAccess/"]
RUN dotnet restore "RBS.Email.Sender.WebApi/RBS.Email.Sender.WebApi.csproj"
COPY . .
WORKDIR "/src/RBS.Email.Sender.WebApi"
RUN dotnet build "RBS.Email.Sender.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RBS.Email.Sender.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "RBS.Auth.WebApi.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet RBS.Email.Sender.WebApi.dll