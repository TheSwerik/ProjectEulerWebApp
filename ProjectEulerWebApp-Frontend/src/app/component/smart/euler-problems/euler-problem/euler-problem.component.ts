import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {EulerProblemDTO} from '../../../../service/model/euler-problem.dto';
import {EulerProblemService} from '../../../../service/euler-problem.service';
import {NGXLogger} from 'ngx-logger';
import {first, map, switchMap} from 'rxjs/operators';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-euler-problem',
  templateUrl: './euler-problem.component.html',
  styleUrls: ['./euler-problem.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class EulerProblemComponent implements OnInit {

  problem: EulerProblemDTO;
  debug: boolean;

  constructor(
    private route: ActivatedRoute,
    private service: EulerProblemService,
    private logger: NGXLogger,
  ) {
    this.problem = new EulerProblemDTO();
    this.problem.description = '';
    this.debug = true;
  }

  ngOnInit() {
    this.route.paramMap
      .pipe(
        first(),
        switchMap(params => this.service.get(+params.get('id')))
      )
      .subscribe(problem => this.logger.info(this.problem = problem));
  }

  refresh() {
    this.service.refresh(this.problem.id).subscribe(
      problem => this.logger.info('Refreshed', this.problem = problem),
      err => this.logger.info('ERROR', err));
  }

  solve() {
    this.service.solve(this.problem).subscribe(
      times => Object.keys(times).forEach((lang) => this.logger.info(lang, ': \t', times[lang])),
      err => this.handleError(err));
  }

  handleError(error: Response) {
    // TODO
    if (error.status === 400) {
      console.log('test');
    } else {
      console.log('test2');
    }
  }
}
