build:
	dotnet build src/AppHost

cli:
	dotnet run --project src/Cli

run:
	aspire run

start:
	aspire start

stop:
	aspire stop

