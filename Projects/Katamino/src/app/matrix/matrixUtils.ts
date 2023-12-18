import { Matrix, Coordinates } from "../types";

export class MatrixUtils {

    public static areCongruent(a: Matrix, b: Matrix): boolean {
        if(this.areEquals(a, b)) return true;

        let rotatedB = this.rotate(b);
        if(this.areEquals(a, rotatedB)) return true;

        rotatedB = this.rotate(b, 2);
        if(this.areEquals(a, rotatedB)) return true;

        rotatedB = this.rotate(b, 1, false);
        if(this.areEquals(a, rotatedB)) return true;

        return false;
    }

    public static mirrorX(matrix: Matrix): Matrix {
        var mirrored = this.clone(matrix);

        for(let i = 0; i < matrix.length; ++i) {
            for(let j = 0; j < matrix[i].length; ++j) {
                mirrored[matrix.length-1-i][j] = matrix[i][j];
            }
        }
        return mirrored;
    }

    public static mirrorY(matrix: Matrix): Matrix {
        var mirrored = this.clone(matrix);

        for(let i = 0; i < matrix.length; ++i) {
            for(let j = 0; j < matrix[i].length; ++j) {
                mirrored[i][matrix.length-1-j] = matrix[i][j];
            }
        }
        return mirrored;
    }

    public static areEquals(a: Matrix, b: Matrix): boolean {
        if(a.length !== b.length) return false;
        const n = a.length;
        for (let i = 0; i < n; i++) {
            if(a[i].length !== b[i].length) return false;

            for (let j = i; j < n; j++) {
                if(a[i][j] !== b[i][j]) return false;
            }
        }

        return true;
    }

    public static rotate(matrix: Matrix, times = 1, clockwise: boolean = true): Matrix {
        const n = matrix.length;        
        var rotated: Matrix = MatrixUtils.clone(matrix);
        
        let rotation = 0;
        while(rotation < times) {
            if(clockwise) {
                MatrixUtils.rotateClockwise(rotated);
            } else {
                MatrixUtils.rotateCounterClockwise(rotated);
            }

            ++rotation;
        }
    
        return rotated;
    }

    public static rotateClockwise(matrix: Matrix) {
        const n = matrix.length; 
        for (let i = 0; i < matrix.length; i++) {
            for (let j = i; j < matrix[i].length; j++) {
                [matrix[i][j], matrix[j][i]] = [matrix[j][i], matrix[i][j]];
            }
        }
        
        // Reverse each row
        for (let i = 0; i < n; i++) {
            matrix[i].reverse();
        }
    }

    public static rotateCounterClockwise(matrix: Matrix) {
        const n = matrix.length; 
        for (let i = 0; i < n; i++) {            
            for (let j = i; j < n; j++) {
                [matrix[i][j], matrix[j][i]] = [matrix[j][i], matrix[i][j]];
            }
        }
    }

    public static GenerateFromCoordinates(coords: Coordinates[], dimension: number): Matrix {
        const newMatrix = MatrixUtils.generate(dimension);
            
        for (const coordinate of coords) {
            newMatrix[coordinate.x][coordinate.y] = 1;
        }

        return newMatrix;
    }

    public static generate(dimension: number) {
        let matrix: Matrix = [];

        for(let i = 0; i < dimension; i++) {
            matrix.push(new Array(dimension).fill(0))
        }

        return matrix;
    }

    public static generateRectangular(x: number, y: number) {
        let matrix: Matrix = [];

        for(let i = 0; i < x; i++) {
            matrix.push(new Array(y).fill(0))
        }

        return matrix;
    }

    public static clone(matrix: Matrix): Matrix {
        const n = matrix.length;
        var clone: Matrix = [];

        for (let i = 0; i < matrix.length; i++) {
            clone[i] = [];
            for (let j = 0; j < matrix[i].length; j++) {
                clone[i][j] = matrix[i][j];
            }
        }

        return clone;
    }

    public static isAdjacent(matrix: Matrix): boolean {

        let indexes = MatrixUtils.getIndexes(matrix);
        const numberOfElements = indexes.length;

        let currentElem = indexes[0];
        indexes.splice(0, 1);

        let visitedElements = MatrixUtils.isAdjacentElements(matrix, indexes, currentElem);
        ++visitedElements;
        return visitedElements === numberOfElements;
    }

    private static isAdjacentElements(matrix: Matrix, indexes: Coordinates[], currentElem: Coordinates): number {
        let numberOfVisited = 0;

        // left
        let possibleElement = {x: currentElem.x + 1, y: currentElem.y};
        let possibleElementIndex = indexes.findIndex(p => p.x === possibleElement.x 
            && p.y === possibleElement.y);

        if(possibleElementIndex !== -1) {
            const item = indexes[possibleElementIndex];
            indexes.splice(possibleElementIndex, 1);
            ++numberOfVisited;
            numberOfVisited += MatrixUtils.isAdjacentElements(matrix, indexes, item);
        }

        // right
        possibleElement = {x: currentElem.x - 1, y: currentElem.y};
        possibleElementIndex = indexes.findIndex(p => p.x === possibleElement.x 
            && p.y === possibleElement.y);

        if(possibleElementIndex !== -1) {
            const item = indexes[possibleElementIndex];
            indexes.splice(possibleElementIndex, 1);
            ++numberOfVisited;
            numberOfVisited += MatrixUtils.isAdjacentElements(matrix, indexes, item);
        }

        // below
        possibleElement = {x: currentElem.x, y: currentElem.y + 1};
        possibleElementIndex = indexes.findIndex(p => p.x === possibleElement.x 
            && p.y === possibleElement.y);

        if(possibleElementIndex !== -1) {
            const item = indexes[possibleElementIndex];
            indexes.splice(possibleElementIndex, 1);
            ++numberOfVisited;
            numberOfVisited += MatrixUtils.isAdjacentElements(matrix, indexes, item);
        }

        // up
        possibleElement = {x: currentElem.x, y: currentElem.y - 1};
        possibleElementIndex = indexes.findIndex(p => p.x === possibleElement.x 
            && p.y === possibleElement.y);

        if(possibleElementIndex !== -1) {
            const item = indexes[possibleElementIndex];
            indexes.splice(possibleElementIndex, 1);
            ++numberOfVisited;
            numberOfVisited += MatrixUtils.isAdjacentElements(matrix, indexes, item);
        }

        return numberOfVisited;
    }

    public static isSquare(matrix: number[][]): boolean {
        const x = matrix.length;

        for(let column of matrix) {
            if(column.length !== x) return false;
        }

        return true;
    }

    public static getIndexes(matrix: Matrix): Coordinates[] {
        const result: Coordinates[] = [];

        for(let i = 0; i < matrix.length; ++i) {
            for(let j = 0; j < matrix[i].length; ++j) {
                if(matrix[i][j] === 0) continue;

                result.push({x: i, y: j});
            }
        }

        return result;
    }
}
