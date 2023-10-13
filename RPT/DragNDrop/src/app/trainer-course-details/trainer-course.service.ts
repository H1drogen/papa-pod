import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TrainerCourseService {
  httpHeaders = { headers: new HttpHeaders({ 'Content-Type': 'application/json' },) };
  apiUrl = 'https://localhost:50000/api/rpt/';
  constructor(private http: HttpClient) {

  }

  ngOnInit(): void {
    this.httpHeaders = { headers: new HttpHeaders({ 'Content-Type': 'application/json' },) };
  }


  GetData(classIs: any) {
    return this.http.get(this.apiUrl + classIs, {

      headers: new HttpHeaders({ 'accept': 'text/plain' },)
    });

  }


  DeleteData(classIs: any, id: any) {
    return this.http.delete(this.apiUrl + classIs + "/" + id, { headers: new HttpHeaders({ 'Content-Type': 'application/json' },) })

  }


  DeleteDataComposite(classIs: any, share_id: any, date: any) {
    return this.http.delete(this.apiUrl + classIs + "/" + share_id + " " + date, { headers: new HttpHeaders({ 'Content-Type': 'application/json' },) })

  }

  PostData(classIs: any, classData: any) {
    return this.http.post(this.apiUrl + classIs, classData, { headers: new HttpHeaders({ 'Content-Type': 'application/json' },) })

  }

  PutData(classIs: any, classData: any) {
    return this.http.put(this.apiUrl + classIs, classData, { headers: new HttpHeaders({ 'Content-Type': 'application/json', },) });
  }

  Logon(classIs: any, classData: any) {
    return this.http.post(this.apiUrl + classIs, classData);
  }

}
