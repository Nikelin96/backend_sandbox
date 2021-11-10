#Begin function Write-Pretty
function Write-Pretty {
   <#   
   .SYNOPSIS   
      This function will output text in different ways depending on the arguments received.
   .DESCRIPTION 
      Write-Pretty will output text in the following ways:
   
       ------
      |Random|
       ------
       Random will add some spacing and split your text into an array using a space. It will take each part of the array and apply a random color to it.
   
       [Random] will be prepended to the text.
   
       -----
      |Error|
       -----
       Error will output the text as red.
   
       [Error] will be prepended to the text.
   
       -------
      |Warning|
       -------
       Warning will output the text as yellow.
   
       [Warning] will be prepended to the text.
   
       ----
      |Info|
       ----
       Info will output the text as as white, as is the default option if you do not specify any parameters. 
   
       [Info] will be prepended to the text.
      
   .PARAMETER prettyText
   
       Alias: Text
       Argument: This parameter is the text you'd like to affect the look of.
   
       This parameter accepts value from the pipeline.
      
   .PARAMETER textType
       Alias: Type
       Argument: This parameter affects the type of text you'd like to display.
   
       Valid types:
       Random
       Error
       Warning
       Info
   
   .NOTES   
       Name: part7.psm1
       Author: Ginger Ninja (Mike Roberts)
       DateCreated: 5/8/16
   
   .LINK  
       http://www.gngrninja.com/script-ninja/2016/5/8/powershell-getting-started-part-7-help  
          
   .EXAMPLE   
       Write-Pretty -Text 'This is some pretty text.' -Type Random
       ---------------------------------------------------------------
   
       [Random] This is some pretty text
   
   .EXAMPLE   
       Write-Pretty -Text 'This is some pretty text, and it is now an error.' -Type Error
       ---------------------------------------------------------------
   
       [Error] This is some pretty text, and it is now an error
   
   .EXAMPLE   
       Write-Pretty -Text 'Warning, this will output warning text!' -Type Warning
       ---------------------------------------------------------------
   
       [Warning] Warning, this will output warning text!
   
   .EXAMPLE   
       Write-Pretty -Text 'This will output info text.' -Type Info
       ---------------------------------------------------------------
   
       [Info] This will output info text.
   
   .EXAMPLE  
       Write-Pretty "I wonder what happens if we don't specify an option..."
       ---------------------------------------------------------------
   
       [Info] I wonder what happens if we don't specify an option...
   
   .EXAMPLE
       Get-Process | Select-Object -ExpandProperty Name -First 5 | Sort-Object Name | Write-Pretty
       ---------------------------------------------------------------
   
       [Info] Battlenet
       [Info] Battlenet Helper
       [Info] AGSService
       [Info] AdobeUpdateService
       [Info] Agent
   #>  
       [cmdletbinding()]
       param(
       [Parameter(
                   Mandatory         = $True,
                   ValueFromPipeline = $True
                  )]
       [Alias('Text')]
       $prettyText,
       [Parameter(Mandatory=$false)]
       [Alias('Type')]
       $textType
       )
   
       Begin {
       
           #Create a space before the text is displayed.
           Write-Host `n 
   
       }
   
       Process { #Begin process for Write-Pretty function
   
           ForEach ($textItem in $prettyText) { #Begin ForEach loop to handle prettyText input (normal and via pipeline)
   
               Switch ($textType) { #Begin switch for textType argument
   
                   {$_ -eq 'Random'} {
   
                       Write-Host -NoNewline "[" -ForegroundColor $(Get-Random -Minimum 1 -Maximum 15) 
                       Write-Host -NoNewline "R" -ForegroundColor $(Get-Random -Minimum 1 -Maximum 15)
                       Write-Host -NoNewline "andom" -ForegroundColor $(Get-Random -Minimum 1 -Maximum 15)
                       Write-Host -NoNewline "]" -ForegroundColor $(Get-Random -Minimum 1 -Maximum 15)
   
                       #Split the text into an array, split by spaces. (Also turns it into a string before the split).
                       #We needed to turn it into a string in case the type wasn't string when it was received. Or else the .Split() method wouldn't work
                       $writeText  = $textItem.ToString().Split(' ')
   
                       #Change the text color for each element in the array.
                       ForEach ($text in $writeText) {
   
                           Write-Host -NoNewLine " $text" -ForegroundColor $(Get-Random -Minimum 1 -Maximum 15)
   
                       }
   
                       Write-Host `n
               
                   }
   
                   {$_ -eq 'Error'} {
   
                       Write-Host -NoNewline "[" -ForegroundColor White 
                       Write-Host -NoNewline "Error" -ForegroundColor Red -BackgroundColor DarkBlue
                       Write-Host -NoNewline "]" -ForegroundColor White 
                       Write-Host " $textItem" -ForegroundColor Red 
   
                   }
   
   
                   {$_ -eq 'Warning'} {
   
                       Write-Host -NoNewline "[" -ForegroundColor White
                       Write-Host -NoNewline "Warning" -ForegroundColor Yellow -BackgroundColor Blue
                       Write-Host -NoNewline "]" -ForegroundColor White
                       Write-Host " $textItem" -ForegroundColor Yellow
   
   
                   }
   
                   {$_ -eq 'Info' -or $_ -eq $null} {
   
                       Write-Host -NoNewline "[" -ForegroundColor White
                       Write-Host -NoNewline "Info" -ForegroundColor Green -BackgroundColor Black
                       Write-Host -NoNewline "]" -ForegroundColor White
                       Write-Host " $textItem" -ForegroundColor White
   
                   }
   
                   #The default option will simply write the text with no changes. This is if you do not specify a valid option for textType.
                   Default { 
           
                       Write-Host $textItem
           
                   }
   
               } #End switch for textType argument
   
           } #End ForEach loop to handle prettyText input (normal and via pipeline)
   
       } #End process for Write-Pretty function
   
       End {
       
           Write-Host `n
   
       }
   
   } #End function Write-Pretty
   
   #Display this message when the module is imported.
   Write-Pretty -Text 'Part7 module loaded!' -textType Random