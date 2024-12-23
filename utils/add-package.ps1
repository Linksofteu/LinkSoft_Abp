# Set the working directory to the location of the script's file
Set-Location -Path (Split-Path -Parent $MyInvocation.MyCommand.Definition)

# Ask the user to enter the name of the package
$package_name = Read-Host "Enter the name of the package"

# Ask the user to enter the description of the package
$package_description = Read-Host "Enter the description of the package"

# Determine the target namespace and folder based on the package type
$target_namespace = "LinkSoft.Abp.$package_name"
$target_folder = "../src/$target_namespace"

# Copy all files from the templates folder to the target folder
New-Item -ItemType Directory -Force -Path $target_folder
Copy-Item -Recurse -Path ../templates/* -Destination $target_folder

# Rename relevant files
Rename-Item -Path "$target_folder/template.csproj" -NewName "$target_namespace.csproj"
Rename-Item -Path "$target_folder/templateModule.cs" -NewName "${package_name}Module.cs"

# Replace {FullName} with {target_namespace} in the copied .csproj and Module files
(Get-Content "$target_folder/$target_namespace.csproj") -replace '{FullName}', $target_namespace | Set-Content "$target_folder/$target_namespace.csproj"
(Get-Content "$target_folder/${package_name}Module.cs") -replace '{FullName}', $target_namespace | Set-Content "$target_folder/${package_name}Module.cs"

# Replace {Description} with the inputted description in the copied .csproj file
(Get-Content "$target_folder/$target_namespace.csproj") -replace '{Description}', $package_description | Set-Content "$target_folder/$target_namespace.csproj"

# Create the folder structure in the same folder as the .csproj
New-Item -ItemType Directory -Force -Path "$target_folder/LinkSoft/Abp/$package_name"

Write-Host "Package setup completed successfully."