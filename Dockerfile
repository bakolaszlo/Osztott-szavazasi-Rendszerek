# Build BattleshipsBE
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnetbuild
WORKDIR /src

COPY BattleshipsBE/BattleshipsBE/BattleshipsBE.csproj .
RUN dotnet restore

COPY BattleshipsBE/BattleshipsBE .
RUN dotnet build -c Release

# RUN dotnet test ...

RUN dotnet publish -c Release -o /dist


# build Vue app:
FROM node:alpine as buildvue

WORKDIR /src

COPY BattleshipsFE/package.json .
RUN npm install

# webpack build
COPY BattleshipsFE .
RUN npm run build


# Copy results from both places into production container:
FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000

# copy .net content
COPY --from=dotnetbuild /dist .
# copy vue content into .net's static files folder:
COPY --from=buildvue /src/dist /app/wwwroot

CMD ASPNETCORE_URLS=http://*:$PORT dotnet BattleshipsBE.dll