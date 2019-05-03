var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
var initialState = {
    home: 0,
    away: 0
};
function reducer(state) {
    if (state === void 0) { state = initialState; }
    var obj = __assign({}, state, { home: state.home + 1 });
    console.log(obj);
}
