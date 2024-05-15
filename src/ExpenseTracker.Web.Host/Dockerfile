FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/ExpenseTracker.Web.Host/ExpenseTracker.Web.Host.csproj", "src/ExpenseTracker.Web.Host/"]
COPY ["src/ExpenseTracker.Web.Core/ExpenseTracker.Web.Core.csproj", "src/ExpenseTracker.Web.Core/"]
COPY ["src/ExpenseTracker.Application/ExpenseTracker.Application.csproj", "src/ExpenseTracker.Application/"]
COPY ["src/ExpenseTracker.Core/ExpenseTracker.Core.csproj", "src/ExpenseTracker.Core/"]
COPY ["src/ExpenseTracker.EntityFrameworkCore/ExpenseTracker.EntityFrameworkCore.csproj", "src/ExpenseTracker.EntityFrameworkCore/"]
WORKDIR "/src/src/ExpenseTracker.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/ExpenseTracker.Web.Host", "src/ExpenseTracker.Web.Host"]
COPY ["src/ExpenseTracker.Web.Core", "src/ExpenseTracker.Web.Core"]
COPY ["src/ExpenseTracker.Application", "src/ExpenseTracker.Application"]
COPY ["src/ExpenseTracker.Core", "src/ExpenseTracker.Core"]
COPY ["src/ExpenseTracker.EntityFrameworkCore", "src/ExpenseTracker.EntityFrameworkCore"]
WORKDIR "/src/src/ExpenseTracker.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "ExpenseTracker.Web.Host.dll"]
