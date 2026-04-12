FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ClinicaBlazor/ClinicaBlazor.csproj", "ClinicaBlazor/"]
RUN dotnet restore "ClinicaBlazor/ClinicaBlazor.csproj"

COPY . .
WORKDIR "/src/ClinicaBlazor"
RUN dotnet publish "ClinicaBlazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "ClinicaBlazor.dll"]
