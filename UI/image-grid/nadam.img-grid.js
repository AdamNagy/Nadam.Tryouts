function ImageGrid(columns, rows) {

	var self = this;

	var columns = columns;
	this.grid = new Array(rows || 4);

	// <ctor>
	(function(){
		for(var i = 0; i < self.grid.length; ++i) {
			self.grid[i] = new Array(columns);
			for(var j = 0; j < columns; ++j) {
				self.grid[i][j] = 'o';
			}
		}	
	})()
	// </ctor>

	// <private>
	var extendGrid = function() {

		var newRow = new Array(columns);
		for(var i = 0; i < columns; ++i) {
			newRow[i] = 'o';
		}
		self.grid.push(newRow);
	}

	var isAreaFree = function(rowIdx, colIdx, rowSpan, colSpan) {

		for(var i = rowIdx; i < rowIdx + rowSpan; ++i) {
			for(var j = colIdx; j < colIdx+colSpan; ++j) {
				if( self.grid[i][j] == "x" )
					return false;
			}
		}

		return true;
	}
	// </private>

	// <public>
	this.nextFreeArea = function(rowIdx, colIdx, rowSpan, colSpan ) {

		if( rowIdx === undefined || rowIdx < 0)
			rowIdx = 0;
		if(colIdx === undefined || colIdx < 0)
			colIdx = 0;

		var found = false;
		var nextAreaIdxs = {}
		for(var i = rowIdx; i < this.grid.length-rowSpan+1  && !found; ++i) {
			for(var j = colIdx; j < this.grid[rowIdx].length-colSpan+1  && !found; ++j) {
				found = isAreaFree(i, j, rowSpan, colSpan);
				if(found) {
					nextAreaIdxs = {i: i, j: j};
					break;
				}
			}

			if( !found && i == this.grid.length-rowSpan )
				extendGrid();			
		}

		// landscape
		if( colSpan > rowSpan ) {

			var nextPortraitIdxs = this.nextFreeArea(nextAreaIdxs.i-2, nextAreaIdxs.j, colSpan, rowSpan);
			_portraitrRowIdx = nextPortraitIdxs.i + colSpan - 1;
			if( (_portraitrRowIdx === nextAreaIdxs.i || 
				_portraitrRowIdx === nextAreaIdxs.i+1) && 
				nextPortraitIdxs.j === nextAreaIdxs.j) {
					nextAreaIdxs = this.nextFreeArea(nextAreaIdxs.i, nextAreaIdxs.j+colSpan-1, rowSpan, colSpan);
				}
		}

		return nextAreaIdxs;
	};

	this.reserve = function(rowIdx, colIdx, rowSpan, colSpan) {

		var _colIdx = colIdx + colSpan;
		var _rowIdx = rowIdx + rowSpan;

		for(var i = rowIdx; i < _rowIdx; ++i) {
			for(var j = colIdx; j < _colIdx; ++j) {
				if(self.grid[i][j] == 'x')
					return false;
				
				self.grid[i][j] = 'x';
			}
		}

		return true;
	};

	this.printGrid = function(element) {
		
		for(var i = 0; i < this.grid.length; ++i) {

			var line = document.createElement("div");
			var lineText = i.toString() + '\t';
			for(var j = 0; j < this.grid[i].length; ++j) {
				lineText = lineText + " " + this.grid[i][j];
			}

			line.innerText = lineText;
			element.append(line);
		}

		element.append(document.createElement("hr"));
	};
	// </public>
}