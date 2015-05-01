#Set-ExecutionPolicy unrestricted

$dir = $args[0]
$configuation = $args[1]

$src = $dir + "..\..\Web\dist\*.*"
$des = $dir + "bin\x86\" + $configuation +"\html\"

XCOPY $src $des /D/E/C/F/R/Y

$configFile =  $des + "app\common\config.js"

(Get-Content $configFile) | ForEach-Object { $_ -replace "ApiUrl: 'https://apibanka.apphb.com/api/'" , "ApiUrl: 'http://localhost:9000/api/'" } | Set-Content $configFile
