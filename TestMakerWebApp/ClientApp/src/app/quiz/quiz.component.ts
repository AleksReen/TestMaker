import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DataQuizService } from '../services/data-quiz.service';

@Component({
    selector: 'quiz',
    templateUrl: './quiz.component.html',
    styleUrls: ['./quiz.component.css']
})

export class QuizComponent implements OnInit {

  quiz: Quiz;

  constructor( private dataQuiz: DataQuizService, private activatedRoute: ActivatedRoute, private router: Router) {
    this.quiz = <Quiz>{};
  }

  ngOnInit(): void {
    this.getQuiz();
  }

  getQuiz() {   
    let id = +this.activatedRoute.snapshot.params["id"];
    if (id) {
      this.dataQuiz.getQuizById(id).subscribe(result => {
        this.quiz = result;
      }, error => console.error(error));
    }
    else {
      console.log("Invalid id: routing back to home....");
      this.router.navigate(["home"]);
    }
  }
}
