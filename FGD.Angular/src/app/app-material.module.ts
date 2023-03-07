import {NgModule} from '@angular/core';

import {MatInputModule} from '@angular/material/input';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule}  from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatListModule} from '@angular/material/list';
import {MatProgressBarModule} from '@angular/material/progress-bar'; 
import {MatMenuModule} from '@angular/material/menu'; 
import {MatDividerModule} from '@angular/material/divider';
import {MatDialogModule} from '@angular/material/dialog'
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatBottomSheetModule} from '@angular/material/bottom-sheet';
import {MatBadgeModule} from '@angular/material/badge';
import {MatCardModule} from '@angular/material/card';
import {MatRippleModule} from '@angular/material/core';


@NgModule({ 
        imports: [
            MatInputModule,
            FormsModule,
            ReactiveFormsModule,
            MatFormFieldModule,
            MatIconModule,
            MatButtonModule,
            MatListModule,
            MatProgressBarModule,
            MatMenuModule,
            MatDividerModule,
            MatDialogModule,
            MatProgressSpinnerModule,
            MatBottomSheetModule,
            MatBadgeModule,
            MatCardModule,
            MatRippleModule 
        ],
        exports: [
            MatInputModule,
            FormsModule,
            ReactiveFormsModule,
            MatFormFieldModule,
            MatIconModule,
            MatButtonModule,
            MatListModule,
            MatProgressBarModule,
            MatMenuModule,
            MatDividerModule,
            MatDialogModule,
            MatProgressSpinnerModule,
            MatBottomSheetModule,
            MatBadgeModule,
            MatCardModule,
            MatRippleModule
        ]
      })
export class MaterialModule{

}