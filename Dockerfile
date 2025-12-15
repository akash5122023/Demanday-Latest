# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["AdvanceCRM/AdvanceCRM.Web/AdvanceCRM.Web.csproj", "AdvanceCRM/AdvanceCRM.Web/"]
RUN dotnet restore "AdvanceCRM/AdvanceCRM.Web/AdvanceCRM.Web.csproj"

COPY . .
WORKDIR /src/AdvanceCRM/AdvanceCRM.Web
RUN dotnet publish "AdvanceCRM.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "AdvanceCRM.Web.dll"]
