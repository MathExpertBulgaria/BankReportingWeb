import { Component, Input } from '@angular/core';
import { OperationAlert } from '../../models/core/data-operation-result-model';

@Component({
  selector: 'app-operation-result',
  standalone: false,
  templateUrl: './operation-result.component.html',
  styleUrl: './operation-result.component.scss'
})
export class OperationResultComponent {
  @Input() dor: OperationAlert[] = [];

  constructor() { }
}
