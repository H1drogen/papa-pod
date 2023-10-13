import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { ShareService } from '../share.service';

@Component({
  selector: 'app-ddsorted',
  templateUrl: './ddsorted.component.html',
  styleUrls: ['./ddsorted.component.css']
})
export class DdsortedComponent implements OnInit {

  constructor(private ss: ShareService) { }

  ngOnInit(): void {
  }

  MoviesList = [
    'The Far Side of the World',
    'Morituri',
    'Napoleon Dynamite',
    'Pulp Fiction',
    'Blade Runner',
    'Cool Hand Luke',
    'Heat',
    'Juice'
  ];


  onDrop(event: CdkDragDrop<string[]>) {
    this.ss.drop(event);
  }

}
