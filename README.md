# Applitrack UI Tests

UI automation tests for AppliTrack. This project uses Selenium and the Page Object Design Pattern.

## Project Contents

This project is organized into several folders:

-   `DataFiles/` - Contains text files listing all browsers available for testing. **NOTE** This has not been implemented yet.
-   `DataGenerators/` - Contains classes that generate data for use in test cases, for example, `UserData.cs` generates data needed to create a user.
-   `Helpers/` - Contains helper classes to perform some common tasks such as switching between IFrames, switching and closing windows, etc.
-   `PageObjects/` - Classes that contain UI Maps and Methods for pages in AppliTrack.
-   `TestCases/` - Contains all the current test cases.
-   `WorkFlows/` - Contains common workflows, for example, `LoginWorkflows.cs` contains methods to login as different types of users.

## Getting Started

Clone the project:

    git clone https://github.com/FrontlineTechnologies/AppliTrack-UI-AutomationTests.git

Set up visual studio to use the Frontline Artifactory server:

1.  Open Visual Studio NuGet / Library Package Manager.
2.  Select Highlight Online and Select Settings.
3.  Select the <+> button to add a new source:
    -   Name: Frontline Artifactory
    -   Source: <http://artifactory.flqa.net/artifactory/api/nuget/fl-nuget-virtual>
4.  Install the Frontline Automation package.
    -   This will install the required Selenium and Selenium Support Dependencies.
    -   The `Automation.dll` should register as a reference in the projects.

## Configuration

This project uses SlowCheetah to create tranformations of `App.config` files.

The `App.config` contains all configuration options for the project. All other files inherit these values.
    -   Modify the `BrowserType` value to choose the browser the tests will execute on, i.e. Chrome, Firefox, etc.

There is also an AppSettingsSecrets.config file in the same folder as ApplitrackUI_Automation.sln.  This file contains sensitive configuration items such as license keys that we do not want persisted in GitHub.  Please see a current team member for correct development values for this file.

The naming convention for transorms is as follows:
`<Server>-<Grid/Local>-<Environment>`
- <**Server**> - what server the test will run on, i.e. AWS QA, QA, Staging, or Production.
- <**Grid/Local**> - whether the tests will run locally or on the grid.
- <**Environment**> - if the tests run in a specific environment, e.g. Frontline Central integration, Jefferson.

### Current Configs

**Production** - www.applitrack.com/
- `Prod-Grid`
- `Prod-Local`
- `Prod-Grid-Integration`
- `Prod-Local-Integration`

**AwsQA** - qa.applitrack.com/
- `AwsQA-Grid`
- `AwsQA-Local`

**QA** - qa2.applitrack.com/
- `QA-Grid`
- `QA-Local`
- `QA-Grid-Integration`
- `QA-Local-Integration`

**Stage** - stage2.applitrack.com/
- `Stage-Grid`
- `Stage-Local`
- `Stage-Grid-Integration`
- `Stage-Local-Integration`
- `Stage-Grid-Jefferson`
- `Stage-Local-Jefferson`
