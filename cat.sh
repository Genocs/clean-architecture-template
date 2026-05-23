# Delete ./out files
rm -rf ./out/Genocs.CleanArchitecture.Template.5.1.0.nupkg
# Delete ./src/bin folder
rm -rf ./src/bin
# Delete ./src/obj folder
rm -rf ./src/obj

# Uninstall the template
dotnet new uninstall Genocs.CleanArchitecture.Template

# Pack and install the template
dotnet pack ./src/Package.Template.csproj -p:PackageVersion=5.1.0 --configuration Release --output ./out
dotnet new install ./out/Genocs.CleanArchitecture.Template.5.1.0.nupkg

# Create a new project using the template
cd ..

echo "🚀 Creating a new project with In-Memory database and Rebus service bus..."
dotnet new gnx-cleanarchitecture --name VulcanMES --database inmemory --service-bus rebus --use-cases full

# Jump to the project folder
cd VulcanMES

# Build the solution
echo "🚀 Building the solution..."
dotnet build VulcanMES.slnx

# Run the tests
echo "🚀 Running the tests..."
dotnet test VulcanMES.slnx --no-build

# Run the application
# dotnet run --project ./src/WebApi

# Create a new project using the template with different options
# write a log message with an emoji

echo "🚀 Deleting the project folder... (./VulcanMES) if it exists"
cd ..
rm -rf ./VulcanMES

echo "🚀 Creating the project with SQL Server and Rebus service bus..."
dotnet new gnx-cleanarchitecture --name VulcanMES --database sqlserver --service-bus rebus --use-cases full

# Jump to the project folder
cd VulcanMES

# Build the solution
dotnet build VulcanMES.slnx

# Run the tests
dotnet test VulcanMES.slnx --no-build


echo "🚀 Deleting the project folder... (./VulcanMES) if it exists"
# Delete the project folder
cd ..
rm -rf ./VulcanMES

echo "🚀 Creating the project with MongoDB and Rebus service bus..."
dotnet new gnx-cleanarchitecture --name VulcanMES --database mongodb --service-bus rebus --use-cases full

# Jump to the project folder
cd VulcanMES

# Build the solution
dotnet build VulcanMES.slnx

# Run the tests
dotnet test VulcanMES.slnx --no-build

cd ..
rm -rf ./VulcanMES