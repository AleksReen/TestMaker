import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataAnswerService } from '../services/data-answer.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'answer-edit',
    templateUrl: './answer-edit.component.html',
    styleUrls: ['./answer-edit.component.less']
})

export class AnswerEditComponent {

  title: string;
  answer: Answer;
  form: FormGroup;
  editMode: boolean;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private dataAnswerService: DataAnswerService,
    private formBuilder: FormBuilder)
  {
    this.answer = <Answer>{};
    this.createForm();
  }

  ngOnInit(): void {
    this.getData();
  }

  createForm() {
    this.form = this.formBuilder.group({
      Text: ['', Validators.required],
      Value: ['',
        [Validators.required, Validators.min(-5), Validators.max(5)]
      ]
    });
  }

  updateForm() {
    this.form.setValue({
      Text: this.answer.Text || '',
      Value: this.answer.Value || 0
    });
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

  getData(): void {
    let id = +this.activatedRoute.snapshot.params["id"];
    this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

    if (this.editMode) {
      this.dataAnswerService.getAnswer(id).subscribe(res => {
        this.answer = res;
        this.updateForm();
      }, er => console.error(er));
    }
    else {
      this.answer.QuestionId = id;
      this.title = "Create a new Answer";
    }
  }

  onSubmit(answer: Answer) {

    let tempAnswer = <Answer>{};
    tempAnswer.QuestionId = this.answer.QuestionId;   
    tempAnswer.Text = this.form.value.Text;
    tempAnswer.Value = this.form.value.Value;

    if (this.editMode) {
      tempAnswer.Id = this.answer.Id;
      this.dataAnswerService.putAnswer(tempAnswer);
    }
    else {
      this.dataAnswerService.postAnswer(tempAnswer);
    }
  }

  onBack() {
    this.router.navigate(["question/edit", this.answer.QuestionId]);
  }
}
