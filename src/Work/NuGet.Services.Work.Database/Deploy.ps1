$Root = $OctopusActionPackageCustomInstallationDirectory

Add-Type -Path "$Root\Microsoft.SqlServer.Dac.dll"

$options = New-Object Microsoft.SqlServer.Dac.DacDeployOptions
$options.BlockOnPossibleDataLoss = $true

$d = New-Object Microsoft.SqlServer.Dac.DacServices $OctopusParameters['Deployment.Sql.MasterConnection']
$p = New-Object Microsoft.SqlServer.Dac.DacPackage "$Root\NuGet.Services.Work.Database.dacpac"

# Deploy!
$d.Deploy($p, $OctopusParameters['Deployment.Sql.Database'], $true, $options)
