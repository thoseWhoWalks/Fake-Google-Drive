#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see http://aka.ms/containercompat 

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY FakeGoogleDrive/FGD.Api.csproj FakeGoogleDrive/
COPY FGD.Encryption/FGD.Encryption.csproj FGD.Encryption/
COPY FGD.Configuration/FGD.Configuration.csproj FGD.Configuration/
COPY FGD.Data/FGD.Data.csproj FGD.Data/
COPY FGD.SharedTypes/FGD.SharedTypes.csproj FGD.SharedTypes/
COPY FGD.Api.Model/FGD.Api.Model.csproj FGD.Api.Model/
COPY FGD.Bussines.Model/FGD.Bussines.Model.csproj FGD.Bussines.Model/
COPY FGD.Data.Service/FGD.Data.Service.csproj FGD.Data.Service/
COPY FGD.Bussines.Service/FGD.Bussines.Service.csproj FGD.Bussines.Service/
RUN dotnet restore FakeGoogleDrive/FGD.Api.csproj
COPY . .
WORKDIR /src/FakeGoogleDrive
RUN dotnet build FGD.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish FGD.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FGD.Api.dll"]
