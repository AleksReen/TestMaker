import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DataQuizService } from '../services/data-quiz.service';

@Component({
    selector: 'quiz-edit',
    templateUrl: './quiz-edit.component.html',
    styleUrls: ['./quiz-edit.component.css']
})

export class QuizEditComponent implements OnInit {

  title: string;
  quiz: Quiz;
  editMode: boolean;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private dataQuiz: DataQuizService) {
    this.quiz = <Quiz>{};  
  }

  ngOnInit(): void {
    this.getMode();
  }

  getMode() {
    let id = +this.activatedRoute.snapshot.params["id"];
    if (id) {
      this.editMode = true;
      this.dataQuiz.getQuizById(id).subscribe(result => {
        this.quiz = result;
        this.title = "Edit - " + this.quiz.Title;
      }, error => console.error(error));
    }
    else {
      this.editMode = false;
      this.title = "Create a new Quiz";
    }
  }

  onSubmit(quiz: Quiz) {

    if (this.editMode) {
      this.dataQuiz.putQuiz(quiz);
    }
    else {    
      this.dataQuiz.postQuiz(quiz);
    }

  }

  onBack() {
    this.router.navigate(["home"]);
  }
}
