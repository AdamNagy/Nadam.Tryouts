[Parameter(Mandatory=$true,ValueFromPipeline=$true)] [string] $sourceRoute,
[Parameter(Mandatory=$false,ValueFromPipeline=$true)] [string] $destRoute
    

if( !$destRoute )
{
    $destRoute = Get-Location;
}

Write-Host "Source dir: "$sourceRoute;
Write-Host "Dest dir: $destRoute";

$splittedRoute = $sourceRoute.Split("\");
$outputFileName = $splittedRoute[$splittedRoute.Length-1]+".content.txt";

Write-Host $outputFileName 

$output = $destRoute + "\" + $outputFileName;

Write-Host $output

get-childitem -path $sourceRoute -File | select-object name | Out-File $output -encoding "utf8"
