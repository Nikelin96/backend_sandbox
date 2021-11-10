function Get-WMIInfo {
    [cmdletbinding()]
    param(
    [parameter(Mandatory=$true)]
    [string]
    $lookup,
    [parameter(Mandatory=$false)]
    [double]
    $version,
    [parameter(Mandatory=$false)]
    [int]
    $typeNumber
    )

    Write-Debug "[$lookup] reference requested."

    Switch ($lookup) {

        {$_ -eq 'osversion'} {

            Write-Debug "Looking up [$version]."

            Switch ($version) {

                10{$versionText = 'Windows 10'}

                6.3 {$versionText = 'Windows 8.1 or Server 2012 R2'}

                6.2 {$versionText = 'Windows 8 or Server 2012'}

                6.1 {$versionText = 'Windows 7 or Server 2008 R2'}

                6.0 {$versionText = 'Windows Vista or Server 2008'}

                5.2 {$versionText = 'Windows Server 2003/2003 R2'}

                5.1 {$versionText = 'Windows XP'}

                5.0 {$versionText = 'Windows 2000'}

                Default {$versionText = 'Unable to determine version!'}

            }

            Write-Debug "[$version] matched with text [$versionText]"

            Return $versionText

        }

        {$_ -eq 'drivenumber'} {


            Write-Debug "Looking up drive # [$typeNumber]"

            Switch ($typeNumber) {

                0 {$typeText = 'Unknown'}

                1 {$typeText = 'No Root Directory'}

                2 {$typeText = 'Removeable Disk'}

                3 {$typeText = 'Local Disk'}

                4 {$typeText = 'Network Drive'}

                5 {$typeText = 'Compact Disk'}

                6 {$typeText = 'RAM Disk'}

                Default {$typeText = "Invalid type number [$typeNumber]"}

            }

            Write-Debug "[$typeNumber] matched with text [$typeText]"

            Return $typeText

        }
    }
}

function Get-OSInfo {
    [cmdletbinding()]
    Param(
    [parameter(Mandatory=$false)]
    [boolean]
    $getDiskInfo = $false
    )

    Write-Verbose "Looking up OS Information..."

    $osInfo = Get-WmiObject -Class Win32_OperatingSystem

    $versionNumber = $osInfo.Version.SubString(0, $osInfo.Version.LastIndexOf('.'))

    Write-Verbose "Looking up the matching windows edition for version #: [$versionNumber]"

    Write-Debug "Version number stored as [$versionNumber]"

    $versionText = Get-WMIInfo -lookup 'osversion' -version $versionNumber

    Write-Host `n"You're running $versionText"`n

    if ($getDiskInfo) {

        Write-Verbose "Gathing disk information via WMI..."

        $disks = Get-WmiObject -Class Win32_LogicalDisk

        if ($disks) {
            
            Write-Host `n"Disk information!"`n -ForegroundColor White -BackgroundColor Black
            
            Foreach ($disk in $disks) {

                Write-Host `n"Device ID    : $($disk.DeviceID)"
                Write-Host ("Free Space   : {0:N2} GB" -f $($disk.FreeSpace / 1GB))
                Write-Host ("Total Size   : {0:N2} GB" -f $($disk.Size / 1GB))
                Write-Host ("% Free       : {0:P0}   " -f $($disk.FreeSpace / $disk.Size))
                Write-Host "Volume Name  : $($disk.VolumeName)"
                Write-Host "Drive Type   : $(Get-WMIInfo -lookup 'drivenumber' -typeNumber $disk.DriveType)"`n

            }


        } else {

            Write-Host "Error getting disk info!"

        }

    }

}

Get-OSInfo -getDiskInfo $true -Debug