import { Component, OnInit } from '@angular/core';



@Component({
  selector: 'app-dand',
  templateUrl: './dand.component.html',
  styleUrls: ['./dand.component.css']
})
export class DandComponent  {

  constructor() { }

  // Here I have used the static employeeList you can use dynamic employeeList
  employeeList = [
    { empName: "Arsenal", designation: "Premier League" },
    { empName: "PSG", designation: "La Ligue" },
    { empName: "Napoli", designation: "Serie A" },
    { empName: "Real Madrid", designation: "La Liga" },
    
  ];

  droppedEmployeeList = [
    { empName: "Entract Frankfurt", designation: "Bundesleaga" },
  ];

  addDragDropItem(e: any) {
    this.droppedEmployeeList.push(e.dragData);
    console.log(e.dragData);
    const index = this.employeeList.indexOf(e.dragData);
    if (index > -1) {
      this.employeeList.splice(index, 1);
    }
  }

  removeDragDropItem(e: any) {
    this.employeeList.push(e.dragData);
    const index = this.droppedEmployeeList.indexOf(e.dragData);
    if (index > -1) {
      this.droppedEmployeeList.splice(index, 1);
    }
  }
}
