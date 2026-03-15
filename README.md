## Setup & Run the Project

### Prerequisites
- Visual Studio 2022+ (or .NET 8 SDK)
- .NET 8.0 runtime

### Steps
1. Open the solution (`KikiCourier.sln`) in Visual Studio.
2. Set **KikiCourier** as the Startup Project.
3. Build the solution (`Ctrl + Shift + B`).

**Alternative (dotnet CLI):**
```bash
cd KikiCourier
dotnet restore
dotnet build

How to Run Tests:

In Visual Studio:
Open Test Explorer (Ctrl + E, T)
Click Run All Tests

Via dotnet CLI:

Bashcd KikiCourier.Tests
dotnet test

How to Run the Application (CLI)
Option A: Interactive Console (Recommended in VS)

Press F5 (or dotnet run).
Paste your input.
Press Enter on a blank line to finish.

Example Input (Problem 02 - Sample from PDF):
text100 5
PKG1 50 30 OFR001
PKG2 75 125 OFR008
PKG3 175 100 OFR003
PKG4 110 60 OFR002
PKG5 155 95 NA
2 70 200
Expected Output:
textPKG1 0 750 3.98
PKG2 0 1475 1.78
PKG3 0 2350 1.42
PKG4 105 1395 0.85
PKG5 0 2125 4.19
Option B: Using Input File (Fastest)
Bash# Create input.txt with the sample above
KikiCourier.exe < input.txt
Problem 01 sample (no vehicle line):
BashKikiCourier.exe < input1.txt
Output will show only 3 columns (no delivery time).



Error Handling Examples

Invalid weight → ERROR: Package PKG1 - Weight must be positive integer
Package too heavy → ERROR: Package PKG1 (300kg) exceeds max carriable weight (200kg)
Empty input → ERROR: Input is empty. Please provide at least the first line.

What I’d Do Next with More Time:

Convert to ASP.NET Core Web API with Swagger + proper DTOs.
Add persistent storage (SQLite/EF Core) for packages, offers, and vehicles.
Implement real routing using Google OR-Tools or a graph algorithm.
Add unit tests for DeliveryTimeEstimator edge cases (e.g., many vehicles, zero packages).
Create a simple Angular UI for easy input and visualization.
Add logging (Serilog) and configuration (appsettings.json).
Dockerize the app + GitHub Actions CI/CD pipeline with code coverage.