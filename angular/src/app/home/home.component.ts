import { Component, Injector, ChangeDetectionStrategy } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { MessageService } from 'primeng/api';
import { AngularFireStorage } from '@angular/fire/compat/storage';
import { forkJoin } from 'rxjs';

@Component({
  templateUrl: './home.component.html',
  animations: [appModuleAnimation()],
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class HomeComponent extends AppComponentBase {

  path: string;
  constructor(
      injector: Injector,
      private fireStorage: AngularFireStorage
    ) {
    super(injector);
  }
  uploadedFiles: any[] = [];

  onUpload($event) {
    console.log($event);
    const promises: any[] = [];
    $event.currentFiles?.forEach(file => promises.push(this.fireStorage.upload(`multiple/${file.name}`, file)));
    const observable = forkJoin([promises]);
    observable.subscribe({
      next: value => console.log(value),
      complete: () => console.log('all done')
    })
  }
}
