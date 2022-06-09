param([String]$toCopy)

$originalImagesSource = "C:\Documents\PowerShell_scripts"
$dest = "C:\Documents\PowerShell_scripts\dest"

$filesToCopy = get-childitem -path $toCopy -Name

# Write-Output $filesToCopy

for($i = 0; $i -lt $filesToCopy.Count; $i += 1)
{    
	Write-Output "looking for:";
	Write-Output $filesToCopy[$i];

	$currentFile = $filesToCopy[$i];
	for($idx = 1; $idx -lt 3; $idx += 1)
	{
		$currentPath = "$originalImagesSource\$idx\$currentFile";
		Write-Output $currentPath;
		if (!(Test-Path $currentPath)) {
			continue
		}

		Write-Output "found in $idx";
		Copy-Item $currentPath -Destination "$dest"
		break
	}    
}