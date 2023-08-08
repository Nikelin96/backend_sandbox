FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env

WORKDIR /GrpcBackendService

COPY GrpcBackendService/*.csproj .

RUN dotnet restore

COPY /GrpcBackendService .

RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/runtime:7.0 as runtime

WORKDIR /publish

COPY --from=build-env /publish .

ENTRYPOINT ["dotnet", "GrpcBackendService.dll"]