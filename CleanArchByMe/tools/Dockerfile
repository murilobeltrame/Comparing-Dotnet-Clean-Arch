FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY  . .
WORKDIR /source/tools/CleanArchByMe.Infrastructure.Tools
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "CleanArchByMe.Infrastructure.Tools.dll"]