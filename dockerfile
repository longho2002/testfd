# Define the build stage
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src

# Copy csproj and restore dependencies
COPY ./video-editing-api/*.csproj ./video-editing-api/
RUN dotnet restore "video-editing-api/video-editing-api.csproj"

# Copy all files and build
COPY . ./video-editing-api/
WORKDIR "/src/video-editing-api"
RUN dotnet publish -c Release -o out

# Define the runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /src/video-editing-api/out .
ENTRYPOINT ["dotnet", "video-editing-api.dll"]
