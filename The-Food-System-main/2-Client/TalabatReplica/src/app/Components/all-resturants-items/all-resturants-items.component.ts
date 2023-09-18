import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-all-resturants-items',
  templateUrl: './all-resturants-items.component.html',
  styleUrls: ['./all-resturants-items.component.css']
})
export class AllResturantsItemsComponent {
  @Input() RestaurantItem:any;
}
