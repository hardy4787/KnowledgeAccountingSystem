import { Directive, Output, EventEmitter } from '@angular/core';

@Directive({ 
	selector: '[ngModel][checkspaces]',
	host: {
	"(blur)": 'onInputChange($event)'
			}
	})
	export class CheckSpacesDirective{
	@Output() ngModelChange:EventEmitter<any> = new EventEmitter()
	value: any
	
	onInputChange($event){
		this.value = $event.target.value.replace(/\s+/g, ' ').trim();
			this.ngModelChange.emit(this.value)
			}
	}