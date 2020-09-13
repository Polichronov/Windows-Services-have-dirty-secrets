#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-nanoserver-1903 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["Demo3Worker/Demo3Worker.csproj", "Demo3Worker/"]
RUN dotnet restore "Demo3Worker/Demo3Worker.csproj"
COPY . .
WORKDIR "/src/Demo3Worker"
RUN dotnet build "Demo3Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo3Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo3Worker.dll"]