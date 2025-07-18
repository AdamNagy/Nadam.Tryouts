import '@ng-web-components-demo/web-components';

// import 'ace-builds/src-noconflict/ace';
// import * as ace from 'ace-builds';
// import 'brace';
// import 'brace/mode/text';
// import 'brace/theme/github';

import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';

bootstrapApplication(App, appConfig).catch((err) => console.error(err));
