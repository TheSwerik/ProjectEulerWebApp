import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'date',
})
export class DatePipe implements PipeTransform {
  transform(date: Date): string {
    if (date === null || date === undefined) {
      return 'Unknown';
    }
    return this.day(date.getUTCDay()) + ', ' + this.date(date.getUTCDate()) + ' of ' +
      this.month(date.getUTCMonth()) + ' ' + date.getUTCFullYear() + ', ' +
      this.time(date.getUTCHours()) + ':' + this.time(date.getUTCMinutes());
  }

  date(date: number){
    switch (date % 10) {
      case 1: return date + 'st';
      case 2: return date + 'nd';
      case 3: return date + 'rd';
      default: return date + 'th';
    }
  }

  day(day: number){
    switch (day) {
      case 0: return 'Sunday';
      case 1: return 'Monday';
      case 2: return 'Tuesday';
      case 3: return 'Wednesday';
      case 4: return 'Thursday';
      case 5: return 'Friday';
      case 6: return 'Saturday';
      default: return 'ERROR';
    }
  }

  month(month: number){
    switch (month) {
      case 0: return 'January';
      case 1: return 'February';
      case 2: return 'March';
      case 3: return 'April';
      case 4: return 'May';
      case 5: return 'June';
      case 6: return 'July';
      case 7: return 'August';
      case 8: return 'September';
      case 9: return 'October';
      case 10: return 'November';
      case 11: return 'December';
      default: return 'ERROR';
    }
  }

  time(time: number) {
    return time < 10 ? '0' + time : time;
  }
}
