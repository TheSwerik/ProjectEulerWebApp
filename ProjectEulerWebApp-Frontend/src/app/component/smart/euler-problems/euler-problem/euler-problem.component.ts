import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {EulerProblemDTO} from '../../../../service/model/eulerProblem.dto';

@Component({
  selector: 'app-euler-problem',
  templateUrl: './euler-problem.component.html',
  styleUrls: ['./euler-problem.component.css']
})
export class EulerProblemComponent implements OnInit {

  problem: EulerProblemDTO;

  constructor(private route: ActivatedRoute) {
    this.problem = new EulerProblemDTO();
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.problem.id = +params.get('id');
    });
  }

}
