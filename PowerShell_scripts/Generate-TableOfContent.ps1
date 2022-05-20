param([Parameter(Mandatory=$true,ValueFromPipeline=$true)] [string] $sourceDir, [string] $destDir)

# Import-Module C:\Documents\PowerShell_scripts\Utils.psm1 -Verbose;

if( !$destDir ) {
    $destDir = Get-Location;
}

Write-Host "Source dir: "$sourceDir;

$sourceDirName = Get-FolderName $sourceDir;
$outputFolder = $destDir + "\" + $sourceDirName;

if ( -Not(Test-Path -Path $outputFolder)) {
    New-Item -Path $outputFolder -ItemType Directory;
}

Write-Host "Dest dir: $outputFolder";

get-childitem -path $sourceDir -File | % { $_.Name } | Out-File $outputFolder"\"$sourceDirName".content.txt";

get-childitem -path $sourceDir -Directory -Recurse |
    % { $_.FullName } |
    Foreach { 
        $currentFolder = Get-FolderName $_;
		get-childitem -path $_ -File | % { $_.Name } | Out-File $outputFolder"\"$currentFolder".content.txt";
	}

get-childitem -path $sourceDir -Directory -Recurse |
    % {$_.FullName, $_.Name} |
    Out-File "$outputFolder\table-of-content.txt"

function Get-FolderName {
    param([Parameter(Mandatory=$true)] [string] $path);

    $splittedPath = $path.Split("\");
    return $splittedPath[$splittedPath.Length-1];
}