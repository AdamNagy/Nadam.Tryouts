#C:\Documents\HTML\MyBitchSite\main\images\tmp\red
#remove ')' character from all files in the folder
DIR 'G:\VSTS\ym-prono\BackEnd\Miv.Web\App_Data\WebImages' | Rename-Item –NewName { $_.name –replace "porn-gallery-","" }