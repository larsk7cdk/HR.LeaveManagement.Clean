# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["src/HR.LeaveManagement.Api/HR.LeaveManagement.Api.csproj", "src/HR.LeaveManagement.Api/"]
COPY ["src/HR.LeaveManagement.Application/HR.LeaveManagement.Application.csproj", "src/HR.LeaveManagement.Application/"]
COPY ["src/HR.LeaveManagement.Domain/HR.LeaveManagement.Domain.csproj", "src/HR.LeaveManagement.Domain/"]
COPY ["src/HR.LeaveManagement.Infrastructure/HR.LeaveManagement.Infrastructure.csproj", "src/HR.LeaveManagement.Infrastructure/"]
COPY ["src/HR.LeaveManagement.Persistence/HR.LeaveManagement.Persistence.csproj", "src/HR.LeaveManagement.Persistence/"]
RUN dotnet restore "./src/HR.LeaveManagement.Api/HR.LeaveManagement.Api.csproj"
COPY . .
WORKDIR "/src/src/HR.LeaveManagement.Api"
RUN dotnet build "./HR.LeaveManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./HR.LeaveManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HR.LeaveManagement.Api.dll"]