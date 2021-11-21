#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DPSP Api v2/DPSP Api v2.csproj", "DPSP Api v2/"]
RUN dotnet restore "DPSP Api v2/DPSP Api v2.csproj"
COPY . .
WORKDIR "/src/DPSP Api v2"
RUN dotnet build "DPSP Api v2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DPSP Api v2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DPSP Api v2.dll"]