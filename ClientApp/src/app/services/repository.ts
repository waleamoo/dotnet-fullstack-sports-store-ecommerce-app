import { Product } from "../models/product.model";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { Observable } from "rxjs";
import { Filter } from "../models/configClasses.repository";

const productsUrl: string = "/api/products";

@Injectable({
  providedIn: 'root'
})

export class Repository {

  filter: Filter = new Filter();

  constructor(private http: HttpClient) {
    this.filter.category = "soccer";
    this.filter.related = true;
    this.getProducts();  
  }

  public getProduct(id: number) : Observable<Product> {
                                // https://localhost:7166/api/products/1
    return this.http.get<Product>(environment.apiUrl + productsUrl + "/" + id);
  }

  getProducts() : Observable<Product[]> {
    let url = `${productsUrl}?related=${this.filter.related}`;
    if(this.filter.category){
      url += `&category=${this.filter.category}`;
    }
    
    if(this.filter.search){
      url += `&search=${this.filter.search}`;
    }
    
    return this.http.get<Product[]>(`${environment.apiUrl}${url}`);
  }


}
