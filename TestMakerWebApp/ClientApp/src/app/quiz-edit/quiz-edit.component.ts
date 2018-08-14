import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DataQuizService } from '../services/data-quiz.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'quiz-edit',
    templateUrl: './quiz-edit.component.html',
    styleUrls: ['./quiz-edit.component.less']
})

export class QuizEditComponent implements OnInit {

  title: string;
  quiz: Quiz;
  form: FormGroup;
  editMode: boolean;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private dataQuiz: DataQuizService, private formBuilder: FormBuilder) {
    this.quiz = <Quiz>{};
    this.createForm()
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
        this.updateForm();
      }, error => console.error(error));
    }
    else {
      this.editMode = false;
      this.title = "Create a new Quiz";
    }
  }

  createForm(): any {
    this.form = this.formBuilder.group({
      Title: ['', Validators.required],
      Description: '',
      Text: ''
    });
  }

  updateForm(): any {
    this.form.setValue({
      Title: this.quiz.Title,
      Description: this.quiz.Description || '',
      Text: this.quiz.Text || ''
    });
  }

  onSubmit(quiz: Quiz) {

    let tempQuiz = <Quiz>{};
    tempQuiz.Title = this.form.value.Title;
    tempQuiz.Description = this.form.value.Description;
    tempQuiz.Text = this.form.value.Text;

    if (this.editMode) {
      tempQuiz.Id = this.quiz.Id;
      this.dataQuiz.putQuiz(tempQuiz);
    }
    else {
      this.dataQuiz.postQuiz(tempQuiz);
    }

  }

  getFormControl(name: string) {
    return this.form.get(name);
  }

  isValid(name: string) {
    let e = this.getFormControl(name);
    return e && e.valid;
  }

  isChanged(name: string) {
    let e = this.getFormControl(name);
    return e && (e.dirty || e.touched);
  }

  hasError(name: string) {
    let e = this.getFormControl(name);
    return e && (e.dirty || e.touched) && !e.valid;
  }

  onBack() {
    this.router.navigate(["home"]);
  }
}
