import {Component, Inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-euler-problem',
  templateUrl: './euler-problem.component.html'
})
export class EulerProblemComponent {
  public problems: EulerProblem[];
  private readonly EulerProblemURL = this.baseUrl + 'api/EulerProblem';

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {
    this.fetch();
  }

  create() {
    const problem: EulerProblem = {
      id: 4,
      title: 'Test2',
      description: 'Test Description2',
      // isSolved: true,
      // solveDate: new Date('December 18, 1995 03:24:00'),
      // solution: '43'
      isSolved: false,
      solveDate: null,
      solution: null
    };

    this.http.post<EulerProblem>(this.EulerProblemURL, problem).subscribe(result => {
      console.log(result);
      this.fetch();
    }, error => console.error(error));
  }

  fetch() {
    this.http.get<EulerProblem[]>(this.EulerProblemURL).subscribe(result => {
      this.problems = result;
    }, error => console.error(error));
  }
}

interface EulerProblem {
  id: number;
  title: string;
  description: string;
  isSolved: boolean;
  solveDate: Date;
  solution: string;
}