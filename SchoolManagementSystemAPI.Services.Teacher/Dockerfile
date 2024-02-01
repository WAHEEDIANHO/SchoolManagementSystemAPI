FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
#FROM waheedianho/message AS referer
WORKDIR /app
#COPY  --from=referer /app/out .
COPY *.csproj ./ 
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out



FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SchoolManagementSystemAPI.Services.Teacher.dll"]