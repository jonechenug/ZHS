FROM microsoft/dotnet:1.0-sdk
RUN mkdir app
RUN mkdir build
WORKDIR /build
ARG source=.
COPY $source .
RUN cd src/ZHS.WebApi && dotnet restore && dotnet publish -o /app
RUN cp /build/src/ZHS.WebApi/bin/Debug/netcoreapp1.0/ZHS.WebApi.xml /app
RUN rm -r -f /build
WORKDIR /app
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
ENTRYPOINT ["dotnet", "ZHS.WebApi.dll"]