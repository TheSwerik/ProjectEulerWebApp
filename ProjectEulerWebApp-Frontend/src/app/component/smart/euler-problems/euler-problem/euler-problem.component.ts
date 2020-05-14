import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {EulerProblemDTO} from '../../../../service/model/euler-problem.dto';
import {EulerProblemService} from '../../../../service/euler-problem.service';
import {NGXLogger} from 'ngx-logger';
import {first, map, switchMap} from 'rxjs/operators';

@Component({
  selector: 'app-euler-problem',
  templateUrl: './euler-problem.component.html',
  styleUrls: ['./euler-problem.component.css']
})
export class EulerProblemComponent implements OnInit {

  problem: EulerProblemDTO;

  constructor(
    private route: ActivatedRoute,
    private service: EulerProblemService,
    private logger: NGXLogger,
  ) {
    this.problem = new EulerProblemDTO();
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
  }

}
