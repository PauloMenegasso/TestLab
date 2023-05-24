FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet build -c Release --no-restore
RUN dotnet test -c Release --no-build /p:CollectCoverage=true /p:Threshold=100
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
CMD ["dotnet", "PersonCRUD.dll"]