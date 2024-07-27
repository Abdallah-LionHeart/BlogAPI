# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official ASP.NET Core SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["API.csproj", "./"]
RUN dotnet restore "API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define the entry point for the container
ENTRYPOINT ["dotnet", "API.dll"]
