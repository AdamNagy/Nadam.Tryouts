get-childitem -path "C:\Documents\Data\TestImages\egyeb" | 
	?{ $_.PSIsContainer } | 
	select-object name | 
	Out-File "C:\Documents\PowerShell_scripts\babesData\_BabesName.txt" -encoding "utf8";

$babesNames = get-content  -path "C:\Documents\PowerShell_scripts\babesData\_BabesName.txt";
for($i = 3; $i -lt $babesNames.Count; $i+=1)
{
    $currBabe = $babesNames[$i].trim();
    $deleteContent = "" | Out-File "C:\Documents\PowerShell_scripts\babesData\$currBabe.txt" -encoding "utf8";
    $currLine = get-childitem -path "C:\Documents\Data\TestImages\egyeb\$currBabe" -filter "*.jpg" | 
		select-object Name, CreationTime | 
		Out-File "C:\Documents\PowerShell_scripts\babesData\$currBabe.txt" -encoding "utf8"
}