import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataQuizService } from '../services/data-quiz.service';
import { Router } from '@angular/router';

@Component({
    selector: 'quiz-list',
    templateUrl: './quiz-list.component.html',
    styleUrls: ['./quiz-list.component.css']
})

export class QuizListComponent implements OnInit {

  @Input() class: string;

  title: string;
  selectedQuiz: Quiz;
  quizzes: Quiz[];

  constructor(private dataQuiz: DataQuizService, private router: Router) {}

  ngOnInit(): void { 
    this.getFunctionality();
  }

  getFunctionality() {
    switch (this.class) {
      case "latest":
      default:
        this.getLatest();
        break;
      case "byTitle":
        this.getByTitle();
        break;
      case "random":
        this.getRandom();
        break;
    }
  }

  getRandom(): any {   
    this.dataQuiz.getRandom().subscribe(result => {
      this.quizzes = result;
      this.title = "Random Quiz";
    }, error => console.error(error));
  }

  getByTitle(): any {
    this.dataQuiz.getByTitle().subscribe(result => {
      this.quizzes = result;
      this.title = "ByTitle Quiz";
    }, error => console.error(error));
  }

  getLatest(): any {   
    this.dataQuiz.getLatest().subscribe(result => {
      this.quizzes = result;
      this.title = "Latest Quiz"; 
    }, error => console.error(error));
  }

  onSelect(quiz: Quiz) {
    this.selectedQuiz = quiz;
    this.router.navigate(["quiz", this.selectedQuiz.Id]);
  }

  //constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private dataQuiz: DataQuizService) {
  //  this.baseUrl = baseUrl + "api/quiz/";
  //}

  //ngOnInit(): void {
  //  this.getFunctionality();
  //}

  //getFunctionality() {
    
  //  switch (this.class) {
  //    case "latest":
  //    default:
  //      this.getLatest();
  //      break;
  //    case "byTitle":
  //      this.getByTitle();
  //      break;
  //    case "random":
  //      this.getRandom();
  //      break;

  //  }
  //}

  //getRandom(): any {
  //  this.title = "Random Quiz";
  //  this.getQuizzesList(this.baseUrl + "Random/");
  //}

  //getByTitle(): any {
  //  this.title = "ByTitle Quiz";
  //  this.getQuizzesList(this.baseUrl + "ByTitle/");
  //}

  //getLatest() {
  //  this.title = "Latest Quiz"; 
  //  this.getQuizzesList(this.baseUrl + "Latest/");
  //}

  //getQuizzesList(url: string) {
  //  this.httpClient.get<Quiz[]>(url).subscribe(result => {
  //    this.quizzes = result;
  //  }, error => console.error(error));
  //}
}
