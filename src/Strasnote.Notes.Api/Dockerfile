#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Notes/Strasnote.Notes.Api/Strasnote.Notes.Api.csproj"
WORKDIR "/src/Notes/Strasnote.Notes.Api"
RUN dotnet build "Strasnote.Notes.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Strasnote.Notes.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Strasnote.Notes.Api.dll"]
