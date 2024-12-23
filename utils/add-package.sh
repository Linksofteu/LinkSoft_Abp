#!/bin/bash
# Set the working directory to the location of the script's file
cd "$(dirname "$0")"

# Ask the user to enter the name of the package
echo "Enter the name of the package:"
read package_name

# Ask the user to enter the description of the package
echo "Enter the description of the package:"
read package_description

# Determine the target namespace and folder based on the package type
target_namespace="LinkSoft.Abp.$package_name"
target_folder="../src/$target_namespace"

# Copy all files from the templates folder to the target folder
mkdir -p "$target_folder"
cp -r ../templates/* "$target_folder"

# Rename relevant files
mv "$target_folder/template.csproj" "$target_folder/$target_namespace.csproj"
mv "$target_folder/templateModule.cs" "$target_folder/${package_name}Module.cs"

# Replace {FullName} with {target_namespace} in the copied .csproj and Module files
sed -i "s/{FullName}/$target_namespace/g" "$target_folder/$target_namespace.csproj" "$target_folder/${package_name}Module.cs"

# Replace {Description} with the inputted description in the copied .csproj file
sed -i "s/{Description}/$package_description/g" "$target_folder/$target_namespace.csproj"

# Create the folder structure in the same folder as the .csproj
mkdir -p "$target_folder/LinkSoft/Abp/$package_name"

echo "Package setup completed successfully."