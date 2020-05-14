import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'difficulty',
})
export class DifficultyPipe implements PipeTransform {
  transform(difficulty: number): string {
    if (difficulty === null || difficulty === undefined || difficulty < 5 || difficulty > 100) { return 'Unknown'; }
    return difficulty + '%';
  }
}
