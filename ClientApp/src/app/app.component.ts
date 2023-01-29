import { Component, OnInit } from '@angular/core';
import { Product } from './models/product.model';
import { Repository } from './services/repository';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  
  product: Product = {}; // one product object
  products: Product[] = []; // array of products 
  //related: boolean = true;
  
  constructor(private repo: Repository) {

  }

  ngOnInit(): void {
    this.repo.getProduct(1).subscribe((result: Product) => (this.product = result));
    this.repo.getProducts().subscribe((result: Product[]) => (this.products = result));
  }

  // getProducts(){
  //   this.repo.getProducts(this.related).subscribe((result: Product[]) => (this.products = result));
  // }

  

}
