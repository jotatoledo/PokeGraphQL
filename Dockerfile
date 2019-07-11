FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/PokeGraphQL/*.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /dist

FROM build AS publish
RUN dotnet publish -c Release -o /dist

FROM base AS final
WORKDIR /app
COPY --from=publish /dist .
CMD dotnet PokeGraphQL.dll