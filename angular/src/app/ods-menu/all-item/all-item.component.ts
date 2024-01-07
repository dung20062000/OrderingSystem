import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { ItemServiceProxy } from '@shared/service-proxies/service-proxies';
import { PaginationParamsModel } from '@shared/common/models/base.model';
import { Table } from 'primeng/table';
import { ceil } from 'lodash';

@Component({
  selector: 'app-all-item',
  templateUrl: './all-item.component.html',
  styleUrls: ['./all-item.component.css']
})
export class AllItemComponent extends AppComponentBase {
  filterText;
  sorting: string = "";
  paginationParams: PaginationParamsModel;
  loading : boolean = true;
  maxResultCount: number = 20;
  rowData: any;
  constructor(
    injector: Injector,
    private _itemServiceProxy: ItemServiceProxy,
    ) {
    super(injector);
  }
  ngOnInit() {
    this.rowData = [];
    this.paginationParams = { pageNum: 1, pageSize: 20, totalCount: 0 };
    this.getAll(this.paginationParams).subscribe(data => {
      console.log(data.items);
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
}
