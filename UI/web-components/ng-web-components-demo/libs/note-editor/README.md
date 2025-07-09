To use text editor nee to add assets to the host app.

```
      {
         "input": "libs/note-editor/src/assets/",
         "glob": "**/*",
         "output": "/assets/"
      }
```

This sniped has to be added into the project json's targets -> build -> options -> assets array property

To be able to run the host app it needs to be built first, because serve does not copy the assets.
