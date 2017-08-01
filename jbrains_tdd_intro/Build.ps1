# Build up path to MSBuild.exe
$msBuildPath = [IO.Directory]::GetFiles(
	[IO.Path]::Combine([Environment]::GetFolderPath([Environment+SpecialFolder]::ProgramFilesX86), "MSBuild"),
	"MSBuild.exe",
	[IO.SearchOption]::AllDirectories
) | Sort-Object -Descending | Select-Object -First 1

& '..\Tools\NuGet.exe' restore .\jbrains_tdd_intro.sln
& $msBuildPath jbrains_tdd_intro.sln /m /v:m /nologo
