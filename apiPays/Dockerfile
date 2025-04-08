FROM mcr.microsoft.com/dotnet/sdk:8.0 AS bob
#ce qu'il y a après le docker pull sur dockerhub

WORKDIR /app
COPY . .
#On copie les fichiers du dossier dans lequel se trouve le dockerfile dans le workdir (/app)

RUN rm ApiPays.sln
#Mon sln trainant dans le même package, on le dégage sinon ça crashe



RUN dotnet restore
#restore les packages nuget



#-c config de compilation en mode Release (sans symbole de débug et plus opti)
# on évite bin comme répertoire de sortie, possiblement la merde (truite c'est quand même mieux)
RUN dotnet publish -c Release -o truite

ENV PATH ~/.dotnet/tools:$PATH

FROM mcr.microsoft.com/dotnet/aspnet:8.0
#le runtime pour faire tourner le programme
WORKDIR /app

COPY --from=bob /app/truite .

EXPOSE 80

ENV ASPNETCORE_URLS=http://*:80

ENV ASPNETCORE_ENVIRONMENT=Development 
#lol swagger

ENTRYPOINT ["dotnet","ApiPays.dll"]
