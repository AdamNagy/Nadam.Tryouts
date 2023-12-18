import { MatrixUtils } from "./matrixUtils";
import { Matrix } from "../types";

describe('Matrix Utils', () => {
  describe('isSquare', () => {
    describe('when passing a square matrix', () => {
        const payload = [
            [1,2,3],
            [1,2,3],
            [1,2,3]
        ];

        it('should return true', () => {
            const result = MatrixUtils.isSquare(payload);
            expect(result).toBeTruthy();
        });
    });

    describe('when passing a rectange matrix', () => {
        const payload = [
            [1,2,3],
            [1,2,3],
            [1,2,3],
            [1,2,3]
        ];

        it('should return false', () => {
            const result = MatrixUtils.isSquare(payload);
            expect(result).toBeFalsy();
        });
    });


  });

  describe('getIndexes', () => {
    describe('when passing an matrix which has 1 elements at index (0,0)', () => {
        const payload = [
            [1, 0, 0],
            [0, 0, 0],
            [0, 0, 0],
        ];

        const result = MatrixUtils.getIndexes(payload);
        it('should return an index array with 1 elem', () => {
            expect(result.length).toBe(1);
        });

        it('should be (0,0)', () => {
            expect(result[0].x).toBe(0);
            expect(result[0].y).toBe(0);
        })
    });

    describe('when passing an matrix which has 1 elements at index (1,1)', () => {
        const payload = [
            [0, 0, 0],
            [0, 1, 0],
            [0, 0, 0],
        ];

        const result = MatrixUtils.getIndexes(payload);
        it('should return an index array with 1 elem', () => {
            expect(result.length).toBe(1);
        });

        it('should be (0,0)', () => {
            expect(result[0].x).toBe(1);
            expect(result[0].y).toBe(1);
        })
    });

    describe('when passing an matrix which has 2 elements at index (0,1), (1,1)', () => {
        const payload = [
            [0, 1, 0],
            [0, 1, 0],
            [0, 0, 0],
        ];

        const result = MatrixUtils.getIndexes(payload);
        it('should return an index array with 2 elem', () => {
            expect(result.length).toBe(2);
        });

        it('should be (0,1) and (1,1)', () => {
            expect(result[0].x).toBe(0);
            expect(result[0].y).toBe(1);

            expect(result[1].x).toBe(1);
            expect(result[1].y).toBe(1);
        })
    });

    describe('when passing a matrix with its first column containing "1"-s', () => {
        const payload = [
            [1, 0, 0],
            [1, 0, 0],
            [2, 0, 0],
            [3, 0, 0],
            [2, 0, 0],
        ];
        const result = MatrixUtils.getIndexes(payload);
        it('should return an index array with 5 elem', () => {
            expect(result.length).toBe(5);
        });

        it('should contain elements in order', () => {
            let i = 0;
            for(let index of result) {
                expect(index.y).toBe(0);
                expect(index.x).toBe(i);
                ++i;
            }
        })
    })
  });

  describe('areAdjacent', () => {
    describe('when passing an adjacent square matrix', () => {
        const payload = [
            [0, 1, 0, 0, 0],
            [0, 1, 1, 0, 0],
            [0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0]
        ];

        it('should return true', () => {
            const result = MatrixUtils.isAdjacent(payload);
            expect(result).toBeTruthy();
        })
    });

    describe('when passing an adjacent rectange matrix', () => {
        const payload = [
            [1, 0, 0, 0, 0],
            [1, 0, 0, 0, 0],
            [1, 0, 0, 0, 0],
            [1, 0, 0, 0, 0],
            [1, 0, 0, 0, 0],
            [0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0]
        ];

        it('should return true', () => {
            const result = MatrixUtils.isAdjacent(payload);
            expect(result).toBeTruthy();
        })
    });

    describe('when passing an non adjacent rectange matrix', () => {
        const payload = [
            [1, 0, 0, 0, 0],
            [1, 0, 0, 0, 0],
            [1, 0, 0, 0, 0],
            [0, 0, 0, 0, 0],
            [1, 0, 0, 0, 0],
            [1, 0, 0, 0, 0],
            [0, 0, 0, 0, 0]
        ];

        it('should return true', () => {
            const result = MatrixUtils.isAdjacent(payload);
            expect(result).toBeFalsy();
        })
    });

    describe('when passing an adjacent square matrix with more complex pattern 1', () => {
        const payload = [
            [1, 0, 1, 0, 0],
            [1, 1, 1, 0, 0],
            [0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0]
        ];

        it('should return true', () => {
            const result = MatrixUtils.isAdjacent(payload);
            expect(result).toBeTruthy();
        })
    });

    describe('when passing an adjacent square matrix with more complex pattern 2', () => {
        const payload = [
            [1, 0, 1, 0, 0],
            [1, 1, 1, 0, 0],
            [0, 0, 1, 0, 0],
            [0, 1, 1, 0, 0],
            [0, 0, 0, 0, 0]
        ];

        it('should return true', () => {
            const result = MatrixUtils.isAdjacent(payload);
            expect(result).toBeTruthy();
        })
    });
  });

  describe('rotate', () => {
    const matrix = [
        [1, 0, 0],
        [1, 0, 0],
        [1, 1, 0]
    ];

    describe('when rotating clockwise 1 time', () => {
        const result = MatrixUtils.rotate(matrix);

        it('should produce a rotatedmatrix', () => {
            const expected = [
                [1, 1, 1],
                [1, 0, 0],
                [0, 0, 0]
            ];

            expect(result).toEqual(expected);
        })
    });

    describe('when rotating clockwise 2 time', () => {
        const result = MatrixUtils.rotate(matrix, 2);

        it('should produce a rotatedmatrix', () => {
            const expected = [
                [0, 1, 1],
                [0, 0, 1],
                [0, 0, 1]
            ];

            expect(result).toEqual(expected);
        })
    });

    describe('when rotating clockwise 3 time', () => {
        const result = MatrixUtils.rotate(matrix, 3);

        it('should produce a rotatedmatrix', () => {
            const expected = [
                [0, 0, 0],
                [0, 0, 1],
                [1, 1, 1]
            ];

            expect(result).toEqual(expected);
        })
    });

    describe('when rotating clockwise 4 time', () => {
        const result = MatrixUtils.rotate(matrix, 4);

        it('should produce the original matrix', () => {
            expect(result).toEqual(matrix);
        })
    });
  });

  describe('mirror X', () => {
    const matrix = [
        [1, 0, 0],
        [1, 0, 0],
        [1, 1, 0]
    ];
    describe('when mirroring a matrix on the X axis', () => {
        const result = MatrixUtils.mirrorX(matrix);

        it('should produce the mirrored version of it', () => {
            const expected = [
                [1, 1, 0],
                [1, 0, 0],
                [1, 0, 0]
            ];

            expect(result).toEqual(expected)
        })
    });

    describe('when mirroring a matrix 2 times on the X axis', () => {
        let result = MatrixUtils.mirrorX(matrix);
        result = MatrixUtils.mirrorX(result);

        it('should produce the orriginal one', () => {
            expect(result).toEqual(matrix);
        })
    });
  });

  describe('mirror Y', () => {
    const matrix = [
        [1, 0, 0],
        [1, 0, 0],
        [1, 1, 0]
    ];
    describe('when mirroring a matrix on the Y axis', () => {
        const result = MatrixUtils.mirrorY(matrix);

        it('should produce the mirrored version of it', () => {
            const expected = [
                [0, 0, 1],
                [0, 0, 1],
                [0, 1, 1]
            ];

            expect(result).toEqual(expected)
        })
    });

    describe('when mirroring a matrix 2 times on the X axis', () => {
        let result = MatrixUtils.mirrorY(matrix);
        result = MatrixUtils.mirrorY(result);

        it('should produce the orriginal one', () => {
            expect(result).toEqual(matrix);
        })
    });
  });
});