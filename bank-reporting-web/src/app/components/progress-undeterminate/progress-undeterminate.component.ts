import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-progress-undeterminate',
  standalone: false,
  templateUrl: './progress-undeterminate.component.html',
  styleUrl: './progress-undeterminate.component.scss'
})
export class ProgressUndeterminateComponent {
  @Input() set showSpinner(value: boolean) {
    this._showSpinner = value;
    if (this._showSpinner) {
      setTimeout(() => {
        if (this.showSpinner) {
          this._doShowSpinner = true;
        }
      }, 500);
    } else {
      this._doShowSpinner = false;
    }
  }
  get showSpinner(): boolean {
    return this._showSpinner;
  }
  private _showSpinner = false;
  _doShowSpinner = false;

  constructor() { }
}
