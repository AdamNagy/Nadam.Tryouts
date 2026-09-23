# Browser smoke checks

Run from the solution directory:

```powershell
dotnet run --project SmokeChecks/SmokeChecks.csproj -p:OutputPath=bin/Smoke/
```

The check opens Chromium with an isolated temporary profile and uses locally fulfilled
requests (no external test site). It exercises the real panel and session HTML:

- Open the session through the panel and display recorded URLs.
- Close and reopen the session without losing visits.
- Record a popup's initial navigation.
- Navigate away from the session and open a replacement without overwriting that tab.
- Open the panel under enforced Trusted Types, and record SPA navigation.
- Preserve the panel after body replacement/removal and avoid duplicate panels on re-injection.
- Capture PNG, CSS background SVG and iframe images, omit failed responses, and display
  original bytes without fetching images again, including after reopening the session.
- Reject images below 256 pixels on either side before session storage, including
  255x512 and 512x255 boundary cases; verify PNG/JPEG/WebP/AVIF/GIF metadata and invalid data.

Append `-- --instagram` to also check the public Instagram page with the isolated,
signed-out profile. This optional check requires internet access.
Use `-- --pinterest` for the public Pinterest page, including a delayed check after loading.

The separate output directory avoids the executable lock when the POC is running.
The temporary profile is retained under the system temporary directory in
`Nadam.Playwright.Smoke` for troubleshooting; the normal browser profile is not used.
