import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {EulerProblemDTO} from '../../../../service/model/euler-problem.dto';
import {EulerProblemService} from '../../../../service/euler-problem.service';
import {NGXLogger} from 'ngx-logger';
import {first, map, switchMap} from 'rxjs/operators';

@Component({
  selector: 'app-euler-problem',
  templateUrl: './euler-problem.component.html',
  styleUrls: ['./euler-problem.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class EulerProblemComponent implements OnInit {

  problem: EulerProblemDTO;
  html: string;

  constructor(
    private route: ActivatedRoute,
    private service: EulerProblemService,
    private logger: NGXLogger,
  ) {
    this.problem = new EulerProblemDTO();
    this.html = '';
  }

  ngOnInit() {
    this.route.paramMap
      .pipe(
        first(),
        switchMap(params => this.service.get(+params.get('id')))
      )
      .subscribe(problem => {
        this.logger.info(this.problem = problem);
        this.problem.publishDate = new Date();
        this.problem.publishDate.setUTCHours(15);
      });
    this.service.html().subscribe(result => this.html = result.fixed(),
      err => this.logger.info('ERROR', err));
  }

}
