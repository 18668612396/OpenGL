FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Silk_OpenGL/Silk_OpenGL.csproj", "Silk_OpenGL/"]
RUN dotnet restore "Silk_OpenGL/Silk_OpenGL.csproj"
COPY . .
WORKDIR "/src/Silk_OpenGL"
RUN dotnet build "Silk_OpenGL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Silk_OpenGL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Silk_OpenGL.dll"]
