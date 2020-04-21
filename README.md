# Selene

[![Build Status](https://jamesdearlove.visualstudio.com/Selene/_apis/build/status/Selene%20MSI%20Build?branchName=master)](https://jamesdearlove.visualstudio.com/Selene/_build/latest?definitionId=3&branchName=master)

A system status bar built for Windows.

## Compatibility

Selene uses Windows 10 Runtimes and features only available on Windows 10 1809 and later

|       Windows Version       |    Status    |
| --------------------------- | ------------ |
| Windows 10 1803 and Earlier | Incompatible |
|       Windows 10 1809       |   Untested   |
|  Windows 10 1903 and Later  |  Compatible  |

## Building
Visual Studio 2019 with the .NET desktop development workload and Windows 10 SDK (10.0.18362.0) is required to build the project.

Ensure you complete a NuGet restore before attempting to build the solution.

## Releases

Official releases are coming soon. Development builds are available from the Azure Pipelines builds above. Please note that Selene is still early in development, these builds may be unstable and features may change until released.

## License

Selene is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## Acknowledgements

* [WinApi Library](https://github.com/prasannavl/WinApi)
* [WPFAppBar](https://github.com/PhilipRieck/WpfAppBar)
* [AudioFlyout](https://github.com/ADeltaX/AudioFlyout) - Music Component Inspiration
