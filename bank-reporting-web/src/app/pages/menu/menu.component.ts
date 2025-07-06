import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ScreenService } from '../../services/screen.service';

@Component({
  selector: 'app-menu',
  standalone: false,
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent implements OnInit, OnDestroy {

  public isSmallScreen = false;

  // Subsription
  subIsSmallScreen: Subscription | null = null;

  constructor(private screenSrv: ScreenService) {

  }

  ngOnInit(): void {
      this.subIsSmallScreen = this.screenSrv.isSmallScreen.subscribe(x => {
        this.isSmallScreen = x;
      });
  }

  ngOnDestroy(): void {
      this.subIsSmallScreen?.unsubscribe();
  }
}
