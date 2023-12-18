
import { MatrixUtils } from "../matrix/matrixUtils";
import { Coordinates, Matrix } from "../types";

export class KataminoFactory {
    private kataminos: Matrix[] = [];
    private dimension: number = -1;

    public initKataminos(dimension: number): Matrix[] {
        this.dimension = dimension;
        this.initBaseKatamino(dimension);
        this.generateKataminos([], 0);        
        return this.kataminos;
    }

    private initBaseKatamino(dimension: number) {
        const baseKatamino: Matrix = [];
        baseKatamino.push(new Array(dimension).fill(1));
        for(let i = 0; i < dimension-1; ++i) {
            baseKatamino.push(new Array(dimension).fill(0))
        }

        this.kataminos.push(baseKatamino);
    }

    private isUnique(newMatrix: Matrix) {
        for (const existingMatrix of this.kataminos) {            
            if (MatrixUtils.areCongruent(newMatrix, existingMatrix)) return false;
        }

        return true;
    };

    private generateKataminos(positions: Coordinates[], index: number): void {
        if (positions.length === this.dimension) {
            const newMatrix = MatrixUtils.generate(this.dimension);
            
            for (const coordinate of positions) {
                newMatrix[coordinate.x][coordinate.y] = 1;
            }

            if (MatrixUtils.isAdjacent(newMatrix) && this.isUnique(newMatrix)) {
                this.kataminos.push(newMatrix);
            }
            
            return;
        }

        if (index >= Math.pow(this.dimension, 2)) {
            return;
        }

        const newX = Math.floor(index / this.dimension);
        const newY = index % this.dimension;

        const newPositions = positions.slice();
        newPositions.push({x: newX, y: newY});

        this.generateKataminos(newPositions, index + 1);
        this.generateKataminos(positions, index + 1);
    };
}
