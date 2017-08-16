FROM microsoft/aspnetcore:2.0
ARG source=.
WORKDIR /app
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
COPY $source .
ENTRYPOINT ["dotnet", "ZHS.WebApi.dll"]
