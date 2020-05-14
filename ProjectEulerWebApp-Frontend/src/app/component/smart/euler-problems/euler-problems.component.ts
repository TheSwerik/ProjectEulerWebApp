import {Component, Inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EulerProblemDTO} from '../../../service/model/eulerProblem.dto';

@Component({
  selector: 'app-euler-problems',
  templateUrl: './euler-problems.component.html'
})
export class EulerProblemsComponent {
  public problems: EulerProblemDTO[];
  private readonly EulerProblemURL = this.baseUrl + 'api/EulerProblem';
  private readonly GetListURL = this.EulerProblemURL + '/get-list';
  private readonly ChangeURL = this.EulerProblemURL + '/change';
  private readonly RemoveURL = this.EulerProblemURL + '/remove';

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {
    this.fetch();
  }

  create() {
    const problem: EulerProblemDTO = {
      id: this.problems[this.problems.length - 1].id + 1,
      title: 'Test2',
      description: 'Test Description2',
      // isSolved: true,
      // solveDate: new Date('December 18, 1995 03:24:00'),
      // solution: '43'
      isSolved: false,
      solveDate: null,
      solution: null
    };

    this.http.post<EulerProblemDTO[]>(this.ChangeURL, problem).subscribe(result => {
      console.log(result);
      this.fetch();
    }, error => console.error(error));
  }

  fetch() {
    this.http.get<EulerProblemDTO[]>(this.GetListURL).subscribe(result => {
      this.problems = result;
    }, error => console.error(error));
  }

  remove(problem: EulerProblemDTO) {
    this.http.post<EulerProblemDTO>(this.RemoveURL, problem).subscribe(result => {
      console.log(result);
      this.fetch();
    }, error => console.error(error));
  }
}
