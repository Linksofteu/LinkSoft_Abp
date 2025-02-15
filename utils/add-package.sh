#!/bin/bash
# Set the working directory to the location of the script's file
cd "$(dirname "$0")"

# Get parameters from command line arguments
package_name="$1"
package_description="$2"
skip_module="$3"

# Validate required parameters
if [ -z "$package_name" ] || [ -z "$package_description" ]; then
    echo "Usage: $0 <package_name> <package_description> [skip_module]"
    exit 1
fi

# Determine the target namespace and folder based on the package type
target_namespace="LinkSoft.Abp.$package_name"
target_folder="../src/$target_namespace"

# Copy all files from the templates folder to the target folder
mkdir -p "$target_folder"
cp -r ../templates/* "$target_folder"

# If skip_module is set, remove the module file
if [ "$skip_module" = "true" ]; then
    rm "$target_folder/templateModule.cs"
else
    # Rename module file only if we're not skipping it
    mv "$target_folder/templateModule.cs" "$target_folder/${package_name}Module.cs"
    sed -i "s/{PackageName}/$package_name/g" "$target_folder/${package_name}Module.cs"
    sed -i "s/{FullName}/$target_namespace/g" "$target_folder/${package_name}Module.cs"
fi

# Rename and update .csproj file
mv "$target_folder/template.csproj" "$target_folder/$target_namespace.csproj"
sed -i "s/{FullName}/$target_namespace/g" "$target_folder/$target_namespace.csproj"
sed -i "s/{PackageName}/$package_name/g" "$target_folder/$target_namespace.csproj"
sed -i "s/{Description}/$package_description/g" "$target_folder/$target_namespace.csproj"

# Create the folder structure in the same folder as the .csproj
mkdir -p "$target_folder/LinkSoft/Abp/$package_name"

# Add the newly created .csproj file to the solution file
dotnet sln ../LinkSoft_Abp.sln add "$target_folder/$target_namespace.csproj"

echo "Package setup completed successfully."