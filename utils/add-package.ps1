param(
    [Parameter(Mandatory=$true)]
    [string]$PackageName,
    
    [Parameter(Mandatory=$true)]
    [string]$PackageDescription,
    
    [Parameter(Mandatory=$false)]
    [string]$SkipModule = "false"
)

# Set the working directory to the location of the script's file
Set-Location -Path (Split-Path -Parent $MyInvocation.MyCommand.Definition)

# If parameters not provided through command line, ask for them
if ([string]::IsNullOrEmpty($PackageName)) {
    $PackageName = Read-Host "Enter the name of the package"
}

if ([string]::IsNullOrEmpty($PackageDescription)) {
    $PackageDescription = Read-Host "Enter the description of the package"
}

# Convert string to boolean
$skipModuleBool = $SkipModule.ToLower() -eq "true"

# Determine the target namespace and folder based on the package type
$target_namespace = "LinkSoft.Abp.$PackageName"
$target_folder = "../src/$target_namespace"

# Copy all files from the templates folder to the target folder
New-Item -ItemType Directory -Force -Path $target_folder
Copy-Item -Recurse -Path "../templates/*" -Destination $target_folder

# If skip_module is set, remove the module file, otherwise rename and update it
if ($skipModuleBool) {
    Remove-Item "$target_folder/templateModule.cs"
} else {
    Rename-Item -Path "$target_folder/templateModule.cs" -NewName "${PackageName}Module.cs"
    (Get-Content "$target_folder/${PackageName}Module.cs") -replace '{PackageName}', $PackageName | Set-Content "$target_folder/${PackageName}Module.cs"
    (Get-Content "$target_folder/${PackageName}Module.cs") -replace '{FullName}', $target_namespace | Set-Content "$target_folder/${PackageName}Module.cs"
}

# Rename and update .csproj file
Rename-Item -Path "$target_folder/template.csproj" -NewName "$target_namespace.csproj"
(Get-Content "$target_folder/$target_namespace.csproj") -replace '{FullName}', $target_namespace | Set-Content "$target_folder/$target_namespace.csproj"
(Get-Content "$target_folder/$target_namespace.csproj") -replace '{PackageName}', $PackageName | Set-Content "$target_folder/$target_namespace.csproj"
(Get-Content "$target_folder/$target_namespace.csproj") -replace '{Description}', $PackageDescription | Set-Content "$target_folder/$target_namespace.csproj"

# Create the folder structure in the same folder as the .csproj
New-Item -ItemType Directory -Force -Path "$target_folder/LinkSoft/Abp/$PackageName"

# Add the newly created .csproj file to the solution file
& dotnet sln ../LinkSoft_Abp.sln add "$target_folder/$target_namespace.csproj"

Write-Host "Package setup completed successfully."