import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DataResultService } from '../services/data-result.service';

@Component({
    selector: 'result-edit',
    templateUrl: './result-edit.component.html',
    styleUrls: ['./result-edit.component.less']
})

export class ResultEditComponent {

  title: string;
  result: Result;
  editMode: boolean;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private dataResultService: DataResultService) {
    this.result = <Result>{};
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
      }, er => console.error(er));
    }
    else {
      this.result.QuizId = id;
      this.title = "Create a new Result";
    }
  }

  onSubmit(result: Result) {

    if (this.editMode) {
      this.dataResultService.putResult(result);
    }
    else {
      this.dataResultService.postResult(result);
    }
  }

  onBack() {
    this.router.navigate(["quiz/edit", this.result.QuizId]);
  }

}
