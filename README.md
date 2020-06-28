docker-compose up

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=123qweEWQ#@!" -p 1433:1433 -it --rm mcr.microsoft.com/mssql/server:2019-latest