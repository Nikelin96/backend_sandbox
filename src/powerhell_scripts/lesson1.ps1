function Get-OSInfo {
    [cmdletbinding()]
    param()
    $osInfo = Get-WmiObject -Class Win32_OperatingSystem

    $versionText = Get-VersionText -version $osInfo.Version.SubString(0, $osInfo.Version.LastIndexOf("."))

    $processes = Get-Process
    Write-Debug "`$processes contains $($processes.Count) processes"
    
    Write-Host "You're running $versionText"
    Write-Verbose "Fuck, this is the verbose shit"
}

function Get-VersionText {
    [cmdletbinding()]
    param([double]$version)
    
    Switch ($version) {
    
        10.0 {$versionText = "Windows 10"}
        
        6.3 {$versionText = "Windows 8.1 or Server 2012 R2"}
        
        6.2 {$versionText = "Windows 8 or Server 2012"}
        
        6.1 {$versionText = "Windows 7 or Server 2008 R2"}
        
        6.0 {$versionText = "Windows Vista or Server 2008"}
        
        5.2 {$versionText = "Windows Server 2003/2003 R2"}
        
        5.1 {$versionText = "Windows XP"}
        
        5.0 {$versionText = "Windows 2000"}
    
        default{$versionText = "unkown version"}
    }
    
    Return $versionText
    
}

Get-OSInfo -Debug