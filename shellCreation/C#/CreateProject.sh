echo "Enter the project name"
read PROJECT_NAME

mkdir "$PROJECT_NAME"
cd "$PROJECT_NAME"

echo "Creating API..."
dotnet new webapi -o "$PROJECT_NAME" -v q
echo "API Created"

echo "Creating test projects..."
dotnet new xunit -o "$PROJECT_NAME.Tests" -v q
dotnet new xunit -o "$PROJECT_NAME.IntegrationTests" -v q
echo "Test porjects created"

echo "Creating solutions and adding references"
dotnet new sln -n "$PROJECT_NAME" -v q

dotnet sln add ./$PROJECT_NAME
dotnet sln add ./$PROJECT_NAME.Tests
dotnet sln add ./$PROJECT_NAME.IntegrationTests

dotnet add ./$PROJECT_NAME.Tests reference ./$PROJECT_NAME
dotnet add ./$PROJECT_NAME.IntegrationTests reference ./$PROJECT_NAME
echo "References add"

cd "$PROJECT_NAME"
echo "Installing packages on WebApi"
dotnet add package dapper -n
dotnet add package newtonsoft.json -n
dotnet add package restsharp

cd ../$PROJECT_NAME.Tests
dotnet add package AutoBogus -n
dotnet add package nsubstitute

cd ../$PROJECT_NAME.IntegrationTests
dotnet add package AutoBogus -n
dotnet add package nsubstitute -n
dotnet add package Microsoft.AspNetCore.Mvc.Testing -n
dotnet add package Selenium.WebDriver
echo "Packages installed"

echo "Aplication $PROJECT_NAME ready!"
read any