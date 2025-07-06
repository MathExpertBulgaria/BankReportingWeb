import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, fromEvent, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ScreenService implements OnDestroy {

  smallMax = 640;
  largeMin = 1008;

  // Subscription
  subResize: Subscription | null = null;

    // Set initially
  public isSmallScreen = new BehaviorSubject<boolean>(
      window.innerWidth > window.innerHeight ? window.innerHeight < this.smallMax : window.innerWidth < this.smallMax
    );

  constructor() {
    // Resize
    this.subResize = fromEvent(window, 'resize')
      .subscribe((evt: any) => {

        this.isSmallScreen.next(
          window.innerWidth > window.innerHeight ? window.innerHeight < this.smallMax : window.innerWidth < this.smallMax
        );
    })
   }

   ngOnDestroy(): void {
       this.subResize?.unsubscribe();
   }
}
