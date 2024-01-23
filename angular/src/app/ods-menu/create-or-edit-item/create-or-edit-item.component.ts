import { Component, EventEmitter, Injector, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { CategoryServiceProxy, CreateOrEditItemDto, ItemServiceProxy, DiscountServiceProxy } from '@shared/service-proxies/service-proxies';
import { forEach } from 'lodash-es';
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
  //checked: boolean = false;
  odsItem: CreateOrEditItemDto = new CreateOrEditItemDto;
  listCategory: any[] = [];
  listDiscount: any[] = [];
  selectCategory: any[] = [];
  selectedDiscount: { value: any, label: string };

  constructor(
    injector : Injector,
    public bsModalRef: BsModalRef,
    private _cateGoryServiceProxy: CategoryServiceProxy,
    private _itemServiceProxy: ItemServiceProxy,
    private _discountServerProxy: DiscountServiceProxy
  ){
    super(injector)
  }
  ngOnInit(): void {

    this._cateGoryServiceProxy.getAllForCreateItem().subscribe(re => {
      re.forEach(e => this.listCategory.push({value: e.id, label: e.categoryName}))
    })
    this._discountServerProxy.getAllForCreateItem().subscribe(re => {
      re.forEach(e => this.listDiscount.push({value: e.id, label: e.discountName}))
    });
  }

  onCategoriesChange(event: any): void {
    this.selectCategory = this.odsItem.categoryIds;
  }

  onDiscountChange() {
    if (this.selectedDiscount && typeof this.selectedDiscount.value === 'number') {
      this.odsItem.discountId = this.selectedDiscount.value;
    } else {
      this.odsItem.discountId = undefined;
    }
  }

  save(): void {
    this.saving = true;
    const listMap = this.selectCategory.map(e => e.value);
    this.odsItem.categoryIds = listMap;
    // this.odsItem.isDisplay = this.checked;
    console.log("checked",  this.odsItem.isDisplay);

    // this._itemServiceProxy.createOrEdit(this.odsItem).subscribe(
    //   () => {
    //     this.notify.success(this.l('SavedSuccessfully'));
    //     this.bsModalRef.hide();
    //     this.onSave.emit();
    //   }, () => {
    //     this.saving = false;
    //   }
    // )

    this.bsModalRef.hide();
  }

}
