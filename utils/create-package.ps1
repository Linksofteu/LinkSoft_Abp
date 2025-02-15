# Set the working directory to the location of the script's file
Set-Location -Path (Split-Path -Parent $MyInvocation.MyCommand.Definition)

# Ask the user to enter the name of the package
$package_name = Read-Host "Enter the name of the package"

# Ask the user to enter the description of the package
$package_description = Read-Host "Enter the description of the package"

# Ask about creating an abstractions package
$create_abstractions = Read-Host "Do you want to create an .Abstractions package? (Y/n) [Y]"
if ([string]::IsNullOrWhiteSpace($create_abstractions)) {
    $create_abstractions = "Y"
}

# Create the main package
./add-package.ps1 -PackageName $package_name -PackageDescription $package_description -SkipModule $false

# Create the abstractions package if requested
if ($create_abstractions.ToUpper() -eq "Y") {
    $abstractions_name = "$package_name.Abstractions" 
    $abstractions_description = "Abstractions package for the package LinkSoft.Abp.$package_name"
    ./add-package.ps1 -PackageName $abstractions_name -PackageDescription $abstractions_description -SkipModule $true
}

Write-Host "All packages created successfully." 