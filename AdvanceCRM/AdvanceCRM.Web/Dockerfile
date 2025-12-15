FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["AdvanceCRM..Web.csproj", "./"]
#RUN apt-get update \
    #&& apt-get install -y --no-install-recommends apt-utils nodejs npm \
    #&& rm -rf /var/lib/apt/lists/*
    RUN apt-get update \
 && DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
      apt-utils nodejs npm \
 && rm -rf /var/lib/apt/lists/*
RUN dotnet restore "AdvanceCRM..Web.csproj"

COPY . .
RUN dotnet publish "AdvanceCRM..Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AdvanceCRM.Web.dll"]
