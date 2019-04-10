import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
	constructor(private router: Router) { }
	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		if (req.headers.get('No-Auth') == "True")
			return next.handle(req.clone());

		if (localStorage.getItem('userToken') != null) {
			const clonedreq = req.clone({
				headers: req.headers.set("Authorization", "Bearer " + localStorage.getItem('userToken'))
			});
			return next.handle(clonedreq).pipe(
				tap(
					succ => { },
					err => {
						if (err.status === 401) {
							this.router.navigateByUrl('/login');
						}
						if (err.status === 403) {
							this.router.navigateByUrl('/forbidden');
						}
						if (err.status === 404) {
							this.router.navigateByUrl('/not-found');
						}

					}
				)
			);
		}
		else
			this.router.navigateByUrl('/login');
	}
}