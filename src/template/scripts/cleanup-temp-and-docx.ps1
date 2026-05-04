# keep it on hold
Get-ChildItem -Path C:\Temp -Include *.* -File -Recurse | foreach { $_.Delete()}


ls -Recurse *.docx | rm