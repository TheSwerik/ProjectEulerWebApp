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

  create() {
    this.service.create(this.dummyProblem())
      .pipe(
        first(problem => {
          this.logger.info('created problem:', problem);
          return true;
        }),
        switchMap(() => this.service.getList())
      )
      .subscribe(list => this.problems = list, err => this.logger.info('failed to create Problem.', err));
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

  dummyProblem(): EulerProblemDTO {
    return {
      id: this.problems[this.problems.length - 1].id + 1,
      title: 'Test2',
      description: 'Test Description2',
      isSolved: false,
      solveDate: null,
      solution: null,
      // isSolved: true,
      // solveDate: new Date('December 18, 1995 03:24:00'),
      // solution: '43',
      publishDate: null,
      difficulty: null
      // publishDate: new Date(),
      // difficulty: 10
    };
  }

}
