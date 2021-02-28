FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["front-end/front-end.csproj", "front-end/"]
RUN dotnet restore "front-end/front-end.csproj"
COPY . .
WORKDIR "/src/front-end"
RUN dotnet build "front-end.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "front-end.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "front-end.dll"]
