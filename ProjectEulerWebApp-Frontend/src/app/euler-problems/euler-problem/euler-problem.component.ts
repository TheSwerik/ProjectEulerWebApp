import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-euler-problem',
  templateUrl: './euler-problem.component.html',
  styleUrls: ['./euler-problem.component.css']
})
export class EulerProblemComponent implements OnInit {

  id = 'NULL';

  constructor(private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => this.id = params.get('id'));
  }

}
