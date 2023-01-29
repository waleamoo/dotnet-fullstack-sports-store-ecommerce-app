import { NgModule } from "@angular/core"
import { Repository } from "../services/repository"
import { HttpClientModule } from '@angular/common/http';
@NgModule({
  imports: [HttpClientModule],
  providers: [Repository]
})

export class ModelModel { }
