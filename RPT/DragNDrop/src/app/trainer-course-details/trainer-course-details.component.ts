import { Component, OnInit } from '@angular/core';
import { TrainerCourseComponent } from './trainer-course.component';
import { TrainerCourseService } from './trainer-course.service';
import { Router } from '@angular/router';
import axios from 'axios';

@Component({
  selector: 'app-trainer-course-details',
  templateUrl: './trainer-course-details.component.html',
  styleUrls: ['./trainer-course-details.component.css']
})
export class TrainerCourseDetailsComponent implements OnInit {
  classIs = "courses";
  shareAre: any = [];
  error = null;
  errorMessage = "";

  deleteMessage = "";
  notDeleted = null;
  apiUrl = 'https://localhost:50000/api/rpt/courses';
  headers = {
    'Content-Type': 'application/json',
  };
  deleteQ = TrainerCourseComponent.askToDelete;
  constructor(private service: TrainerCourseService, private router: Router) { }

  ngOnInit(): void {
    this.GetData();
  }


  async GetData() {
    let allData : any[]
    this.errorMessage = "";
    this.error = null;
  //   this.service.GetData(this.classIs)
  //    .subscribe({
  //      next: dataIs => {
  //        console.log(dataIs);
  //        this.shareAre = dataIs;
  //        this.show_create = true;
  //      },
  //      error: error => {
  //        this.error = null;
  //        this.createPress = false;
  //        this.show_create = false;
  //        console.log(error.message);
  //        this.logonAgain = false;
  //        if (error.status === 0) {
  //          this.errorMessage = TrainerCourseComponent.logonAgain
  //          this.logonAgain = true
  //        }
  //        else {
  //          this.errorMessage = error.status
  //        }
  //      }
  //    }
  //  )
  //}

    await axios.get(this.apiUrl, { headers: this.headers },)
          .then(response => {
            console.log(response.data);
            allData = response.data;
            this.shareAre = allData;
          }
          )
    .catch((error) => {
      console.error(error);
    });
  }





}
