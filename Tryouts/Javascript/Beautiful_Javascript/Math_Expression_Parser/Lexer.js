function Lexer() {

	var _expression = "";
	var _index = 0,
		_length = 0;
	var _token;

	var isWhiteSpace = function(ch) {
		return (ch === 'u0009') || (ch === ' ') || (ch === 'u00A0');
	}
	
	var isLetter = function(ch) {
		return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z');
	}
	
	var isDecimalDigit = function(ch) {
		return (ch >= '0') && (ch <= '9');
	}
	
	var createToken = function(type, value) {
		return {
			type: type,
			value: value
		};
	}
	
	var getNextChar = function() {
		var ch = 'x00',
			idx = _index;
		if (idx < _length) {
			ch = _expression.charAt(idx);
			_index += 1;
		}
		return ch;
	}
	
	var peekNextChar = function() {
		var idx = _index;
		return ((idx < _length) ? _expression.charAt(idx) : 'x00');
	}
	
	var skipSpaces = function() {
		var ch;
		while (_index < _length) {
			ch = peekNextChar();
			if (!isWhiteSpace(ch)) {
				break;
			}
			getNextChar();
		}
	}
	
	var isIdentifierStart= function(ch) {
		return (ch === '_') || isLetter(ch);
	}
	
	var isIdentifierPart = function(ch) {
		return isIdentifierStart(ch) || isDecimalDigit(ch);
	}
	
	var scanIdentifier = function() {
		var ch, id;
		ch = peekNextChar();
		if (!isIdentifierStart(ch)) {
			return undefined;
		}
		id = getNextChar();
		while (true) {
			ch = peekNextChar();
			if (!isIdentifierPart(ch)) {
				break;
			}
			id += getNextChar();
		}
		return createToken('Identifier', id);
	}
	
	var scanOperator = function() {
		var ch = peekNextChar();
		if ('+-*/()='.indexOf(ch) >= 0) {
			return createToken('Operator', getNextChar());
		}
		return undefined;
	}

	var scanNumber = function() {
		ch = peekNextChar();
		if (!isDecimalDigit(ch) && (ch !== '.')) {
			return undefined;
		}
	
		number = '';
		if (ch !== '.') {
			number = getNextChar();
			while (true) {
				ch = peekNextChar();
				if (!isDecimalDigit(ch)) {
					break;
				}
				number += getNextChar();
			}
		}
	
		if (ch === '.') {
			number += getNextChar();
			while (true) {
				ch = peekNextChar();
				if (!isDecimalDigit(ch)) {
					break;
				}
				number += getNextChar();
			}
		}
	
		if (ch === 'e' || ch === 'E') {
			number += getNextChar();
			ch = peekNextChar();
			if (ch === '+' || ch === '-' || isDecimalDigit(ch)) {
				number += getNextChar();
				while (true) {
					ch = peekNextChar();
					if (!isDecimalDigit(ch)) {
						break;
					}
					number += getNextChar();
				}
			} else {
				throw new SyntaxError('Unexpected character after exponent sign');
			}
		}

		return createToken('Number', number);
	}
	
	// Advancing to the next token
	this.next = function() {
		skipSpaces();

		var token;
		if (_index >= _length) {
			return undefined;
		}

		token = scanNumber();
		if (typeof token !== 'undefined') {
			_token = token;
			return token;
		}

		token = scanOperator();
		if (typeof token !== 'undefined') {
			_token = token;
			return token;
		}

		token = scanIdentifier();
		if (typeof token !== 'undefined') {
			_token = token;
			return token;
		}

		throw new SyntaxError('Unknown token from character ' + peekNextChar());
	}

	this.reset = function(expression) {
		_expression = expression;
		_length = _expression.length;
		_index = 0;
	}

	// Should return current (or the next) token without advanceing to the next index
	this.peek = function() {
		if( _token === undefined )
			this.next();
		return _token;
	}
}