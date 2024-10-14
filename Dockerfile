# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files to the container
COPY ["TicketMaster/TicketMaster.csproj", "TicketMaster/"]
RUN dotnet restore "TicketMaster/TicketMaster.csproj"

# Copy everything else and build the application
COPY . .
WORKDIR "/src/TicketMaster"
RUN dotnet build "TicketMaster.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "TicketMaster.csproj" -c Release -o /app/publish

# Final image stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketMaster.dll"]
