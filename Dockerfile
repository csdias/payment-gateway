# ---- Run Build ----
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . /app/
WORKDIR /app
RUN dotnet restore PaymentGateway.sln
RUN dotnet publish PaymentGateway.sln -c Release -o out

# ---- Run App ----
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as release
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "FrameworksAndDrivers.dll"]
