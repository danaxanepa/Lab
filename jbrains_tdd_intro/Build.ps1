param([string]$TargetEnvironment = "dev")

# Build up path to MSBuild.exe
$msBuildPath = [IO.Directory]::GetFiles(
	[IO.Path]::Combine([Environment]::GetFolderPath([Environment+SpecialFolder]::ProgramFilesX86), "MSBuild"),
	"MSBuild.exe",
	[IO.SearchOption]::AllDirectories
) | Sort-Object -Descending | Select-Object -First 1

& '..\Tools\NuGet.exe' restore .\jbrains_tdd_intro.sln

# Call MSBuild on the solution and pass in our Build and Environment parameters
$pParameters = "/p:Build=$Build;Environment=$TargetEnvironment"
& $msBuildPath jbrains_tdd_intro.sln /m /v:m $pParameters /nologo
