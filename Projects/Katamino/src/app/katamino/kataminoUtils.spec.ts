import { KataminoUtils } from "./kataminoUtils";

describe('Katamino utils', () => {
    describe('Rotate', () => {
        const testkatamino = KataminoUtils.createFromMatrix([
            [1, 0, 0],
            [1, 0, 0],
            [1, 1, 0]
        ]);

        describe('When rotating katamino 1 time clockwise', () => {
            const result = KataminoUtils.rotate(testkatamino);

            it('should return a rotated katamino', () => {
                const expected = KataminoUtils.createFromMatrix([
                    [1, 1, 1],
                    [1, 0, 0],
                    [0, 0, 0]
                ]);
                
                expect(result).toEqual(expected);
            })
        })
    });
});