import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { Matrix } from './types';
import { MatrixUtils } from './matrix/matrixUtils';
import { MatrixComponent } from './matrix/matrix.component';
import { KataminoFactory } from './katamino/katamino-factory';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MatrixComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  
  public kataminos: Matrix[] = [];

  ngOnInit(): void {
    var factory = new KataminoFactory();
    this.kataminos = factory.initKataminos(5);
  }

  title = 'Katamino';
}
