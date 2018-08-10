import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { DataAnswerService } from '../services/data-answer.service';
import { Router } from '@angular/router';

@Component({
    selector: 'answer-list',
    templateUrl: './answer-list.component.html',
    styleUrls: ['./answer-list.component.css']
})

export class AnswerListComponent implements OnChanges {

  @Input() question: Question;
  answers: Answer[];
  title: string;

  constructor(private dataAnswerService: DataAnswerService, private router: Router) {
    this.answers = [];
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (typeof changes['question'] !== "undefined") {
      var change = changes['question'];
      if (!change.isFirstChange()) {
        this.loadData();
      }
    }
  }

  loadData(): void {
    this.dataAnswerService.loadData(this.question.Id).subscribe(res => {
      this.answers = res;
    }, er => console.error(er));
  }

  onCreate() {
    this.router.navigate(["/answer/create", this.question.Id]);
  }

  onEdit(answer: Answer) {
    this.router.navigate(["/answer/edit", answer.Id]);
  }

  onDelete(answer: Answer) {
    if (confirm("Do you really want to delete this answer?")) {
      this.dataAnswerService.deleteAnswer(answer.Id).subscribe(res => {
        console.log("Answer " + answer.Id + " has been deleted.");
        this.loadData();
      }, er => console.error(er));
    }
  }
}
