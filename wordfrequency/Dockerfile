FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY --link *.csproj .
RUN dotnet restore

# copy everything else and build app
COPY --link . .
RUN dotnet publish -c release -o /app --no-restore --runtime linux-x64 --self-contained false

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["./wordfrequency"]
