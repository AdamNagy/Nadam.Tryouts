function Get-FolderName {
    param([Parameter(Mandatory=$true)] [string] $path);

    $splittedPath = $path.Split("\");
    return $splittedPath[$splittedPath.Length-1];
}