FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/PokeGraphQL/*.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o /dist

FROM microsoft/dotnet:2.2-aspnetcore-runtime as start
WORKDIR /app
COPY --from=build /dist .
ENTRYPOINT ["dotnet" ," PokeGraphQL.dll"]