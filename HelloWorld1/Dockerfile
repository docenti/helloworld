FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HelloWorld1/HelloWorld1.csproj", "HelloWorld1/"]
RUN dotnet restore "HelloWorld1/HelloWorld1.csproj"
COPY . .
WORKDIR "/src/HelloWorld1"
RUN dotnet build "HelloWorld1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloWorld1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloWorld1.dll"]
