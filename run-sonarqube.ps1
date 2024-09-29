# Configurações
$sonarToken = "sqp_ebe1f0ccc7e38ad813dff13bf325fdf7d3e386e3"
$sonarProjectKey = "voting-api"
$sonarHostUrl = "http://localhost:9000"
$coverageReportPath = "coverage.xml"

# Caminho do dotnet-sonarscanner (se necessário)
$dotnetSonarScannerPath = "C:\Users\seu_usuario\.dotnet\tools\dotnet-sonarscanner"

# Inicia a análise do SonarQube
& dotnet sonarscanner begin `
    /k:$sonarProjectKey `
    /d:sonar.host.url=$sonarHostUrl `
    /d:sonar.token=$sonarToken `
    /d:sonar.cs.vscoveragexml.reportsPaths=$coverageReportPath

# Compila o projeto
dotnet build voting-api --no-incremental

# Coleta cobertura de código
dotnet-coverage collect "dotnet test voting-api" -f xml -o "coverage.xml"

# Finaliza a análise do SonarQube
& dotnet sonarscanner end /d:sonar.token=$sonarToken

# Exibe mensagem de sucesso
Write-Host "Análise do SonarQube concluída com sucesso!"
