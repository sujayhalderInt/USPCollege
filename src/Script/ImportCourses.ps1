Param([string] $importDomain)

[System.Net.ServicePointManager]::MaxServicePointIdleTime = 5000000

$uri = "${importDomain}/umbraco/api/CourseImportApi/GetCourses";

$date = Get-Date
Write-Output "Starting Import $date"
try 
{
    $results = Invoke-WebRequest -Uri $uri -TimeoutSec 300 | ConvertFrom-Json

    Write-Output "Processed $($results.REMS_Records) records."
    Write-Output "$($results.Created) records were created."
    Write-Output "$($results.Updated) records were updated."

    if ($results.Invalid -gt 0)
    {
        Write-Output "$($results.Invalid) records were invalid:"
        foreach ($err in $results.Errors)
        {
            Write-Output $err
        }
    }

}
catch
{
    $error = $_.Exception
    Write-Output "ERROR: $_.Exception"
}
Write-Output "Completed Import Task"