import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EulerProblemDTO} from '../../../service/model/euler-problem.dto';
import {EulerProblemService} from '../../../service/euler-problem.service';
import {catchError, first, switchMap, takeUntil} from 'rxjs/operators';
import {of} from 'rxjs';
import {NGXLogger} from 'ngx-logger';

@Component({
  selector: 'app-euler-problems',
  styleUrls: ['./euler-problems.component.scss'],
  templateUrl: './euler-problems.component.html'
})
export class EulerProblemsComponent implements OnInit {
  public problems: EulerProblemDTO[];
  page: number;
  pageSize: number;

  constructor(
    private service: EulerProblemService,
    private logger: NGXLogger,
  ) {
    this.page = 1;
    this.pageSize = 50;
  }

  ngOnInit(): void {
    this.service.getList().subscribe(list => this.problems = list.sort((a, b) => this.compare(a, b)));
  }

  remove(problem: EulerProblemDTO) {
    this.service.remove(problem.id)
      .pipe(
        first(removedProblem => {
          this.logger.info('removed problem:', removedProblem);
          return true;
        }),
        switchMap(() => this.service.getList())
      )
      .subscribe(list => this.problems = list.sort((a, b) => this.compare(a, b)),
        err => this.logger.info('failed to remove Problem.', err));
  }

  refreshAll() {
    this.service.refreshAll(true).subscribe(list => this.problems = list.sort((a, b) => this.compare(a, b)),
      err => this.logger.info('Failed to refresh Problems.', err));
  }

  sort() {
    this.problems = this.problems.sort((a, b) => this.compare(a, b));
  }

  compare(a: EulerProblemDTO, b: EulerProblemDTO): number {
    if (a.id > b.id) {
      return 1;
    }
    if (a.id === b.id) {
      return 0;
    }
    return -1;
  }

}
