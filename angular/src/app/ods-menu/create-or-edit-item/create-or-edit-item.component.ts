import { Component, EventEmitter, Injector, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { CategoryServiceProxy, CreateOrEditItemDto, ItemServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef, BsModalService, ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-or-edit-item',
  templateUrl: './create-or-edit-item.component.html',
  styleUrls: ['./create-or-edit-item.component.css']
})
export class CreateOrEditItemComponent extends AppComponentBase {
  //@ViewChild("#createOrEditItemComponent", {static: true}) modal: ModalDirective;
  @Output() onSave: EventEmitter<any> = new EventEmitter<any>();

  saving: boolean = false;
  odsItem: CreateOrEditItemDto = new CreateOrEditItemDto;
  listCategory: any[] = [];
  selectCategory: any[] = [];

  constructor(
    injector : Injector,
    public bsModalRef: BsModalRef,
    private _cateGoryServiceProxy: CategoryServiceProxy,
    private _itemServiceProxy: ItemServiceProxy,
  ){
    super(injector)
  }
  ngOnInit(): void {
    this._cateGoryServiceProxy.getAllForCreateItem().subscribe(re => {
      re.forEach(e => this.listCategory.push({value: e.id, label: e.categoryName}))
    })
  }


  // show(itemId?: number): void {
  //   if(!itemId){
  //     this.odsItem = new CreateOrEditItemDto();
  //     this.odsItem.id = itemId;
  //   }
  // }

  onCategoriesChange(event: any): void {
    this.selectCategory = this.odsItem.categoryIds;
  }

  save(): void {
    this.saving = true;
    const listMap = this.selectCategory.map(e => e.value);
    this.odsItem.categoryIds = listMap;

    this._itemServiceProxy.createOrEdit(this.odsItem).subscribe(
      () => {
        this.notify.success(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      }, () => {
        this.saving = false;
      }
    )

    this.bsModalRef.hide();
  }

}
