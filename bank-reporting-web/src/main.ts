import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

export function getBaseAppPath() {
  // Define regex
  const regEx = /https?:[/]{2}[^/]+(\/[^/]*)\//ig;

  // Get it
  const res = regEx.exec(document.getElementsByTagName('base')[0].href);
  if (res  != null) {
    return res[1];
  } else {
    return'/';
  }
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
  { provide: 'BASE_APP_PATH', useFactory: getBaseAppPath, deps: [] },
];

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.error(err));
