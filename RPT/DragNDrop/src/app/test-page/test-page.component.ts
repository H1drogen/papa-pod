import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-test-page',
  templateUrl: './test-page.component.html',
  styleUrls: ['./test-page.component.css']
})
export class TestPageComponent implements OnInit {
  arrayIs = [];
  editIndex = null;
  rowIs = null;
  filterIs: number = 0;
  buttonType = "";
  valueIs: number = 0;
  numbersAre: number[] = [];
  filterData: number[] = [];
  numberGen: number = 13;
  numberOnRow = 6;
  randonNumberMax = 31;
  constructor() { }

  ngOnInit(): void {

    this.generateNumbers();
  }


  generateNumbers() {
    let iCounter = 1;
    this.editIndex = null;
    this.numbersAre = [];

    do {
      this.numbersAre.push
        (
          Math.floor(Math.random() * (this.randonNumberMax - 1) + 1)
        )
      iCounter += 1;
    } while (iCounter < this.numberGen + 1)
    iCounter = 1;
    let index = 0
    this.filterData = this.numbersAre;

  }

  filterItOut() {
    let iCounter = 0;
    this.editIndex = null;
    this.filterData = [];

    do {
      if (this.numbersAre[iCounter] === this.filterIs)

        this.filterData.push
          (
            this.numbersAre[iCounter]
          )
      iCounter += 1;
    } while (iCounter < this.numbersAre.length)
    iCounter = 1;
    let index = 0
  }

  editClick(indexIs: any, valueIs: any) {
    this.editIndex = indexIs;
    this.rowIs = indexIs;
    this.valueIs = valueIs;
    this.buttonType = "Edit";
  }

  deleteClick(indexIs: any, valueIs: any) {
    this.editIndex = indexIs;
    this.rowIs = indexIs;
    this.valueIs = valueIs;
    this.buttonType = "Delete";
  }

  editCancel() {
    this.editIndex = null;
    this.buttonType = "";

  }

  deleteRow(i: number) {
    this.editIndex = null;
    delete this.numbersAre[i];
    this.buttonType = "";

    let iCounter = 0;
    this.editIndex = null;
    this.filterData = [];


    do {
      if (this.numbersAre[iCounter] != undefined)
        this.filterData.push
          (
            this.numbersAre[iCounter]
          )
      iCounter += 1;
    } while (iCounter < this.numbersAre.length)

    iCounter = 1;
    let index = 0
    this.numbersAre = [];
    this.numbersAre = this.filterData;
  }

  editUpdate(i: number, valueIs: number) {
    this.editIndex = null;
    this.numbersAre[i] = valueIs
  }

  filterClear() {
    this.filterData = this.numbersAre;
    this.filterIs = 0;
  }

}




