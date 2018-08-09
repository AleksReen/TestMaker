import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataAnswerService } from '../services/data-answer.service';

@Component({
    selector: 'answer-edit',
    templateUrl: './answer-edit.component.html',
    styleUrls: ['./answer-edit.component.css']
})

export class AnswerEditComponent {

  title: string;
  answer: Answer;
  editMode: boolean;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private dataAnswerService: DataAnswerService) {
    this.answer = <Answer>{};
  }

  ngOnInit(): void {
    this.getData();
  }

  getData(): void {
    let id = +this.activatedRoute.snapshot.params["id"];
    this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

    if (this.editMode) {
      this.dataAnswerService.getAnswer(id).subscribe(res => {
        this.answer = res;
      }, er => console.error(er));
    }
    else {
      this.answer.QuestionId = id;
      this.title = "Create a new Answer";
    }
  }

  onSubmit(answer: Answer) {

    if (this.editMode) {
      this.dataAnswerService.putAnswer(answer);
    }
    else {
      this.dataAnswerService.postAnswer(answer);
    }
  }

  onBack() {
    this.router.navigate(["quiz/edit", this.answer.QuestionId]);
  }
}
