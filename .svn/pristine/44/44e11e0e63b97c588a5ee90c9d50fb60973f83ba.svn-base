function Merge-Csv {
    param (
        [string]$InputFolder,
        [string]$OutputFileCsv
    )
    
    # Delete old output file
    if ([System.IO.File]::Exists($OutputFileCsv)) {
        Echo "Deleted output file";
        Remove-Item $OutputFileCsv;
    }

    # Get all files in order - Sort-Object cast to integer to get integer-sorting instead of string-sorting (1, 10, 100, 2, 3,... vs 1, 2, 3, 10, 100,...)
    $files = Get-ChildItem $InputFolder -Exclude $OutputFileCsv | Sort-Object {[int]($_.BaseName -replace '\D')};

    # Create new output file
    New-Item $OutputFileCsv;

    # Iterate files,
    #  > First file, also copy first row
    #  > Not-First file, skip first row
    $isFirst = $true;

    for ($i = 0; $i -lt $files.Count; $i ++) {
        $lines = Get-Content -Path $files[$i].FullName;

        $start = 1;
        if ($isFirst -eq $true) {
            $start = 0;
            $isFirst = $false;
        }

        echo "Appending file " + $files[$i].FullName + " starting on line " + $start;
        for ($j = $start; $j -lt $lines.Count; $j ++) {
            $lines[$j] | Out-File -FilePath $OutputFileCsv -Append;
        }
    }
}

# Input parameters
$inputfolder01 = $args[0]
$outputfile01 = $args[1]

# $inputfolder01 = "C:\Users\hans-peter.dugardein\Desktop\csvs";
# $outputfile01 = "C:\Users\hans-peter.dugardein\Desktop\csvs\_output.csv";

Merge-Csv -InputFolder $inputfolder01 -OutputFileCsv $outputfile01