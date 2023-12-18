import { MatrixUtils } from "../matrix/matrixUtils";
import { Coordinates, Katamino, Matrix } from "../types";

export class KataminoUtils {

    public static createFromMatrix(matrix: Matrix): Katamino {
        const result: Coordinates[] = [];

        for(let i = 0; i < matrix.length; ++i) {
            for(let j = 0; j < matrix[i].length; ++j) {
                if(matrix[i][j] === 0) continue;

                result.push({x: i, y: j});
            }
        }

        return result;
    }

    public static convertToMatrix(katamino: Katamino): Matrix {
        const dimensions = this.getDimensions(katamino);
        const matrix: Matrix = MatrixUtils.generateRectangular(dimensions.x, dimensions.x);

        for(let item of katamino) {
            matrix[item.x][item.y] = 1;
        }

        return matrix;
    }

    public static rotate(katamino: Katamino): Katamino {
        let clone = this.clone(katamino);
        clone = this.normalizeKatamino(clone);
        let dimensions = this.getDimensions(clone);

        for(let coordinate of clone) {
            let newX = dimensions.x - coordinate.x;
            coordinate.x = newX;
        }

        return clone;
    }

    public static normalizeKatamino(katamino: Katamino): Katamino {
        let minX = 9999;
        let minY = 9999;

        for(let coordinate of katamino) {
            if( coordinate.x < minX ) minX = coordinate.x;

            if( coordinate.y < minY ) minY = coordinate.y;
        }

        if(minX > 0) katamino = this.shiftLeft(katamino, minX);
        if(minY > 0) katamino = this.shiftUp(katamino, minY);

        return katamino;
    }

    private static shiftLeft(coordinates: Katamino, shift: number): Katamino {        
        return coordinates.map(p => ({x: p.x - shift, y: p.y}));
    }

    private static shiftUp(coordinates: Katamino, shift: number): Katamino {
        return coordinates.map(p => ({x: p.x, y: p.y - shift}));
    }

    private static getDimensions(katamino: Katamino): Coordinates {
        var normalized = this.normalizeKatamino(katamino)
        let x = -1;
        let y = -1;

        for(let coordinate of normalized) {
            if( coordinate.x > x ) x = coordinate.x;

            if( coordinate.y > y ) y = coordinate.y;
        }

        return {x, y};
    }

    private static clone(katamino: Katamino): Katamino {
        return katamino.map(p => ({...p}));
    }
}