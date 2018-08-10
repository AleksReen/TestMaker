import { Component, OnChanges, Input, Inject, SimpleChanges, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataQuestionService } from '../services/data-question.service';

@Component({
    selector: 'question-list',
    templateUrl: './question-list.component.html',
    styleUrls: ['./question-list.component.css']
})

export class QuestionListComponent implements OnChanges {

  @Input() quiz: Quiz;
  questions: Question[];
  title: string;

  constructor(private router: Router, private dataQuestionService: DataQuestionService) {
    this.questions = [];
  }

  ngOnChanges(changes: SimpleChanges) {
    if (typeof changes['quiz'] !== "undefined") {     
      var change = changes['quiz'];    
      if (!change.isFirstChange()) {
        this.loadData(this.quiz.Id);
      }
    }
  }  

  loadData(quizId: number): void {
    this.dataQuestionService.loadData(quizId).subscribe(res => {
      this.questions = res;
    }, error => console.error(error));
  }

  onCreate(): void {
    this.router.navigate(["/question/create", this.quiz.Id]);
  }

  onEdit(question: Question): void {
    this.router.navigate(["/question/edit", question.Id]);
  }

  onDelete(question: Question): void {
    if (confirm("Do you really want to delete this question?")) {
      this.dataQuestionService.deleteQuestion(question.Id).subscribe(res => {
        console.log("Question " + question.Id + " has been deleted.");
        this.loadData(this.quiz.Id);
      }, er => console.error(er));
    }
  }
}
