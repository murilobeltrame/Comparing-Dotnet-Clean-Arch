dotnet test --collect:"XPlat Code Coverage"
dotnet reportgenerator "-reports:tests\*\TestResults\*\coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
start "" "coveragereport\index.html"