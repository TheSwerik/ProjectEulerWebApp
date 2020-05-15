import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {EulerProblemDTO} from './model/euler-problem.dto';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EulerProblemService {
  private readonly EulerProblemURL = this.baseUrl + 'api/EulerProblem/';
  private readonly CreateURL = this.EulerProblemURL + 'create/';
  private readonly GetURL = this.EulerProblemURL + 'get/';
  private readonly RemoveURL = this.EulerProblemURL + 'remove/';
  private readonly HTMLURL = this.EulerProblemURL + 'html/';

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {
  }

  create(problem: EulerProblemDTO): Observable<EulerProblemDTO> {
    return this.http.post<EulerProblemDTO>(this.CreateURL, problem);
  }

  getList(): Observable<EulerProblemDTO[]> {
    return this.http.get<EulerProblemDTO[]>(this.GetURL);
  }

  get(id: number): Observable<EulerProblemDTO> {
    return this.http.get<EulerProblemDTO>(this.GetURL + id);
  }

  remove(id: number): Observable<EulerProblemDTO> {
    return this.http.delete<EulerProblemDTO>(this.RemoveURL + id);
  }

  html(id: number): Observable<string> {
    return this.http.post<string>(this.HTMLURL, '"' + id + '"', {responseType: 'text' as 'json'});
  }
}
