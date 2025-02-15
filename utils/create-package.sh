#!/bin/bash
# Set the working directory to the location of the script's file
cd "$(dirname "$0")"

# Ask the user to enter the name of the package
echo "Enter the name of the package:"
read package_name

# Ask the user to enter the description of the package
echo "Enter the description of the package:"
read package_description

# Ask about creating an abstractions package
echo "Do you want to create an .Abstractions package? (Y/n) [Y]:"
read create_abstractions
create_abstractions=${create_abstractions:-Y}

# Create the main package
./add-package.sh "$package_name" "$package_description" "false"

# Create the abstractions package if requested
if [[ ${create_abstractions^^} == "Y" ]]; then
    abstractions_name="${package_name}.Abstractions"
    abstractions_description="Abstractions package for the package LinkSoft.Abp.$package_name"
    ./add-package.sh "$abstractions_name" "$abstractions_description" "true"
fi

echo "All packages created successfully." 