import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { TestPageComponent } from './test-page/test-page.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgDragDropModule } from 'ng-drag-drop'; //Import NgDragDropModule module
import { DandComponent } from './dand/dand.component';
import { NavmenuComponent } from './navmenu/navmenu.component';
import { DdsortedComponent } from './ddsorted/ddsorted.component';
import { ShareService } from './share.service';
import { DdtoComponent } from './ddto/ddto.component';
import { TododdComponent } from './tododd/tododd.component';
import { HorizontalddComponent } from './horizontaldd/horizontaldd.component';
import { MultipleddComponent } from './multipledd/multipledd.component';
import { TrainerCourseDetailsComponent } from './trainer-course-details/trainer-course-details.component';

const appRoutes: Routes = [
  { path: 'test-page', component: TestPageComponent },
  { path: 'dand', component: DandComponent },
  { path: 'ddsorted', component: DdsortedComponent },
  { path: 'tododd', component: TododdComponent },
  { path: 'horizontaldd', component: HorizontalddComponent },
  { path: 'multipledd', component: MultipleddComponent },
  { path: 'trainercoursedetails', component: TrainerCourseDetailsComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    TestPageComponent,
    DandComponent,
    NavmenuComponent,
    DdsortedComponent,
    DdtoComponent,
    TododdComponent,
    HorizontalddComponent,
    MultipleddComponent,
    TrainerCourseDetailsComponent
  ],
  imports: [
    BrowserModule, FormsModule, HttpClientModule, DragDropModule, RouterModule.forRoot(appRoutes),NgDragDropModule.forRoot()
  ],
  providers: [ShareService],
  bootstrap: [AppComponent]
})
export class AppModule { }
