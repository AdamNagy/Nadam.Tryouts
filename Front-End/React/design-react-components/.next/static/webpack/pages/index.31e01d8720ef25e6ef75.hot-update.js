self["webpackHotUpdate_N_E"]("pages/index",{

/***/ "./src/hooks/useRequestDelay.js":
/*!**************************************!*\
  !*** ./src/hooks/useRequestDelay.js ***!
  \**************************************/
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "REQUEST_STATUS": function() { return /* binding */ REQUEST_STATUS; }
/* harmony export */ });
/* harmony import */ var C_Users_Adam_Nagy1_Documents_Learning_Nadam_Tryouts_Front_End_React_design_react_components_node_modules_babel_runtime_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./node_modules/@babel/runtime/regenerator */ "./node_modules/@babel/runtime/regenerator/index.js");
/* harmony import */ var C_Users_Adam_Nagy1_Documents_Learning_Nadam_Tryouts_Front_End_React_design_react_components_node_modules_babel_runtime_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(C_Users_Adam_Nagy1_Documents_Learning_Nadam_Tryouts_Front_End_React_design_react_components_node_modules_babel_runtime_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var C_Users_Adam_Nagy1_Documents_Learning_Nadam_Tryouts_Front_End_React_design_react_components_node_modules_babel_runtime_helpers_esm_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./node_modules/@babel/runtime/helpers/esm/asyncToGenerator */ "./node_modules/@babel/runtime/helpers/esm/asyncToGenerator.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_2__);
/* module decorator */ module = __webpack_require__.hmd(module);



var _s = $RefreshSig$();


var REQUEST_STATUS = {
  LOADING: "loading",
  SUCCESS: "success",
  FAILURE: "failure"
};

function useRequestDelay() {
  _s();

  var delayTime = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : 1000;
  var initialData = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : [];

  var _useState = (0,react__WEBPACK_IMPORTED_MODULE_2__.useState)(initialData),
      data = _useState[0],
      setData = _useState[1];

  var _useState2 = (0,react__WEBPACK_IMPORTED_MODULE_2__.useState)(REQUEST_STATUS.LOADING),
      requestStatus = _useState2[0],
      setRequestStatus = _useState2[1];

  var _useState3 = (0,react__WEBPACK_IMPORTED_MODULE_2__.useState)(""),
      error = _useState3[0],
      setError = _useState3[1];

  var delay = function delay(ms) {
    return new Promise(function (resolve, reject) {
      return setTimeout(resolve, ms);
    });
  };

  (0,react__WEBPACK_IMPORTED_MODULE_2__.useEffect)(function () {
    function delayFunc() {
      return _delayFunc.apply(this, arguments);
    }

    function _delayFunc() {
      _delayFunc = (0,C_Users_Adam_Nagy1_Documents_Learning_Nadam_Tryouts_Front_End_React_design_react_components_node_modules_babel_runtime_helpers_esm_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__.default)( /*#__PURE__*/C_Users_Adam_Nagy1_Documents_Learning_Nadam_Tryouts_Front_End_React_design_react_components_node_modules_babel_runtime_regenerator__WEBPACK_IMPORTED_MODULE_0___default().mark(function _callee() {
        return C_Users_Adam_Nagy1_Documents_Learning_Nadam_Tryouts_Front_End_React_design_react_components_node_modules_babel_runtime_regenerator__WEBPACK_IMPORTED_MODULE_0___default().wrap(function _callee$(_context) {
          while (1) {
            switch (_context.prev = _context.next) {
              case 0:
                _context.prev = 0;
                _context.next = 3;
                return delay(delayTime);

              case 3:
                setRequestStatus(REQUEST_STATUS.SUCCESS);
                setData(data);
                _context.next = 11;
                break;

              case 7:
                _context.prev = 7;
                _context.t0 = _context["catch"](0);
                setRequestStatus(REQUEST_STATUS.FAILURE);
                setError("Shit happens");

              case 11:
              case "end":
                return _context.stop();
            }
          }
        }, _callee, null, [[0, 7]]);
      }));
      return _delayFunc.apply(this, arguments);
    }

    delayFunc();
  }, []);

  function updateRecord(recordUpdate, doneCallback) {
    var newRecords = data.map(function (rec) {
      return rec.id === recordUpdate.id ? recordUpdate : rec;
    });

    function delayFunction() {
      return _delayFunction.apply(this, arguments);
    }

    function _delayFunction() {
      _delayFunction = (0,C_Users_Adam_Nagy1_Documents_Learning_Nadam_Tryouts_Front_End_React_design_react_components_node_modules_babel_runtime_helpers_esm_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__.default)( /*#__PURE__*/C_Users_Adam_Nagy1_Documents_Learning_Nadam_Tryouts_Front_End_React_design_react_components_node_modules_babel_runtime_regenerator__WEBPACK_IMPORTED_MODULE_0___default().mark(function _callee2() {
        return C_Users_Adam_Nagy1_Documents_Learning_Nadam_Tryouts_Front_End_React_design_react_components_node_modules_babel_runtime_regenerator__WEBPACK_IMPORTED_MODULE_0___default().wrap(function _callee2$(_context2) {
          while (1) {
            switch (_context2.prev = _context2.next) {
              case 0:
                _context2.prev = 0;
                setData(newRecords);
                _context2.next = 4;
                return delay(400);

              case 4:
                if (doneCallback) {
                  doneCallback();
                }

                _context2.next = 10;
                break;

              case 7:
                _context2.prev = 7;
                _context2.t0 = _context2["catch"](0);
                console.log(_context2.t0);

              case 10:
              case "end":
                return _context2.stop();
            }
          }
        }, _callee2, null, [[0, 7]]);
      }));
      return _delayFunction.apply(this, arguments);
    }

    delayFunction();
  }

  return {
    data: data,
    requestStatus: requestStatus,
    error: error,
    updateRecord: updateRecord
  };
}

_s(useRequestDelay, "/Q5hd3zsV2Ij7JDh84VFSCYTHHg=");

/* harmony default export */ __webpack_exports__["default"] = (useRequestDelay);

;
    var _a, _b;
    // Legacy CSS implementations will `eval` browser code in a Node.js context
    // to extract CSS. For backwards compatibility, we need to check we're in a
    // browser context before continuing.
    if (typeof self !== 'undefined' &&
        // AMP / No-JS mode does not inject these helpers:
        '$RefreshHelpers$' in self) {
        var currentExports = module.__proto__.exports;
        var prevExports = (_b = (_a = module.hot.data) === null || _a === void 0 ? void 0 : _a.prevExports) !== null && _b !== void 0 ? _b : null;
        // This cannot happen in MainTemplate because the exports mismatch between
        // templating and execution.
        self.$RefreshHelpers$.registerExportsForReactRefresh(currentExports, module.id);
        // A module can be accepted automatically based on its exports, e.g. when
        // it is a Refresh Boundary.
        if (self.$RefreshHelpers$.isReactRefreshBoundary(currentExports)) {
            // Save the previous exports on update so we can compare the boundary
            // signatures.
            module.hot.dispose(function (data) {
                data.prevExports = currentExports;
            });
            // Unconditionally accept an update to this module, we'll check if it's
            // still a Refresh Boundary later.
            module.hot.accept();
            // This field is set when the previous version of this module was a
            // Refresh Boundary, letting us know we need to check for invalidation or
            // enqueue an update.
            if (prevExports !== null) {
                // A boundary can become ineligible if its exports are incompatible
                // with the previous exports.
                //
                // For example, if you add/remove/change exports, we'll want to
                // re-execute the importing modules, and force those components to
                // re-render. Similarly, if you convert a class component to a
                // function, we want to invalidate the boundary.
                if (self.$RefreshHelpers$.shouldInvalidateReactRefreshBoundary(prevExports, currentExports)) {
                    module.hot.invalidate();
                }
                else {
                    self.$RefreshHelpers$.scheduleUpdate();
                }
            }
        }
        else {
            // Since we just executed the code for the module, it's possible that the
            // new exports made it ineligible for being a boundary.
            // We only care about the case when we were _previously_ a boundary,
            // because we already accepted this update (accidental side effect).
            var isNoLongerABoundary = prevExports !== null;
            if (isNoLongerABoundary) {
                module.hot.invalidate();
            }
        }
    }


/***/ })

});
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly9fTl9FLy4vc3JjL2hvb2tzL3VzZVJlcXVlc3REZWxheS5qcyJdLCJuYW1lcyI6WyJSRVFVRVNUX1NUQVRVUyIsIkxPQURJTkciLCJTVUNDRVNTIiwiRkFJTFVSRSIsInVzZVJlcXVlc3REZWxheSIsImRlbGF5VGltZSIsImluaXRpYWxEYXRhIiwidXNlU3RhdGUiLCJkYXRhIiwic2V0RGF0YSIsInJlcXVlc3RTdGF0dXMiLCJzZXRSZXF1ZXN0U3RhdHVzIiwiZXJyb3IiLCJzZXRFcnJvciIsImRlbGF5IiwibXMiLCJQcm9taXNlIiwicmVzb2x2ZSIsInJlamVjdCIsInNldFRpbWVvdXQiLCJ1c2VFZmZlY3QiLCJkZWxheUZ1bmMiLCJ1cGRhdGVSZWNvcmQiLCJyZWNvcmRVcGRhdGUiLCJkb25lQ2FsbGJhY2siLCJuZXdSZWNvcmRzIiwibWFwIiwicmVjIiwiaWQiLCJkZWxheUZ1bmN0aW9uIiwiY29uc29sZSIsImxvZyJdLCJtYXBwaW5ncyI6Ijs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7O0FBQUE7QUFFTyxJQUFNQSxjQUFjLEdBQUc7QUFDMUJDLFNBQU8sRUFBRSxTQURpQjtBQUUxQkMsU0FBTyxFQUFFLFNBRmlCO0FBRzFCQyxTQUFPLEVBQUU7QUFIaUIsQ0FBdkI7O0FBTVAsU0FBU0MsZUFBVCxHQUE2RDtBQUFBOztBQUFBLE1BQXBDQyxTQUFvQyx1RUFBeEIsSUFBd0I7QUFBQSxNQUFsQkMsV0FBa0IsdUVBQUosRUFBSTs7QUFBQSxrQkFDakNDLCtDQUFRLENBQUNELFdBQUQsQ0FEeUI7QUFBQSxNQUNsREUsSUFEa0Q7QUFBQSxNQUM1Q0MsT0FENEM7O0FBQUEsbUJBRWZGLCtDQUFRLENBQUNQLGNBQWMsQ0FBQ0MsT0FBaEIsQ0FGTztBQUFBLE1BRWxEUyxhQUZrRDtBQUFBLE1BRW5DQyxnQkFGbUM7O0FBQUEsbUJBRy9CSiwrQ0FBUSxDQUFDLEVBQUQsQ0FIdUI7QUFBQSxNQUdsREssS0FIa0Q7QUFBQSxNQUczQ0MsUUFIMkM7O0FBS3pELE1BQU1DLEtBQUssR0FBRyxTQUFSQSxLQUFRLENBQUNDLEVBQUQ7QUFBQSxXQUFRLElBQUlDLE9BQUosQ0FBWSxVQUFDQyxPQUFELEVBQVVDLE1BQVY7QUFBQSxhQUFxQkMsVUFBVSxDQUFDRixPQUFELEVBQVVGLEVBQVYsQ0FBL0I7QUFBQSxLQUFaLENBQVI7QUFBQSxHQUFkOztBQUVBSyxrREFBUyxDQUFDLFlBQU07QUFBQSxhQUNHQyxTQURIO0FBQUE7QUFBQTs7QUFBQTtBQUFBLDZZQUNaO0FBQUE7QUFBQTtBQUFBO0FBQUE7QUFBQTtBQUFBO0FBQUEsdUJBRWNQLEtBQUssQ0FBQ1QsU0FBRCxDQUZuQjs7QUFBQTtBQUdRTSxnQ0FBZ0IsQ0FBQ1gsY0FBYyxDQUFDRSxPQUFoQixDQUFoQjtBQUNBTyx1QkFBTyxDQUFDRCxJQUFELENBQVA7QUFKUjtBQUFBOztBQUFBO0FBQUE7QUFBQTtBQU1RRyxnQ0FBZ0IsQ0FBQ1gsY0FBYyxDQUFDRyxPQUFoQixDQUFoQjtBQUNBVSx3QkFBUSxDQUFDLGNBQUQsQ0FBUjs7QUFQUjtBQUFBO0FBQUE7QUFBQTtBQUFBO0FBQUE7QUFBQSxPQURZO0FBQUE7QUFBQTs7QUFZWlEsYUFBUztBQUNaLEdBYlEsRUFhTixFQWJNLENBQVQ7O0FBZUEsV0FBU0MsWUFBVCxDQUFzQkMsWUFBdEIsRUFBb0NDLFlBQXBDLEVBQWtEO0FBQzlDLFFBQU1DLFVBQVUsR0FBR2pCLElBQUksQ0FBQ2tCLEdBQUwsQ0FBUyxVQUFDQyxHQUFELEVBQVM7QUFDakMsYUFBT0EsR0FBRyxDQUFDQyxFQUFKLEtBQVdMLFlBQVksQ0FBQ0ssRUFBeEIsR0FBNkJMLFlBQTdCLEdBQTRDSSxHQUFuRDtBQUNILEtBRmtCLENBQW5COztBQUQ4QyxhQUsvQkUsYUFMK0I7QUFBQTtBQUFBOztBQUFBO0FBQUEsaVpBSzlDO0FBQUE7QUFBQTtBQUFBO0FBQUE7QUFBQTtBQUVRcEIsdUJBQU8sQ0FBQ2dCLFVBQUQsQ0FBUDtBQUZSO0FBQUEsdUJBR2NYLEtBQUssQ0FBQyxHQUFELENBSG5COztBQUFBO0FBSVEsb0JBQUlVLFlBQUosRUFBbUI7QUFDZkEsOEJBQVk7QUFDZjs7QUFOVDtBQUFBOztBQUFBO0FBQUE7QUFBQTtBQVNRTSx1QkFBTyxDQUFDQyxHQUFSOztBQVRSO0FBQUE7QUFBQTtBQUFBO0FBQUE7QUFBQTtBQUFBLE9BTDhDO0FBQUE7QUFBQTs7QUFrQjlDRixpQkFBYTtBQUNoQjs7QUFFRCxTQUFPO0FBQ0hyQixRQUFJLEVBQUpBLElBREc7QUFFSEUsaUJBQWEsRUFBYkEsYUFGRztBQUdIRSxTQUFLLEVBQUxBLEtBSEc7QUFJSFUsZ0JBQVksRUFBWkE7QUFKRyxHQUFQO0FBTUg7O0dBakRRbEIsZTs7QUFtRFQsK0RBQWVBLGVBQWYiLCJmaWxlIjoic3RhdGljL3dlYnBhY2svcGFnZXMvaW5kZXguMzFlMDFkODcyMGVmMjVlNmVmNzUuaG90LXVwZGF0ZS5qcyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IHVzZVN0YXRlLCB1c2VFZmZlY3QgfSBmcm9tIFwicmVhY3RcIjtcclxuXHJcbmV4cG9ydCBjb25zdCBSRVFVRVNUX1NUQVRVUyA9IHtcclxuICAgIExPQURJTkc6IFwibG9hZGluZ1wiLFxyXG4gICAgU1VDQ0VTUzogXCJzdWNjZXNzXCIsXHJcbiAgICBGQUlMVVJFOiBcImZhaWx1cmVcIlxyXG59XHJcblxyXG5mdW5jdGlvbiB1c2VSZXF1ZXN0RGVsYXkoZGVsYXlUaW1lID0gMTAwMCwgaW5pdGlhbERhdGEgPSBbXSkge1xyXG4gICAgY29uc3QgW2RhdGEsIHNldERhdGFdID0gdXNlU3RhdGUoaW5pdGlhbERhdGEpO1xyXG4gICAgY29uc3QgW3JlcXVlc3RTdGF0dXMsIHNldFJlcXVlc3RTdGF0dXNdID0gdXNlU3RhdGUoUkVRVUVTVF9TVEFUVVMuTE9BRElORyk7XHJcbiAgICBjb25zdCBbZXJyb3IsIHNldEVycm9yXSA9IHVzZVN0YXRlKFwiXCIpO1xyXG5cclxuICAgIGNvbnN0IGRlbGF5ID0gKG1zKSA9PiBuZXcgUHJvbWlzZSgocmVzb2x2ZSwgcmVqZWN0KSA9PiBzZXRUaW1lb3V0KHJlc29sdmUsIG1zKSk7XHJcblxyXG4gICAgdXNlRWZmZWN0KCgpID0+IHtcclxuICAgICAgICBhc3luYyBmdW5jdGlvbiBkZWxheUZ1bmMoKSB7XHJcbiAgICAgICAgICAgIHRyeSB7XHJcbiAgICAgICAgICAgICAgICBhd2FpdCBkZWxheShkZWxheVRpbWUpO1xyXG4gICAgICAgICAgICAgICAgc2V0UmVxdWVzdFN0YXR1cyhSRVFVRVNUX1NUQVRVUy5TVUNDRVNTKTtcclxuICAgICAgICAgICAgICAgIHNldERhdGEoZGF0YSk7XHJcbiAgICAgICAgICAgIH0gY2F0Y2ggKGUpIHtcclxuICAgICAgICAgICAgICAgIHNldFJlcXVlc3RTdGF0dXMoUkVRVUVTVF9TVEFUVVMuRkFJTFVSRSk7XHJcbiAgICAgICAgICAgICAgICBzZXRFcnJvcihcIlNoaXQgaGFwcGVuc1wiKTtcclxuICAgICAgICAgICAgfVxyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgZGVsYXlGdW5jKCk7XHJcbiAgICB9LCBbXSk7XHJcblxyXG4gICAgZnVuY3Rpb24gdXBkYXRlUmVjb3JkKHJlY29yZFVwZGF0ZSwgZG9uZUNhbGxiYWNrKSB7XHJcbiAgICAgICAgY29uc3QgbmV3UmVjb3JkcyA9IGRhdGEubWFwKChyZWMpID0+IHtcclxuICAgICAgICAgICAgcmV0dXJuIHJlYy5pZCA9PT0gcmVjb3JkVXBkYXRlLmlkID8gcmVjb3JkVXBkYXRlIDogcmVjO1xyXG4gICAgICAgIH0pO1xyXG5cclxuICAgICAgICBhc3luYyBmdW5jdGlvbiBkZWxheUZ1bmN0aW9uKCkge1xyXG4gICAgICAgICAgICB0cnkge1xyXG4gICAgICAgICAgICAgICAgc2V0RGF0YShuZXdSZWNvcmRzKTtcclxuICAgICAgICAgICAgICAgIGF3YWl0IGRlbGF5KDQwMCk7XHJcbiAgICAgICAgICAgICAgICBpZiggZG9uZUNhbGxiYWNrICkge1xyXG4gICAgICAgICAgICAgICAgICAgIGRvbmVDYWxsYmFjaygpO1xyXG4gICAgICAgICAgICAgICAgfVxyXG5cclxuICAgICAgICAgICAgfSBjYXRjaChlKSB7XHJcbiAgICAgICAgICAgICAgICBjb25zb2xlLmxvZyhlKTtcclxuICAgICAgICAgICAgfVxyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgZGVsYXlGdW5jdGlvbigpO1xyXG4gICAgfVxyXG5cclxuICAgIHJldHVybiB7XHJcbiAgICAgICAgZGF0YSwgXHJcbiAgICAgICAgcmVxdWVzdFN0YXR1cyxcclxuICAgICAgICBlcnJvcixcclxuICAgICAgICB1cGRhdGVSZWNvcmRcclxuICAgIH1cclxufVxyXG5cclxuZXhwb3J0IGRlZmF1bHQgdXNlUmVxdWVzdERlbGF5OyJdLCJzb3VyY2VSb290IjoiIn0=