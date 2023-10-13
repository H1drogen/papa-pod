import { Component, OnInit } from '@angular/core';
import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { ShareService } from '../share.service';


@Component({
  selector: 'app-ddto',
  templateUrl: './ddto.component.html',
  styleUrls: ['./ddto.component.css']
})
export class DdtoComponent implements OnInit {

  listTo= [];
  constructor(private ss: ShareService) { }
  
  MoviesWatched = [
    'Transformers'
  ];

  ngOnInit(): void {
  }


  onDrop(event: CdkDragDrop<string[]>) {
    this.ss.drop(event);
  }
}


