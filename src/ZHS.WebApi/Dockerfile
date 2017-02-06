FROM microsoft/dotnet:1.0.3-sdk-projectjson
RUN mkdir -p /app
ARG source=.
WORKDIR /source
COPY $source .
RUN dotnet restore
RUN dotnet publish
RUN cp -r  bin/Debug/netcoreapp1.0 /app
RUN rm -rf /source/*
WORKDIR /app
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
ENTRYPOINT dotnet ZHS.WebApi.dll