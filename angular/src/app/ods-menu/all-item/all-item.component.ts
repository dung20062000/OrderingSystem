import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { ItemServiceProxy } from '@shared/service-proxies/service-proxies';
import { PaginationParamsModel } from '@shared/common/models/base.model';
import { Table } from 'primeng/table';
import { ceil } from 'lodash';
import { CreateOrEditItemComponent } from '../create-or-edit-item/create-or-edit-item.component';
import { BsModalService } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-all-item',
  templateUrl: './all-item.component.html',
  styleUrls: ['./all-item.component.css']
})
export class AllItemComponent extends AppComponentBase {
  //@ViewChild('#createOrEditItemComponent') createOrEditItemComponent: CreateOrEditItemComponent;
  filterText;
  sorting: string = "";
  paginationParams: PaginationParamsModel;
  loading : boolean = true;
  maxResultCount: number = 20;
  rowData: any;
  constructor(
    injector: Injector,
    private _itemServiceProxy: ItemServiceProxy,
    private _modalService: BsModalService
    ) {
    super(injector);
  }
  ngOnInit() {
    this.rowData = [];
    this.paginationParams = { pageNum: 1, pageSize: 20, totalCount: 0 };
    this.getAll(this.paginationParams).subscribe(data => {
      this.rowData = data.items;
      this.paginationParams.totalPage = ceil(data.totalCount / this.maxResultCount);
      this.paginationParams.totalCount = data.totalCount;
    });
  }

  getAll(paginationParams: PaginationParamsModel) {
    return this._itemServiceProxy.getAll(
      this.filterText,
      this.sorting ?? null,
      paginationParams ? paginationParams.skipCount : 0,
      paginationParams ? paginationParams.pageSize : 20,

    );
  }

  clear(table: Table) {
    table.clear();
  }
  createItem() {
   this._modalService.show(
    CreateOrEditItemComponent,
    {
      class: 'modal-lg',
    }
  );
  }
}
