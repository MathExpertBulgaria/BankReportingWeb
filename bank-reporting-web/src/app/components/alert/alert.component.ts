import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-alert',
  standalone: false,
  templateUrl: './alert.component.html',
  styleUrl: './alert.component.scss'
})
export class AlertComponent {
  @Input() color = 'warn';
  @Input() fontIcon?: string;
  @Input() fontSet?: string;

  constructor() { }

  ngOnInit(): void {
  }

  get getClassName(): any {
    const obj = {
      'alert-primary': this.color === 'primary',
      'alert-accent': this.color === 'accent',
      'alert-warn': this.color === 'warn',
      'alert-success': this.color === 'success',
      'alert-info': this.color === 'info'
    };
    return obj;
  }
}
