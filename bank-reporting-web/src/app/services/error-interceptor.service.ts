import { HttpInterceptorFn } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const ErrorInterceptor: HttpInterceptorFn = (req, next) => {
  console.log('auth interceptor...');
  const router = inject(Router);

  return next(req)
    .pipe(
      catchError((err, caught) => {
        router.navigateByUrl('/error-page');
        return throwError(() => {
          const error: any = new Error(`An error ocurred ${err.message}`);
          return error;
        });
      })
    );
};