function Node(_value, left, right) {
	this.value = _value;
	this.left = left;
	this.right = right;
}

function BinarySearchTree() {
	this.root = null;
	this.insert = insert;
	this.inOrder = inOrder;
	this.postOrder = postOrder;
	this.preOrder = preOrder;
	this.minValue = getMin;
	this.maxValue = getMax;
	this.find = find;
}

function insert(_value) {
	var n = new Node(_value, null, null);
	if (this.root == null) {
		this.root = n;
	}
	else {
		var current = this.root;
		var parent;
		while (true) {
			parent = current;
			if (_value < current.value) {
				current = current.left;
				if (current == null) {
					parent.left = n;
					break;
				}
			}
			else {
				current = current.right;
				if (current == null) {
					parent.right = n;
					break;
				}
			}
		}
	}
}

function inOrder(node) {
	if (!(node == null)) {
		inOrder(node.left);
		console.log(node.value + " ");
		inOrder(node.right);
	}
}

function preOrder(node) {
	if (!(node == null)) {
		console.log(node.value + " ");
		preOrder(node.left);
		preOrder(node.right);
	}
}

function postOrder(node) {
	if (!(node == null)) {
		postOrder(node.left);
		postOrder(node.right);
		console.log(node.value + " ");
	}
}

function getMin() {
	var current = this.root;
	while (!(current.left == null)) {
		current = current.left;
	}
	return current.value;
}

function getMax() {
	var current = this.root;
	while (!(current.right == null)) {
		current = current.right;
	}
	return current.value;
}

function find(val) {
	var current = this.root;
	while (current.value != val) {
		if (val < current.value) {
			current = current.left;
		}
		else {
			current = current.right;
		}
		if (current == null) {
			return null;
		}
	}
	return current;
}

function remove(val) {
	root = removeNode(this.root, val);
}

function removeNode(node, data) {
	if (node == null) {
		return null;
	}
	if (data == node.value) {
		// node has no children
		if (node.left == null && node.right == null) {
			return null;
		}

		// node has no left child
		if (node.left == null) {
			return node.right;
		}

		// node has no right child
		if (node.right == null) {
			return node.left;
		}

		// node has two children
		var tempNode = getSmallest(node.right);
		node.value = tempNode.data;
		node.right = removeNode(node.right, tempNode.value);
		return node;
	}
	else if (data < node.value) {
		node.left = removeNode(node.left, data);
		return node;
	}
	else {
		node.right = removeNode(node.right, data);
		return node;
	}
}