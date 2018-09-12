import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";
import { AppModule } from "./app/app.module";

require("webpack-jquery-ui");
require("webpack-jquery-ui/css");

export const platformRef: any = platformBrowserDynamic();

export function main(): void {
  return platformRef.bootstrapModule(AppModule)
    .catch(err => console.error(err));
}

// support async tag or hmr
switch (document.readyState) {
  case "interactive":
  case "complete":
    main();
    break;
  case "loading":
  default:
    document.addEventListener("DOMContentLoaded", () => main());
}
