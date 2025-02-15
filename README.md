# LinkSoft
Repository for open source Linksoft shared libraries based on Abp Framework

## Create a new package

There are two ways to create new packages:

### Using the Create Package Scripts (Recommended)
Run either:
- `utils/create-package.sh` (Bash)
- `utils/create-package.ps1` (PowerShell)

These scripts will prompt you for:
- Name of the package
- Package description
- Whether to create an accompanying .Abstractions package

### Using the Add Package Scripts Directly
If you need more control, you can use the add-package scripts directly:

**PowerShell:**
```powershell
.\utils\add-package.ps1 -PackageName "PackageName" -PackageDescription "Package Description" -SkipModule $false
```

**Bash:**
```bash
./utils/add-package.sh "PackageName" "Package Description" false
```

### Notes
- The `-SkipModule` parameter is optional and defaults to `false`.


Both approaches will automatically scaffold .csproj and module files and corresponding file structure to an appropriate location in the project.

The created package will follow the standard structure:
- src/LinkSoft.Abp.[PackageName]/
  - LinkSoft/Abp/[PackageName]/
  - [PackageName]Module.cs (if not skipped)
  - LinkSoft.Abp.[PackageName].csproj