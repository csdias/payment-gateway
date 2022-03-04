FROM dtr.containerh.prudentialdobrasil.com.br/dev/core-aspnet as release
ENV SWAGGER_YAML_FILE "swagger.yaml"
WORKDIR /app
COPY ./out .
COPY swagger.yaml .
ENTRYPOINT ["dotnet", "FrameworksAndDrivers.dll"]