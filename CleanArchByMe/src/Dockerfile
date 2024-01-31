FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY  . .
WORKDIR /source/CleanArchByMe.Api
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "CleanArchByMe.Api.dll"]