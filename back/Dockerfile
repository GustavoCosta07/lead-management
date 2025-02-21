FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY . ./

RUN dotnet restore

RUN rm -rf $HOME/.dotnet/toolResolverCache
RUN dotnet tool install --global dotnet-ef --version 6.0.0
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet build --no-restore

RUN dotnet test

CMD ["sh", "-c", "dotnet tool restore && dotnet ef database update -p src/Infrastructure/Infrastructure.csproj -s src/API/API.csproj && sleep 5 && dotnet run --project src/API/API.csproj"]
