#Set-ExecutionPolicy unrestricted

$dir = $args[0]
$configuation = $args[1]
$outputPath = $args[2]

$src = $dir + "..\..\Web\dist\*.*"
$des = $outputPath +"\html\"

XCOPY $src $des /D/E/C/F/R/Y

$configFile =  $des + "app\common\config.js"

(Get-Content $configFile) | ForEach-Object { $_ -replace "ApiUrl: 'http://localhost/banka.api/api/'" , "ApiUrl: 'http://localhost:9000/api/'" } | Set-Content $configFile
