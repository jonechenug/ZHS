FROM microsoft/dotnet:1.0-sdk
RUN mkdir -p /app
ARG source=.
WORKDIR /src/ZHS.WebApi
COPY $source .
RUN dotnet restore
RUN dotnet publish
RUN cp -r  bin/Debug/netcoreapp1.0/publish /app
WORKDIR /app/publish
RUN rm -rf /ZHS.WebApi/*
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
ENTRYPOINT dotnet ZHS.WebApi.dll