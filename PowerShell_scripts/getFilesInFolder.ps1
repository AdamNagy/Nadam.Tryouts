param([String]$route)

$splittedRoute = $route.Split("\");
$outputFileName = $splittedRoute[$splittedRoute.Length-1] + ".content.txt";

$output = "C:\Documents\PowerShell_scripts\" + $outputFileName;

get-childitem -path $route | select-object name | Out-File $output -encoding "utf8"
	