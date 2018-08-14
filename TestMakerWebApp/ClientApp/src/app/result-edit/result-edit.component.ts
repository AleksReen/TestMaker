import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DataResultService } from '../services/data-result.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'result-edit',
    templateUrl: './result-edit.component.html',
    styleUrls: ['./result-edit.component.less']
})

export class ResultEditComponent {

  title: string;
  result: Result;
  form: FormGroup;
  editMode: boolean;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private dataResultService: DataResultService, private formBuilder: FormBuilder) {
    this.result = <Result>{};
    this.createForm();
  }

  ngOnInit(): void {
    this.getData();
  }

  getData(): void {
    let id = +this.activatedRoute.snapshot.params["id"];
    this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

    if (this.editMode) {
      this.dataResultService.getResult(id).subscribe(res => {
        this.result = res;
        this.updateForm();
      }, er => console.error(er));
    }
    else {
      this.result.QuizId = id;
      this.title = "Create a new Result";
    }
  }

  createForm() {
    this.form = this.formBuilder.group({
      Text: ['', Validators.required],
      MinValue: ['', Validators.pattern(/^\d*$/)],
      MaxValue: ['', Validators.pattern(/^\d*$/)]
    });
  }

  updateForm() {
    this.form.setValue({
      Text: this.result.Text,
      MinValue: this.result.MinValue || '',
      MaxValue: this.result.MaxValue || ''
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

  onSubmit(result: Result) {

    let tempResult = <Result>{};
    tempResult.QuizId = this.result.QuizId;
    tempResult.Text = this.form.value.Text;
    tempResult.MinValue = this.form.value.MinValue;
    tempResult.MaxValue = this.form.value.MaxValue;

    if (this.editMode) {
      tempResult.Id = this.result.Id;
      this.dataResultService.putResult(tempResult);
    }
    else {
      this.dataResultService.postResult(tempResult);
    }
  }

  onBack() {
    this.router.navigate(["quiz/edit", this.result.QuizId]);
  }

}
