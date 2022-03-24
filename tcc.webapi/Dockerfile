#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["tcc.webapi/tcc.webapi.csproj", "tcc.webapi/"]
#RUN dotnet restore "tcc.webapi/tcc.webapi.csproj"
#COPY . .
#WORKDIR "/src/tcc.webapi"
#RUN dotnet build "tcc.webapi.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "tcc.webapi.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "tcc.webapi.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["tcc.webapi/tcc.webapi.csproj", "tcc.webapi/"]
RUN dotnet restore "tcc.webapi/tcc.webapi.csproj"
COPY . .
WORKDIR "/src/tcc.webapi"
RUN dotnet build "tcc.webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "tcc.webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet tcc.webapi.dll

#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY . .
#RUN dotnet restore
#RUN dotnet build --no-restore -c Release -o /app
#
#FROM base AS publish
#RUN dotnet publish "tcc.webapi.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet tcc.webapi.dll