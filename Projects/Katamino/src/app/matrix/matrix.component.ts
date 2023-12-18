import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { Matrix } from '../types';
import { MatrixUtils } from './matrixUtils';

@Component({
  selector: 'app-matrix',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './matrix.component.html',
  styleUrl: './matrix.component.scss'
})
export class MatrixComponent implements OnInit{
  
    @Input()
    public matrix: Matrix = [];

    public dimension: number = -1;
    public isAdjacent: boolean = false;
    public isSquare: boolean = false;


    public ngOnInit() {
      this.dimension = this.matrix.length;
      this.isSquare = this.matrix.length === this.matrix[0].length;
      this.isAdjacent = MatrixUtils.isAdjacent(this.matrix);
    }
}
