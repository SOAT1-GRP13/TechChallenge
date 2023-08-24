# Construindo a aplicação
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiando os arquivos de projeto e restaurando as dependências
COPY ./*.sln ./
COPY ./API/*.csproj ./API/
COPY ./Application/*.csproj ./Application/
COPY ./Domain/*.csproj ./Domain/
COPY ./Domain.Tests/*.csproj ./Domain.Tests/
COPY ./Application.Tests/*.csproj ./Application.Tests/
COPY ./Infra/*.csproj ./Infra/
RUN dotnet restore

# Copiando o código-fonte e compilando a aplicação
#COPY ./API ./API/
#COPY ./Application ./Application/
#COPY ./Domain ./Domain/
#COPY ./Domain.Tests ./Domain.Tests/
#COPY ./Infra ./Infra/
COPY . ./
RUN dotnet publish -c Release -o out

# Executando a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Expondo a porta da aplicação
EXPOSE 80

# Iniciar a aplicação quando o contêiner for iniciado
ENTRYPOINT ["dotnet", "API.dll"]