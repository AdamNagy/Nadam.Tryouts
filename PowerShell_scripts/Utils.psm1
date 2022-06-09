function Get-TableOfContent {
    param([Parameter(Mandatory=$true,ValueFromPipeline=$true)] [string] $sourceRoute)

    Write-Host "Source dir: "$sourceRoute;
    Write-Host "Dest dir: $destRoute";

    $splittedRoute = $sourceRoute.Split("\");
    $outputFileName = $splittedRoute[$splittedRoute.Length-1]+".content.txt";

    Write-Host $outputFileName 

    $output = $destRoute + "\" + $outputFileName;

    Write-Host $output

    return get-childitem -path $sourceRoute -File | % { $_.Name }
}

function Get-FolderName {
    param([Parameter(Mandatory=$true)] [string] $path);

    $splittedPath = $path.Split("\");
    return $splittedSourceDir[$splittedSourceDir.Length-1];
}