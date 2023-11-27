import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TemperatureService } from './services/temperature.service';
import { catchError, of } from 'rxjs';
import { HttpResponse } from '@angular/common/http';
import { TemperatureType } from './models/temperature.type';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'temperature_convertor';
  temperatureForm: FormGroup;
  seleteTypeLabel: string = "Celsius";

  public temperatureModel: any = {
    temperatureC:0,
    temperatureK:0,
    temperatureF: 0
 };

  public temperatureInputType: TemperatureType = 0;
  public inputDegree: number = 0;

  constructor(private temperatureService: TemperatureService)
  {
      this.temperatureForm = new FormGroup({
        inputType: new FormControl({value: '0' , disabled: false}, [Validators.required]),
        inputDegree: new FormControl({ value: '', disabled: false }, [Validators.required, Validators.minLength(1), Validators.maxLength(8), Validators.pattern(/^-?(0|[1-9]\d*)?$/)])
    
      });
    
  }

  onInputTypeChanged(type: TemperatureType)
  {
    this.temperatureInputType = type;
    this.seleteTypeLabel = TemperatureType[type];
    this.temperatureForm.controls['inputDegree'].setValue(null);
  }
  onInputClick(){
    this.temperatureForm.updateValueAndValidity();
  }

  convertTemperature(): any {

      this.temperatureService.convertTemperature(this.temperatureInputType, this.inputDegree).pipe(
        catchError((error: any) => {
            return of(null);
        })
      )
      .subscribe((response: HttpResponse<any>) => {
        if(response!=null)
        {
          switch(response.status)
          {
            case 200:
              var result = response.body;
              this.temperatureModel.temperatureC = result.temperatureC;
              this.temperatureModel.temperatureF = result.temperatureF;
              this.temperatureModel.temperatureK = result.temperatureK;
              console.log(result);
              break;
            case 400:
            case 405:
            case 500:
              console.log(`convert temperature error: ${response.status}`);
              break;
            default: 
              console.log(result);
          }
        }
        else
        {
          console.log('something wrong with the api, please check.');
        }
        
      });;

  }

  onTemperatureInputChange(){
    if(this.temperatureForm.status.toLocaleLowerCase() == "valid")
    {
      this.convertTemperature();
    }
  }
}
