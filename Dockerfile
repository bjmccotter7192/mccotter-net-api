FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:5000;http://*:44319
ENV DB_HOST=POSTGRES_HOST
ENV DB_USER=POSTGRES_USER
ENV DB_PASSWORD=POSTGRES_PASSWORD
ENV DB_PORT=POSTGRES_PORT
ENV DB_DATABASE=POSTGRES_DATABASE

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["mccotter-net-api.csproj", "./"]
RUN dotnet restore "mccotter-net-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "mccotter-net-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mccotter-net-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mccotter-net-api.dll"]
