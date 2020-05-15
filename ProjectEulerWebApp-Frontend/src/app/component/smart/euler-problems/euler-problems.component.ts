import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EulerProblemDTO} from '../../../service/model/euler-problem.dto';
import {EulerProblemService} from '../../../service/euler-problem.service';
import {catchError, first, switchMap, takeUntil} from 'rxjs/operators';
import {of} from 'rxjs';
import {NGXLogger} from 'ngx-logger';

@Component({
  selector: 'app-euler-problems',
  templateUrl: './euler-problems.component.html'
})
export class EulerProblemsComponent implements OnInit {
  public problems: EulerProblemDTO[];

  constructor(
    private service: EulerProblemService,
    private logger: NGXLogger,
  ) {
  }

  ngOnInit(): void {
    this.service.getList().subscribe(list => this.problems = list);
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
      .subscribe(list => this.problems = list, err => this.logger.info('failed to remove Problem.', err));
  }

  refreshAll() {
    this.service.refreshAll(true).subscribe(list => this.problems = list, err => this.logger.info('Failed to refresh Problems.', err));
  }

}
