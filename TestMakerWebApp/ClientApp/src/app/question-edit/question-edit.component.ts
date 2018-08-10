import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DataQuestionService } from '../services/data-question.service';

@Component({
    selector: 'question-edit',
    templateUrl: './question-edit.component.html',
    styleUrls: ['./question-edit.component.css']
})

export class QuestionEditComponent implements OnInit {

  title: string;
  question: Question;
  editMode: boolean;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private dataQuestionService: DataQuestionService) {
    this.question = <Question>{};
  }

  ngOnInit(): void {
    this.getData();
  }

  getData(): void {
    let id = +this.activatedRoute.snapshot.params["id"];
    this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

    if (this.editMode) {
      this.dataQuestionService.getQuestion(id).subscribe(res => {
        this.question = res;
      }, er => console.error(er));
    }
    else {
      this.question.QuizId = id;
      this.title = "Create a new Question";
    }
  }

  onSubmit(question: Question) {

    if (this.editMode) {
      this.dataQuestionService.putQuestion(question);
    }
    else {
      this.dataQuestionService.postQuestion(question);
    }
  }

  onBack() {
    this.router.navigate(["quiz/edit", this.question.QuizId]);
  }

}
