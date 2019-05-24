const path = require('path');
var webpack = require('webpack');

module.exports = {
  entry: {
		app: './src/index.js',
		bootstrap: './src/bootstrap.js'
	},
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        use: 'ts-loader',
        exclude: /node_modules/
	  },
	  {
        test: /\.css$/,
        use: ['style-loader', 'css-loader']
      }
    ]
  },
  resolve: {
    extensions: [ '.tsx', '.ts', '.js' ]
  },
  output: {
	filename: '[name].bundle.js',
	chunkFilename: '[name].chunk.js',
    path: path.resolve(__dirname, 'dist')
  },
  // OPTION 2 to include jquery, other is in index.js
  plugins: [
	new webpack.ProvidePlugin({
		$: 'jquery',
		jQuery: 'jquery',
		'window.jQuery': 'jquery',
		Masonry: 'masonry-layout',
		jQueryBridget: 'jquery-bridget'
	})
  ],
  optimization: {
		splitChunks: {
			chunks: 'all'
		}
	}
};