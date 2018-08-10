import { Component, OnChanges, Input, Inject, SimpleChanges, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataResultService } from '../services/data-result.service';

@Component({
    selector: 'result-list',
    templateUrl: './result-list.component.html',
    styleUrls: ['./result-list.component.css']
})

export class ResultListComponent implements OnChanges {

  @Input() quiz: Quiz;
  results: Result[];
  title: string;

  constructor(private router: Router, private dataResultService: DataResultService) {
    this.results = [];
  }

  ngOnChanges(changes: SimpleChanges) {
    if (typeof changes['quiz'] !== "undefined") {
      var change = changes['quiz'];
      if (!change.isFirstChange()) {
        this.loadData();
      }
    }
  }

  loadData(): void {
    this.dataResultService.loadData(this.quiz.Id).subscribe(res => {
      this.results = res;
    }, error => console.error(error));
  }

  onCreate(): void {
    this.router.navigate(["/result/create", this.quiz.Id]);
  }

  onEdit(result: Result): void {
    this.router.navigate(["/result/edit", result.Id]);
  }

  onDelete(result: Result): void {
    if (confirm("Do you really want to delete this result?")) {
      this.dataResultService.deleteResult(result.Id).subscribe(res => {
        console.log("Result " + result.Id + " has been deleted.");
        this.loadData();
      }, er => console.error(er));
    }
  }
}
