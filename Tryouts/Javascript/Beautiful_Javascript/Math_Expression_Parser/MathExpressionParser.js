// Parser

var T = {
	Identifier: "Identifier",
	Number: "Number",
	Assignment: "Assignment",
	Operator: "Operator"
}

var lexer = new Lexer();

function matchOp(token, op) {
	return (typeof token !== 'undefined') &&
		token.type === T.Operator &&
		token.value === op;
}

function parseAssignment() {
	var token, expr;
	expr = parseAdditive();
	if (typeof expr !== 'undefined' && expr.Identifier) {
		token = lexer.peek();
		if (matchOp(token, '=')) {
			lexer.next();
			return {
				'Assignment': {
					name: expr,
					value: parseAssignment()
				}
			};
		}
		return expr;
	}
	return expr;
}

function parseExpression() {
	return parseAssignment();
}

function parseAdditive() {
	var expr, token;
	expr = parseMultiplicative();
	token = lexer.peek();
	while (matchOp(token, '+') || matchOp(token, '-')) {
		token = lexer.next();
		expr = {
			'Binary': {
				operator: token.value,
				left: expr,
				right: parseMultiplicative()
			}
		}
		token = lexer.peek();
	};
	return expr;
}

function parseMultiplicative() {
	var expr, token;
	expr = parseUnary();
	token = lexer.peek();
	while (matchOp(token, '*') || matchOp(token, '/')) {
		token = lexer.next();
		expr = {
			'Binary': {
				operator: token.value,
				left: expr,
				right: parseUnary()
			}
		};
		token = lexer.peek();
	}
	return expr;
}

function parseUnary() {
	var token, expr;
	token = lexer.peek();

	if (matchOp(token, '-') || matchOp(token, '+')) {
		token = lexer.next();
		expr = parseUnary();

		return {
			'Unary': {
				operator: token.value,
				expression: expr
			}
		};
	}

	return parsePrimary();
}

function parsePrimary() {
	var token, expr;
	token = lexer.peek();

	if (token.type === T.Identifier) {
		token = lexer.next();
		if (matchOp(lexer.peek(), '(')) {
			return parseFunctionCall(token.value);
		} else {
			return {
				'Identifier': token.value
			};
		}
	}

	if (token.type === T.Number) {
		token = lexer.next();
		return {
			'Number': token.value
		};
	}

	if (matchOp(token, '(')) {
		lexer.next();
		expr = parseAssignment();
		token = lexer.next();
		if (!matchOp(token, ')')) {
			throw new SyntaxError('Expecting )');
		}
		return {
			'Expression': expr
		};
	}

	throw new SyntaxError('Parse error, can not process token ' + token.value);
}

function parseArgumentList() {
	var token, expr, args = [];
	while (true) {
		expr = parseExpression();
		if (typeof expr === 'undefined') {
			break;
		}
		args.push(expr);
		token = lexer.peek();
		if (!matchOp(token, ',')) {
			break;
		}
		lexer.next();
	}
	return args;
}

function parseFunctionCall(name) {
	var token, args = [];
	token = lexer.next();
	if (!matchOp(token, '(')) {
		throw new SyntaxError('Expecting ( in a function call "' + name + '"');
	}
	token = lexer.peek();
	if (!matchOp(token, ')')) {
		args = parseArgumentList();
	}
	token = lexer.next();
	if (!matchOp(token, ')')) {
		throw new SyntaxError('Expecting ) in a function call "' + name + '"');
	}
	return {
		'FunctionCall': {
			'name': name,
			'args': args
		}
	};
}

// main entry point
function parse(expression) {
	var expr;
	lexer.reset(expression);
	expr = parseExpression();
	
	return {
		'Expression': expr
	};
}

// Tree Walker and Expression Evaluator
function EvaluatExpression() {

	var context = {
		Constants: {
			pi: 3.1415926535897932384,
			phi: 1.6180339887498948482
		},
		Functions: {
			abs: Math.abs,
			acos: Math.acos,
			asin: Math.asin,
			atan: Math.atan,
			ceil: Math.ceil,
			cos: Math.cos,
			exp: Math.exp,
			floor: Math.floor,
			ln: Math.ln,
			random: Math.random,
			sin: Math.sin,
			sqrt: Math.sqrt,
			tan: Math.tan
		},
		Variables: {}
	}

	if (node.hasOwnProperty('Number')) {
		return parseFloat(node.Number);
	}

	if (node.hasOwnProperty('Unary')) {
		node = node.Unary;
		expr = exec(node.expression);
		switch (node.operator) {
			case '+':
				return expr;
			case '-':
				return -expr;
			default:
				throw new SyntaxError('Unknown operator ' + node.operator);
		}
	}

	if (node.hasOwnProperty('Binary')) {
		node = node.Binary;
		left = exec(node.left);
		right = exec(node.right);
		switch (node.operator) {
			case '+':
				return left + right;
			case '-':
				return left - right;
			case '*':
				return left * right;
			case '/':
				return left / right;
			default:
				throw new SyntaxError('Unknown operator ' + node.operator);
		}
	}

	if (node.hasOwnProperty('Identifier')) {
		if (context.Constants.hasOwnProperty(node.Identifier)) {
			return context.Constants[node.Identifier];
		}
		if (context.Variables.hasOwnProperty(node.Identifier)) {
			return context.Variables[node.Identifier];
		}
		throw new SyntaxError('Unknown identifier');
	}

	if (node.hasOwnProperty('Assignment')) {
		right = exec(node.Assignment.value);
		context.Variables[node.Assignment.name.Identifier] = right;
		return right;
	}

	if (node.hasOwnProperty('FunctionCall')) {
		expr = node.FunctionCall;
		if (context.Functions.hasOwnProperty(expr.name)) {
			args = [];
			for (i = 0; i < expr.args.length; i += 1) {
				args.push(exec(expr.args[i]));
			}
			return context.Functions[expr.name].apply(null, args);
		}
		throw new SyntaxError('Unknown function ' + expr.name);
	}

	context.Functions.sum = function () {
		var i, total = 0;
		for (i = 0; i < arguments.length; i += 1) {
			total += arguments[i];
		}
		return total;
	}
}

