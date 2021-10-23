function Replace($file, $before, $after)
{
    $content = Get-Content $file | Foreach-Object {$_ -replace $before, $after }
    $content | Set-Content $file -Encoding UTF8
}

function UpdateVersion($project, $version)
{
    $file = "./$project/Properties/AssemblyInfo.cs"
    Replace $file "AssemblyVersion\s*\([^\)]+\)"     "AssemblyVersion    (`"$version`")"
    Replace $file "AssemblyFileVersion\s*\([^\)]+\)" "AssemblyFileVersion(`"$version`")"
}

function UpdateVsVersion($project, $version)
{
    UpdateVersion $project $version
        
    Replace "./$project/UntabifyReplacementPackage.cs" `
            "\[InstalledProductRegistration\(`"#110`", `"#112`", `"[^`"]+`"\)\]" `
            "[InstalledProductRegistration(`"#110`", `"#112`", `"$version`")]"
        
    Replace "./$project/source.extension.vsixmanifest" `
            "6897f9b0-a95a-4e02-88d5-46484a59ac9e`" Version=`"[^`"]+`"" `
            "6897f9b0-a95a-4e02-88d5-46484a59ac9e`" Version=`"$version`""
}