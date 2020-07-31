import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-values',
  templateUrl: './values.component.html',
  styleUrls: ['./values.component.css']
})
export class ValuesComponent implements OnInit {

  constructor(private http: HttpClient) { }

  values: any[];

  ngOnInit(): void {
    this.getValues();
  }

  getValues() {
    this.http.get('http://localhost:5000/api/values').subscribe(
      (res: any[]) => this.values = res),
      error => console.log(error)
  }

}
