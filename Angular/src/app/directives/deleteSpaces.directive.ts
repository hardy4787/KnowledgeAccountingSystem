import { Directive, Output, EventEmitter } from '@angular/core';

@Directive({ 
	selector: '[ngModel][deletespaces]',
	host: {
	"(blur)": 'onInputChange($event)'
			}
	})
	export class DeleteSpacesDirective{
	@Output() ngModelChange:EventEmitter<any> = new EventEmitter()
	value: any
	
	onInputChange($event){
			this.value = $event.target.value.replace(/\s+/g, '');
			this.ngModelChange.emit(this.value)
			}
	}