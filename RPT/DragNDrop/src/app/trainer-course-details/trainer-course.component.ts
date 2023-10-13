import { Component, OnInit } from '@angular/core';
import { TrainerCourseService } from './trainer-course.service';




@Component({
  selector: 'app-trainer-course',
  //styleUrls: ['./trainer-course.component.css'],
  templateUrl: './trainer-course.component.html',
  providers: [TrainerCourseService]
})
export class TrainerCourseComponent implements OnInit {
  static errorIs = 404;
  static errorName = 'HttpErrorResponse';
  static logonAgain = "Session has ended, please logon again"
  static cannotLogon = 'Please Check Credentials';
  static usernotfound = "User not found or incorrect password"
  static askToDelete = "Do you want to delete "
  static cannotDelete = " cannot be deleted"
  static userNameIs: any;
  static redirectToLogon = '<a href="/logon" (click)="redirectToLogon($event)">Navigate</a>'
  ngOnInit(): void {

  }
}
