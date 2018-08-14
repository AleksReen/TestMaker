import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DataQuestionService } from '../services/data-question.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'question-edit',
    templateUrl: './question-edit.component.html',
    styleUrls: ['./question-edit.component.less']
})

export class QuestionEditComponent implements OnInit {

  title: string;
  question: Question;
  form: FormGroup;
  editMode: boolean;
  activityLog: string;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private dataQuestionService: DataQuestionService,
    private formBuilder: FormBuilder)
  {
    this.question = <Question>{};
    this.createForm();
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
        this.updateForm();
      }, er => console.error(er));
    }
    else {
      this.question.QuizId = id;
      this.title = "Create a new Question";
    }
  }

  log(str: string) {
    this.activityLog += "["
      + new Date().toLocaleString()
      + "] " + str + "<br />";
  }

  createForm() {
    this.form = this.formBuilder.group({
      Text: ['', Validators.required]     
    });
    this.activityLog = '';
    this.log("Form has been initialized.");

    this.form.get('Text')!.valueChanges.subscribe(val => {
      if (!this.form.dirty) {
        this.log("Text control has been loaded.");
      }
      else {
        this.log("Text control updated by the user.");
      }
    })

    //this.form.valueChanges.subscribe(val => {
    //  if (!this.form.dirty) {
    //    this.log("Form Model has been loaded.");
    //  }
    //  else {
    //    this.log("Form was updated by the user.");
    //  }
    //})
  }

  updateForm() {
    this.form.setValue({
      Text: this.question.Text || ''
    });
  }

  onSubmit(question: Question) {

    let tempQuestion = <Question>{};
    tempQuestion.Text = this.form.value.Text;
    tempQuestion.QuizId = this.question.QuizId;

    if (this.editMode) {
      this.dataQuestionService.putQuestion(tempQuestion);
    }
    else {
      this.dataQuestionService.postQuestion(tempQuestion);
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
    this.router.navigate(["quiz/edit", this.question.QuizId]);
  }

}
