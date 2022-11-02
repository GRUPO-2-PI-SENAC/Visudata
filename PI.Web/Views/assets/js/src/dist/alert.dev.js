"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports["default"] = void 0;

var _index = require("./util/index");

var _eventHandler = _interopRequireDefault(require("./dom/event-handler"));

var _baseComponent = _interopRequireDefault(require("./base-component"));

var _componentFunctions = require("./util/component-functions");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

function _typeof(obj) { if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

function _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === "object" || typeof call === "function")) { return call; } return _assertThisInitialized(self); }

function _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return self; }

function _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

var NAME = 'alert';
var DATA_KEY = 'bs.alert';
var EVENT_KEY = ".".concat(DATA_KEY);
var EVENT_CLOSE = "close".concat(EVENT_KEY);
var EVENT_CLOSED = "closed".concat(EVENT_KEY);
var CLASS_NAME_FADE = 'fade';
var CLASS_NAME_SHOW = 'show';
/**
 * Class definition
 */

var Alert =
/*#__PURE__*/
function (_BaseComponent) {
  _inherits(Alert, _BaseComponent);

  function Alert() {
    _classCallCheck(this, Alert);

    return _possibleConstructorReturn(this, _getPrototypeOf(Alert).apply(this, arguments));
  }

  _createClass(Alert, [{
    key: "close",
    // Public
    value: function close() {
      var _this = this;

      var closeEvent = _eventHandler["default"].trigger(this._element, EVENT_CLOSE);

      if (closeEvent.defaultPrevented) {
        return;
      }

      this._element.classList.remove(CLASS_NAME_SHOW);

      var isAnimated = this._element.classList.contains(CLASS_NAME_FADE);

      this._queueCallback(function () {
        return _this._destroyElement();
      }, this._element, isAnimated);
    } // Private

  }, {
    key: "_destroyElement",
    value: function _destroyElement() {
      this._element.remove();

      _eventHandler["default"].trigger(this._element, EVENT_CLOSED);

      this.dispose();
    } // Static

  }], [{
    key: "jQueryInterface",
    value: function jQueryInterface(config) {
      return this.each(function () {
        var data = Alert.getOrCreateInstance(this);

        if (typeof config !== 'string') {
          return;
        }

        if (data[config] === undefined || config.startsWith('_') || config === 'constructor') {
          throw new TypeError("No method named \"".concat(config, "\""));
        }

        data[config](this);
      });
    }
  }, {
    key: "NAME",
    // Getters
    get: function get() {
      return NAME;
    }
  }]);

  return Alert;
}(_baseComponent["default"]);
/**
 * Data API implementation
 */


(0, _componentFunctions.enableDismissTrigger)(Alert, 'close');
/**
 * jQuery
 */

(0, _index.defineJQueryPlugin)(Alert);
var _default = Alert;
exports["default"] = _default;