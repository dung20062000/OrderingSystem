import { CommonModule } from '@angular/common';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppSessionService } from './session/app-session.service';
import { AppUrlService } from './nav/app-url.service';
import { AppAuthService } from './auth/app-auth.service';
import { AppRouteGuard } from './auth/auth-route-guard';
import { LocalizePipe } from '@shared/pipes/localize.pipe';

import { AbpPaginationControlsComponent } from './components/pagination/abp-pagination-controls.component';
import { AbpValidationSummaryComponent } from './components/validation/abp-validation.summary.component';
import { AbpModalHeaderComponent } from './components/modal/abp-modal-header.component';
import { AbpModalFooterComponent } from './components/modal/abp-modal-footer.component';
import { LayoutStoreService } from './layout/layout-store.service';

import { BusyDirective } from './directives/busy.directive';
import { EqualValidator } from './directives/equal-validator.directive';
//-------
import { ButtonModule } from 'primeng/button';
import { FileUpload, FileUploadModule } from 'primeng/fileupload';
import { AngularFireModule } from "@angular/fire/compat";
import { AngularFireStorageModule } from "@angular/fire/compat/storage";
import { environment } from 'environments/environment';
import {TableModule} from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ModalDirective, ModalModule } from 'ngx-bootstrap/modal';
import { MultiSelectModule } from 'primeng/multiselect';
import { DropdownModule } from 'primeng/dropdown';
// import { NgxSpinnerModule, NgxSpinnerComponent } from 'ngx-spinner';


@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        NgxPaginationModule,
        ButtonModule,
        FileUploadModule,
        AngularFireModule.initializeApp(environment.firebaseConfig),
        AngularFireStorageModule,
        TableModule,
        PaginatorModule,
        ModalModule,
        MultiSelectModule,
        DropdownModule
        // NgxSpinnerModule.forRoot()
    ],
    declarations: [
        AbpPaginationControlsComponent,
        AbpValidationSummaryComponent,
        AbpModalHeaderComponent,
        AbpModalFooterComponent,
        LocalizePipe,
        BusyDirective,
        EqualValidator,
    ],
    exports: [
        AbpPaginationControlsComponent,
        AbpValidationSummaryComponent,
        AbpModalHeaderComponent,
        AbpModalFooterComponent,
        LocalizePipe,
        BusyDirective,
        EqualValidator,
        FileUpload,
        ButtonModule,
        TableModule,
        PaginatorModule,
        MultiSelectModule,
        DropdownModule
        // NgxSpinnerModule
    ],
    providers: [],
})
export class SharedModule {
    static forRoot(): ModuleWithProviders<SharedModule> {
        return {
            ngModule: SharedModule,
            providers: [
                AppSessionService,
                AppUrlService,
                AppAuthService,
                AppRouteGuard,
                LayoutStoreService
            ]
        };
    }
}
