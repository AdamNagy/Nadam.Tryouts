$dotNET4_0_30319 = "C:\Windows\Microsoft.NET\Framework\v4.0.30319";
$currOutFolder = "C:\inetpub\wwwroot\miv\Server";
$compOutput = "C:\Documents\PowerShell_scripts\compOutput.txt";

cd $dotNET4_0_30319;
.\csc /out:$currOutFolder\BIService.exe $currOutFolder\BIService.asmx.cs

$res = "" | Out-File $compOutput;

cd $currOutFolder;