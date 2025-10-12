### Docker Sql Server

```
docker run -d \
--name leavemanagement \
-e "ACCEPT_EULA=Y" \
-e "MSSQL_SA_PASSWORD=P@ssword2025" \
-e "MSSQL_PID=Developer" \
-e "MSSQL_AGENT_ENABLED=true" \
-p 1433:1433 \
-v mssql_data:/var/opt/mssql \
--health-cmd='bash -c "/opt/mssql-tools18/bin/sqlcmd -C -S localhost -U sa -P \"$MSSQL_SA_PASSWORD\" -Q \"SELECT 1\" || exit 1"' \
--health-interval=10s --health-timeout=3s --health-retries=10 \
mcr.microsoft.com/mssql/server:2022-latest
```

## Migration

Navigate to the Persistence project directory and run the following command to add the initial migration:

```
add-migration InitialMigration
```

```
dotnet ef database update
```
